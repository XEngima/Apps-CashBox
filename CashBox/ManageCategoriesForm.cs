using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyBase.BusinessLayer;
using EasyBase.Classes;

namespace CashBox
{
    public partial class ManageCategoriesForm : Form
    {
        public ManageCategoriesForm(DataCache dataCache)
        {
            InitializeComponent();
            DataCache = dataCache;
            IsDirty = false;
            IgnoreCategoryDropDownChange = false;
            IgnoreParentDropDownChange = false;
            NewCategory = false;
        }

        private DataCache DataCache
        {
            get; set;
        }

        private bool NewCategory
        {
            get;
            set;
        }

        private void EnableDisableControls()
        {
            settingsGroupBox.Enabled = (categoryComboBox.SelectedValue != null && (int)categoryComboBox.SelectedValue > 0) || NewCategory;
            saveButton.Enabled = IsDirty;
            deleteCategoryButton.Enabled = categoryComboBox.SelectedValue != null && (int)categoryComboBox.SelectedValue > 0;
        }

        private void ManageCategoriesForm_Load(object sender, EventArgs e)
        {
            ReloadForm();
            EnableDisableControls();
        }

        private Category _currentCategory;

        private Category CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                _currentCategory = value;

                if (_currentCategory != null)
                {
                    parentComboBox.SelectedValue = _currentCategory.ParentCategoryNo != null ? _currentCategory.ParentCategoryNo : 0;
                    nameTextBox.Text = _currentCategory.Name;
                    typeIncomeRadioButton.Checked = _currentCategory.Type == CategoryType.Income;
                    typeExpenseRadioButton.Checked = _currentCategory.Type == CategoryType.Expense;
                    showInDiagramCheckBox.Checked = _currentCategory.ShowInDiagram;
                    isArchivedCheckBox.Checked = _currentCategory.IsArchived;
                }
                else
                {
                    parentComboBox.SelectedValue = 0;
                    nameTextBox.Text = "";
                    typeIncomeRadioButton.Checked = false;
                    typeExpenseRadioButton.Checked = false;
                    showInDiagramCheckBox.Checked = true;
                    isArchivedCheckBox.Checked = false;
                }

                IsDirty = false;
            }
        }

        private bool _isDirty;
        private bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                EnableDisableControls();
            }
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IgnoreCategoryDropDownChange)
            {
                int categoryNo = (int)categoryComboBox.SelectedValue;

                if (categoryNo == 0)
                {
                    CurrentCategory = null;
                }
                else
                {
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        core.Connect();
                        CurrentCategory = core.GetCategory(categoryNo);
                    }
                }

                EnableDisableControls();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = System.Windows.Forms.DialogResult.Yes;

            if (IsDirty)
            {
                result = MessageBox.Show("Den valda kategorin är ändrad, men inte sparad. Om du stänger dialogrutan kommer dina ändringar gå förlorade. Vill du stänga dialogrutan?", CurrentApplication.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
        }

        private void parentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void typeIncomeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void typeExpenseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void isArchivedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private bool ValidateParentComboBox()
        {
            ComboBox ctrl = parentComboBox;

            if (ctrl.SelectedValue == null || (int)ctrl.SelectedValue == 0) {
                errorProvider.SetError(ctrl, "");
                return true;
            }

            if (CurrentCategory != null)
            {
                bool isCircleReference = (int) ctrl.SelectedValue == CurrentCategory.No;

                if (!isCircleReference && (int) parentComboBox.SelectedValue > 0)
                {
                    using (var core = new StandardBusinessLayer(DataCache))
                    {
                        core.Connect();

                        Category category = core.GetCategory((int) parentComboBox.SelectedValue);
                        while (category.ParentCategoryNo != null && (int) category.ParentCategoryNo > 0)
                        {
                            if (category.ParentCategoryNo != null &&
                                (int) category.ParentCategoryNo == CurrentCategory.No)
                            {
                                isCircleReference = true;
                                break;
                            }

                            category = core.GetCategory((int) category.ParentCategoryNo);
                        }
                    }
                }

                if (isCircleReference)
                {
                    errorProvider.SetError(ctrl,
                        "Nuvarande inställning får föräldrakategorin att orsaka en cirkelreferens.");
                }
                else
                {
                    errorProvider.SetError(ctrl, "");
                }
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateNameTextBox()
        {
            TextBox ctrl = nameTextBox;

            if (ctrl.Text.Trim() == "") {
                errorProvider.SetError(ctrl, "Fältet får inte vara tomt.");
            }
            else {
                errorProvider.SetError(ctrl, "");
            }

            return errorProvider.GetError(ctrl) == "";
        }

        private bool ValidateTypeRadioButtons()
        {
            RadioButton ctrl1 = typeIncomeRadioButton;
            RadioButton ctrl2 = typeExpenseRadioButton;

            if (!ctrl1.Checked && !ctrl2.Checked)
            {
                errorProvider.SetError(ctrl2, "En kategorityp måste väljas.");
            }
            else
            {
                errorProvider.SetError(ctrl2, "");
            }

            return errorProvider.GetError(ctrl2) == "";
        }

        private bool ValidateForm()
        {
            bool b1 = ValidateParentComboBox();
            bool b2 = ValidateNameTextBox();
            bool b3 = ValidateTypeRadioButtons();

            return b1 && b2 && b3;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (ValidateForm()) {
                using (var core = new StandardBusinessLayer(DataCache)) {
                    core.Connect();
                    Category category;

                    string name = nameTextBox.Text.Trim();
                    int? parentCategoryNo = null;
                    if (parentComboBox.SelectedValue != null && (int)parentComboBox.SelectedValue > 0) {
                        parentCategoryNo = (int)parentComboBox.SelectedValue;
                    }
                    CategoryType categoryType = typeExpenseRadioButton.Checked ? CategoryType.Expense : CategoryType.Income;
                    bool showInDiagram = showInDiagramCheckBox.Checked;
                    bool isArchived = isArchivedCheckBox.Checked;

                    if (NewCategory) {
                        category = new Category(parentCategoryNo, categoryType, name, isArchived, CurrentApplication.DateTimeNow, showInDiagram);
                    }
                    else {
                        category = core.GetCategory((int)categoryComboBox.SelectedValue);
                        category.Name = name;
                        category.ParentCategoryNo = parentCategoryNo;
                        category.Type = categoryType;
                        category.ShowInDiagram = showInDiagram;
                        category.IsArchived = isArchived;
                    }

                    core.Save(category);
                    NewCategory = false;
                    CurrentCategory = category;
                }

                ReloadForm();
            }
        }

        private bool IgnoreCategoryDropDownChange
        {
            get;
            set;
        }

        private bool IgnoreParentDropDownChange
        {
            get;
            set;
        }

        private void ReloadForm()
        {
            using (var core = new StandardBusinessLayer(DataCache))
            {
                core.Connect();

                int currentCategoryNo = CurrentCategory != null ? CurrentCategory.No : 0;

                IgnoreCategoryDropDownChange = true;

                var categories = core.GetCategoriesHierarchical();
                categoryComboBox.DataSource = null;
                categories.Insert(0, new DictionaryItem<int, string>() { Key = 0, Value = "*** Välj en kategori ***" });

                var archivedCategories = core.GetCategoriesHierarchicalArchived();
                if (archivedCategories.Count > 0)
                {
                    categories.Add(new DictionaryItem<int, string>() { Key = -1, Value = "*** Arkiverade kategorier ***" });
                    categories.AddRange(archivedCategories);
                }

                categoryComboBox.DisplayMember = "Value";
                categoryComboBox.ValueMember = "Key";
                categoryComboBox.DataSource = categories;
                categoryComboBox.SelectedValue = 0;

                var parents = core.GetCategoriesHierarchical();
                parentComboBox.DataSource = null;
                parents.Insert(0, new DictionaryItem<int, string>() { Key = 0, Value = "" });
                parentComboBox.DisplayMember = "Value";
                parentComboBox.ValueMember = "Key";
                parentComboBox.DataSource = parents;
                parentComboBox.SelectedValue = 0;

                CurrentCategory = core.GetCategory(currentCategoryNo);

                if (CurrentCategory != null)
                {
                    categoryComboBox.SelectedValue = CurrentCategory.No;
                    parentComboBox.SelectedValue = CurrentCategory.ParentCategoryNo != null ? CurrentCategory.ParentCategoryNo : 0;
                }

                IgnoreCategoryDropDownChange = false;
            }

            IsDirty = false;
        }

        private void deleteCategoryButton_Click(object sender, EventArgs e)
        {
            int categoryNo = (int)categoryComboBox.SelectedValue;
            bool categoryDeleted = false;

            // Category cannot be deleted if it has sub categories
            using (var core = new StandardBusinessLayer(DataCache)) {
                core.Connect();

                int subCategoriesCount = core.CountCategoriesByParentCategoryNo(categoryNo);

                if (subCategoriesCount > 0) {
                    MessageBox.Show("Kategorin kan inte tas bort eftersom den har underkategorier.", CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                int cashBookTransactionsCount = core.CountCashBookTransactionsByCategoryNo(categoryNo);
                if (cashBookTransactionsCount > 0) {
                    MessageBox.Show("Kategorin kan inte tas bort eftersom den har registrerade kassabokshändelser.", CurrentApplication.Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                Category category = core.GetCategory(categoryNo);

                DialogResult result = MessageBox.Show("Är du säker på att du vill ta bort kategorin '" + category.Name + "'?", CurrentApplication.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes) {
                    core.DeleteCategory(categoryNo);
                    categoryDeleted = true;
                }
            }

            if (categoryDeleted) {
                ReloadForm();
            }
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            if (IsDirty) {
                DialogResult result = MessageBox.Show("Den valda kategorin är ändrad. Vill du spara ändringarna innan du skapar en ny kategori?", CurrentApplication.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                if (result == DialogResult.Yes) {
                    Save();
                }
                else if (result == DialogResult.Cancel) {
                    return;
                }
            }

            categoryComboBox.SelectedValue = 0;
            NewCategory = true;
            nameTextBox.Text = "Ny kategori";
            nameTextBox.SelectionStart = 0;
            nameTextBox.SelectionLength = nameTextBox.Text.Length;
            nameTextBox.Focus();
            IsDirty = true;
        }

        private void showInDiagramCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }
    }
}
