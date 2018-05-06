using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyBase;
using System.IO;

namespace EasyBaseGenerator
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateDataClasses());
		}

		private void generateDataMethodsButton_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateDataMethods());
		}

		private void generateDataTables_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateDataTables());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateBusinessClasses());
		}

		private void generateBusinessMethodsButton_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateBusinessMethods());
		}

		private void generateBusinessCollectionButton_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateBusinessCollections());
		}

		private void generateEnumsButton_Click(object sender, EventArgs e)
		{
			StreamReader reader = new StreamReader("C:\\Users\\Daniel Nilsson\\Desktop\\Entities.txt");
			StringBuilder text = new StringBuilder();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(reader.ReadToEnd());

			reader.Close();

			TextMessageBox.Show(easyBaseSystem.GenerateEnums());
		}

		private void generateAllButton_Click(object sender, EventArgs e)
		{
            StreamReader reader = new StreamReader(entitiesPathFileNameTextBox.Text.Trim());
            StringBuilder text = new StringBuilder();
            
			string xmlString = reader.ReadToEnd();
			reader.Close();

			EasyBaseSystem easyBaseSystem = new EasyBaseSystem(xmlString);

			StreamWriter writer = new StreamWriter(projectPathTextBox.Text.Trim() + "\\Shared\\Generated\\Classes.cs");
			writer.Write(easyBaseSystem.GenerateDataClasses());
			writer.Close();

			writer = new StreamWriter(projectPathTextBox.Text.Trim() + "\\Shared\\Generated\\DataTables.cs");
			writer.Write(easyBaseSystem.GenerateDataTables());
			writer.Close();

            writer = new StreamWriter(projectPathTextBox.Text.Trim() + "\\Shared\\Generated\\Collections.cs");
			writer.Write(easyBaseSystem.GenerateBusinessCollections());
			writer.Close();

            writer = new StreamWriter(projectPathTextBox.Text.Trim() + "\\Shared\\Generated\\Enums.cs");
			writer.Write(easyBaseSystem.GenerateEnums());
			writer.Close();

            writer = new StreamWriter(projectPathTextBox.Text.Trim() + "\\DataLayer\\Generated\\DataLayer.cs");
			writer.Write(easyBaseSystem.GenerateDataMethods());
			writer.Close();

            writer = new StreamWriter(projectPathTextBox.Text.Trim() + "\\BusinessLayer\\Generated\\BusinessLayer.cs");
			writer.Write(easyBaseSystem.GenerateBusinessMethods());
			writer.Close();

			MessageBox.Show("Kod genererad!", "Easy Base Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
	}
}
