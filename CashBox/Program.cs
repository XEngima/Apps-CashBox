using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using EasyBase.BusinessLayer;
using EasyBase.Classes;

namespace CashBox
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool firstStart = false;
            DialogResult result;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool databaseCreatedFromScratch = false;

            try {
                using (var core = new StandardBusinessLayer())
                {
                    try
                    {
                        core.Connect(true);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 4060) // Database does not exist
                        {
                            core.CreateDatabaseFromScratch();
                            databaseCreatedFromScratch = true;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                using (var core = new StandardBusinessLayer()) {
                    bool needToUpdate = false;

                    if (databaseCreatedFromScratch)
                    {
                        int retries = 0;
                        bool databaseCreated = false;

                        while (!databaseCreated && retries < 10)
                        {
                            try
                            {
                                core.Connect(true);
                                core.BuildDatabase();
                                databaseCreated = true;
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 4060)
                                {
                                    retries++;
                                    Thread.Sleep(1000);
                                }
                            }
                        }

                        if (!databaseCreated)
                        {
                            MessageBox.Show("Anslutningen till databasen misslyckades. Det kan bero på att databasen behöver mer tid på sig för att skapas. Avsluta programmet och försök på nytt om någon minut.");
                        }
                    }
                    else
                    {
                        core.Connect(true);

                        try
                        {
                            needToUpdate = core.NeedToUpdateDatabase();
                        }
                        catch (DatabaseCorruptException)
                        {
                            DialogResult dialogResult = MessageBox.Show(
                                "Viktiga systemfält saknas i databasen. Vill du skapa om databasen?\n\nOm du väljer att skapa om databasen kommer all inlagd data att försvinna. Gör detta endast om du installerar programmet för första gången eller om du helt nyligen har tagit en backup.", "Korrupt databas", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                            if (dialogResult == DialogResult.Yes)
                            {
                                core.BuildDatabase();
                            }
                            else
                            {
                                Application.Exit();
                                return;
                            }
                        }
                    }

                    if (needToUpdate) {
                        result = MessageBox.Show("Databasen behöver uppdateras. Vill du uppdatera databasen nu?", "CashBox", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes) {

                            if (core.ConnectedToReleaseDatabase) {
                                DialogResult result2 = MessageBox.Show("Varning!\n\nDu försöker nu uppdatera det skarpa systemet. Vill du verkligen fortsätta?", "Varning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (result2 == DialogResult.No) {
                                    return;
                                }
                            };

                            // Uppdatera databasen
                            core.UpdateDatabase();
                        }
                        else {
                            Environment.Exit(0);
                        }
                    }

                    firstStart = core.CountUsers() == 0;
                }

                if (firstStart)
                {
                    CreateUsersForm createUsersForm = new CreateUsersForm();
                    createUsersForm.ShowDialog();
                }

                LoginForm loginForm = new LoginForm();
                result = loginForm.ShowDialog();

                if (result == DialogResult.OK) {
                    EasyBase.Classes.CurrentApplication.UserNo = loginForm.UserNo;
                    Application.Run(new MainForm());
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "CashBox Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (ex.InnerException != null) {
                    MessageBox.Show(ex.Message, "Inner Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
