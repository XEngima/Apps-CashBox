using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
    public partial class StandardDataLayer
    {
        public User GetUser(int no)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users where No = @no";
            Connection.GetTable(userTable, sql, no);

            if (userTable.Rows.Count > 0) {
                return new User(userTable[0]);
            }

            return null;
        }

        public User GetUserByEmail(string email)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users where Email = @email";
            Connection.GetTable(userTable, sql, email);

            if (userTable.Rows.Count > 0) {
                return new User(userTable[0]);
            }

            return null;
        }

        public UserCollection GetUsersByName(string name)
        {
            return GetUsersByName(name, null);
        }

        public UserCollection GetUsersByEmail(string email)
        {
            return GetUsersByEmail(email, null);
        }

        public UserCollection GetUsersByPassword(string password)
        {
            return GetUsersByPassword(password, null);
        }

        public UserCollection GetUsersByCreatedTime(DateTime createdTime)
        {
            return GetUsersByCreatedTime(createdTime, null);
        }

        public UserCollection GetUsersByName(string name, SortOrder sortOrder)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users where Name = @name";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(userTable, sql, name);
            return UserCollection.CreateUserCollection(userTable);
        }

        public UserCollection GetUsersByEmail(string email, SortOrder sortOrder)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users where Email = @email";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(userTable, sql, email);
            return UserCollection.CreateUserCollection(userTable);
        }

        public UserCollection GetUsersByPassword(string password, SortOrder sortOrder)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users where Password = @password";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(userTable, sql, password);
            return UserCollection.CreateUserCollection(userTable);
        }

        public UserCollection GetUsersByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(userTable, sql, createdTime);
            return UserCollection.CreateUserCollection(userTable);
        }

        public UserCollection GetUsers()
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from Users";
            Connection.GetTable(userTable, sql);
            return UserCollection.CreateUserCollection(userTable);
        }
        public UserCollection GetUsers(Condition condition)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from [Users] where " + condition.ToSqlString();
            Connection.GetTable(userTable, sql);
            return UserCollection.CreateUserCollection(userTable);
        }
        public UserCollection GetUsers(SortOrder sortOrder)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from [Users] order by " + sortOrder.ToSqlString();
            Connection.GetTable(userTable, sql);
            return UserCollection.CreateUserCollection(userTable);
        }
        public UserCollection GetUsers(Condition condition, SortOrder sortOrder)
        {
            UserTable userTable = new UserTable();
            string sql = "select No, Name, Email, Password, CreatedTime from [Users] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(userTable, sql);
            return UserCollection.CreateUserCollection(userTable);
        }
        public DataTable GetUsersTable(params string[] sColumns)
        {
            UserTable userTable = new UserTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Users];");
            Connection.GetTable(userTable, sql.ToString());
            return userTable;
        }
        public DataTable GetUsersTable(Condition condition, params string[] sColumns)
        {
            UserTable userTable = new UserTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Users] where " + condition.ToSqlString());
            Connection.GetTable(userTable, sql.ToString());
            return userTable;
        }
        public DataTable GetUsersTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            UserTable userTable = new UserTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Users] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(userTable, sql.ToString());
            return userTable;
        }

        public int CountUsers()
        {
            string sql = "select count(*) from Users";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountUsersByNo(int no)
        {
            string sql = "select count(*) from Users where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountUsersByName(string name)
        {
            string sql = "select count(*) from Users where Name = @name";
            return Convert.ToInt32(Connection.GetScalar(sql, name));
        }

        public int CountUsersByEmail(string email)
        {
            string sql = "select count(*) from Users where Email = @email";
            return Convert.ToInt32(Connection.GetScalar(sql, email));
        }

        public int CountUsersByPassword(string password)
        {
            string sql = "select count(*) from Users where Password = @password";
            return Convert.ToInt32(Connection.GetScalar(sql, password));
        }

        public int CountUsersByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from Users where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountUsers(Condition condition)
        {
            string sql = "select count(*) from Users where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(User user)
        {
            if (user.No == 0) {
                string sql = "insert into [Users] (Name, Email, Password, CreatedTime) values (@name, @email, @password, @createdTime)";

                Connection.Execute(sql, user.Name, user.Email, user.Password, user.CreatedTime);
                user.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [Users] set Name=@name, Email=@email, Password=@password, CreatedTime=@createdTime where [No] = @no";

                int affectedRows = Connection.Execute(sql, user.Name, user.Email, user.Password, user.CreatedTime, user.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("User " + user.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteUser(int no)
        {
            string sql = "delete from Users where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllUsers()
        {
            string sql = "delete from Users";
            return Convert.ToInt32(Connection.Execute(sql));
        }


        public bool DeleteUserByEmail(string email)
        {
            string sql = "delete from Users where Email = @email";
            return Convert.ToInt32(Connection.Execute(sql, email)) == 1;
        }
        public int DeleteUsers(Condition condition)
        {
            string sql = "delete from Users where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public Category GetCategory(int no)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where No = @no";
            Connection.GetTable(categoryTable, sql, no);

            if (categoryTable.Rows.Count > 0) {
                return new Category(categoryTable[0]);
            }

            return null;
        }

        public CategoryCollection GetCategoriesByParentCategoryNo(int? parentCategoryNo)
        {
            return GetCategoriesByParentCategoryNo(parentCategoryNo, null);
        }

        public CategoryCollection GetCategoriesByType(CategoryType type)
        {
            return GetCategoriesByType(type, null);
        }

        public CategoryCollection GetCategoriesByName(string name)
        {
            return GetCategoriesByName(name, null);
        }

        public CategoryCollection GetCategoriesByIsArchived(bool isArchived)
        {
            return GetCategoriesByIsArchived(isArchived, null);
        }

        public CategoryCollection GetCategoriesByCreatedTime(DateTime createdTime)
        {
            return GetCategoriesByCreatedTime(createdTime, null);
        }

        public CategoryCollection GetCategoriesByShowInDiagram(bool showInDiagram)
        {
            return GetCategoriesByShowInDiagram(showInDiagram, null);
        }

        public CategoryCollection GetCategoriesByParentCategoryNo(int? parentCategoryNo, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where ParentCategoryNo = @parentCategoryNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(categoryTable, sql, parentCategoryNo);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

        public CategoryCollection GetCategoriesByType(CategoryType type, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where Type = @type";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(categoryTable, sql, type);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

        public CategoryCollection GetCategoriesByName(string name, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where Name = @name";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(categoryTable, sql, name);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

        public CategoryCollection GetCategoriesByIsArchived(bool isArchived, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where IsArchived = @isArchived";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(categoryTable, sql, isArchived);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

        public CategoryCollection GetCategoriesByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(categoryTable, sql, createdTime);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

        public CategoryCollection GetCategoriesByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories where ShowInDiagram = @showInDiagram";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(categoryTable, sql, showInDiagram);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }

        public CategoryCollection GetCategories()
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from Categories";
            Connection.GetTable(categoryTable, sql);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }
        public CategoryCollection GetCategories(Condition condition)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from [Categories] where " + condition.ToSqlString();
            Connection.GetTable(categoryTable, sql);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }
        public CategoryCollection GetCategories(SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from [Categories] order by " + sortOrder.ToSqlString();
            Connection.GetTable(categoryTable, sql);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }
        public CategoryCollection GetCategories(Condition condition, SortOrder sortOrder)
        {
            CategoryTable categoryTable = new CategoryTable();
            string sql = "select No, ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram from [Categories] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(categoryTable, sql);
            return CategoryCollection.CreateCategoryCollection(categoryTable);
        }
        public DataTable GetCategoriesTable(params string[] sColumns)
        {
            CategoryTable categoryTable = new CategoryTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Categories];");
            Connection.GetTable(categoryTable, sql.ToString());
            return categoryTable;
        }
        public DataTable GetCategoriesTable(Condition condition, params string[] sColumns)
        {
            CategoryTable categoryTable = new CategoryTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Categories] where " + condition.ToSqlString());
            Connection.GetTable(categoryTable, sql.ToString());
            return categoryTable;
        }
        public DataTable GetCategoriesTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            CategoryTable categoryTable = new CategoryTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Categories] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(categoryTable, sql.ToString());
            return categoryTable;
        }

        public int CountCategories()
        {
            string sql = "select count(*) from Categories";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountCategoriesByNo(int no)
        {
            string sql = "select count(*) from Categories where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountCategoriesByParentCategoryNo(int? parentCategoryNo)
        {
            string sql = "select count(*) from Categories where ParentCategoryNo = @parentCategoryNo";
            return Convert.ToInt32(Connection.GetScalar(sql, parentCategoryNo));
        }

        public int CountCategoriesByType(CategoryType type)
        {
            string sql = "select count(*) from Categories where Type = @type";
            return Convert.ToInt32(Connection.GetScalar(sql, type));
        }

        public int CountCategoriesByName(string name)
        {
            string sql = "select count(*) from Categories where Name = @name";
            return Convert.ToInt32(Connection.GetScalar(sql, name));
        }

        public int CountCategoriesByIsArchived(bool isArchived)
        {
            string sql = "select count(*) from Categories where IsArchived = @isArchived";
            return Convert.ToInt32(Connection.GetScalar(sql, isArchived));
        }

        public int CountCategoriesByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from Categories where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountCategoriesByShowInDiagram(bool showInDiagram)
        {
            string sql = "select count(*) from Categories where ShowInDiagram = @showInDiagram";
            return Convert.ToInt32(Connection.GetScalar(sql, showInDiagram));
        }

        public int CountCategories(Condition condition)
        {
            string sql = "select count(*) from Categories where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(Category category)
        {
            if (category.No == 0) {
                string sql = "insert into [Categories] (ParentCategoryNo, Type, Name, IsArchived, CreatedTime, ShowInDiagram) values (@parentCategoryNo, @type, @name, @isArchived, @createdTime, @showInDiagram)";

                Connection.Execute(sql, category.ParentCategoryNo, category.Type, category.Name, category.IsArchived, category.CreatedTime, category.ShowInDiagram);
                category.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [Categories] set ParentCategoryNo=@parentCategoryNo, Type=@type, Name=@name, IsArchived=@isArchived, CreatedTime=@createdTime, ShowInDiagram=@showInDiagram where [No] = @no";

                int affectedRows = Connection.Execute(sql, category.ParentCategoryNo, category.Type, category.Name, category.IsArchived, category.CreatedTime, category.ShowInDiagram, category.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("Category " + category.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteCategory(int no)
        {
            string sql = "delete from Categories where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllCategories()
        {
            string sql = "delete from Categories";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteCategoriesByParentCategoryNo(int? parentCategoryNo)
        {
            string sql = "delete from Categories where ParentCategoryNo = @parentCategoryNo";
            return Convert.ToInt32(Connection.Execute(sql, parentCategoryNo));
        }

        public int DeleteCategories(Condition condition)
        {
            string sql = "delete from Categories where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public Account GetAccount(int no)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where No = @no";
            Connection.GetTable(accountTable, sql, no);

            if (accountTable.Rows.Count > 0) {
                return new Account(accountTable[0]);
            }

            return null;
        }

        public AccountCollection GetAccountsByUserNo(int userNo)
        {
            return GetAccountsByUserNo(userNo, null);
        }

        public AccountCollection GetAccountsByType(AccountType type)
        {
            return GetAccountsByType(type, null);
        }

        public AccountCollection GetAccountsByName(string name)
        {
            return GetAccountsByName(name, null);
        }

        public AccountCollection GetAccountsByBalanceBroughtForwardAmount(decimal balanceBroughtForwardAmount)
        {
            return GetAccountsByBalanceBroughtForwardAmount(balanceBroughtForwardAmount, null);
        }

        public AccountCollection GetAccountsByIsArchived(bool isArchived)
        {
            return GetAccountsByIsArchived(isArchived, null);
        }

        public AccountCollection GetAccountsByCreatedTime(DateTime createdTime)
        {
            return GetAccountsByCreatedTime(createdTime, null);
        }

        public AccountCollection GetAccountsByShowInDiagram(bool showInDiagram)
        {
            return GetAccountsByShowInDiagram(showInDiagram, null);
        }

        public AccountCollection GetAccountsByBalance(decimal balance)
        {
            return GetAccountsByBalance(balance, null);
        }

        public AccountCollection GetAccountsByUserNo(int userNo, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where UserNo = @userNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, userNo);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByType(AccountType type, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where Type = @type";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, type);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByName(string name, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where Name = @name";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, name);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByBalanceBroughtForwardAmount(decimal balanceBroughtForwardAmount, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where BalanceBroughtForwardAmount = @balanceBroughtForwardAmount";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, balanceBroughtForwardAmount);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByIsArchived(bool isArchived, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where IsArchived = @isArchived";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, isArchived);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, createdTime);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where ShowInDiagram = @showInDiagram";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, showInDiagram);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccountsByBalance(decimal balance, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView where Balance = @balance";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTable, sql, balance);
            return AccountCollection.CreateAccountCollection(accountTable);
        }

        public AccountCollection GetAccounts()
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from AccountsView";
            Connection.GetTable(accountTable, sql);
            return AccountCollection.CreateAccountCollection(accountTable);
        }
        public AccountCollection GetAccounts(Condition condition)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from [AccountsView] where " + condition.ToSqlString();
            Connection.GetTable(accountTable, sql);
            return AccountCollection.CreateAccountCollection(accountTable);
        }
        public AccountCollection GetAccounts(SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from [AccountsView] order by " + sortOrder.ToSqlString();
            Connection.GetTable(accountTable, sql);
            return AccountCollection.CreateAccountCollection(accountTable);
        }
        public AccountCollection GetAccounts(Condition condition, SortOrder sortOrder)
        {
            AccountTable accountTable = new AccountTable();
            string sql = "select No, UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram, Balance from [AccountsView] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(accountTable, sql);
            return AccountCollection.CreateAccountCollection(accountTable);
        }
        public DataTable GetAccountsTable(params string[] sColumns)
        {
            AccountTable accountTable = new AccountTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountsView];");
            Connection.GetTable(accountTable, sql.ToString());
            return accountTable;
        }
        public DataTable GetAccountsTable(Condition condition, params string[] sColumns)
        {
            AccountTable accountTable = new AccountTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountsView] where " + condition.ToSqlString());
            Connection.GetTable(accountTable, sql.ToString());
            return accountTable;
        }
        public DataTable GetAccountsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            AccountTable accountTable = new AccountTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountsView] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(accountTable, sql.ToString());
            return accountTable;
        }

        public int CountAccounts()
        {
            string sql = "select count(*) from AccountsView";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountAccountsByNo(int no)
        {
            string sql = "select count(*) from AccountsView where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountAccountsByUserNo(int userNo)
        {
            string sql = "select count(*) from AccountsView where UserNo = @userNo";
            return Convert.ToInt32(Connection.GetScalar(sql, userNo));
        }

        public int CountAccountsByType(AccountType type)
        {
            string sql = "select count(*) from AccountsView where Type = @type";
            return Convert.ToInt32(Connection.GetScalar(sql, type));
        }

        public int CountAccountsByName(string name)
        {
            string sql = "select count(*) from AccountsView where Name = @name";
            return Convert.ToInt32(Connection.GetScalar(sql, name));
        }

        public int CountAccountsByBalanceBroughtForwardAmount(decimal balanceBroughtForwardAmount)
        {
            string sql = "select count(*) from AccountsView where BalanceBroughtForwardAmount = @balanceBroughtForwardAmount";
            return Convert.ToInt32(Connection.GetScalar(sql, balanceBroughtForwardAmount));
        }

        public int CountAccountsByIsArchived(bool isArchived)
        {
            string sql = "select count(*) from AccountsView where IsArchived = @isArchived";
            return Convert.ToInt32(Connection.GetScalar(sql, isArchived));
        }

        public int CountAccountsByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from AccountsView where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountAccountsByShowInDiagram(bool showInDiagram)
        {
            string sql = "select count(*) from AccountsView where ShowInDiagram = @showInDiagram";
            return Convert.ToInt32(Connection.GetScalar(sql, showInDiagram));
        }

        public int CountAccountsByBalance(decimal balance)
        {
            string sql = "select count(*) from AccountsView where Balance = @balance";
            return Convert.ToInt32(Connection.GetScalar(sql, balance));
        }

        public int CountAccounts(Condition condition)
        {
            string sql = "select count(*) from AccountsView where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(Account account)
        {
            if (account.No == 0) {
                string sql = "insert into [Accounts] (UserNo, Type, Name, BalanceBroughtForwardAmount, IsArchived, CreatedTime, ShowInDiagram) values (@userNo, @type, @name, @balanceBroughtForwardAmount, @isArchived, @createdTime, @showInDiagram)";

                Connection.Execute(sql, account.UserNo, account.Type, account.Name, account.BalanceBroughtForwardAmount, account.IsArchived, account.CreatedTime, account.ShowInDiagram);
                account.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [Accounts] set UserNo=@userNo, Type=@type, Name=@name, BalanceBroughtForwardAmount=@balanceBroughtForwardAmount, IsArchived=@isArchived, CreatedTime=@createdTime, ShowInDiagram=@showInDiagram where [No] = @no";

                int affectedRows = Connection.Execute(sql, account.UserNo, account.Type, account.Name, account.BalanceBroughtForwardAmount, account.IsArchived, account.CreatedTime, account.ShowInDiagram, account.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("Account " + account.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteAccount(int no)
        {
            string sql = "delete from Accounts where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllAccounts()
        {
            string sql = "delete from Accounts";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteAccountsByUserNo(int userNo)
        {
            string sql = "delete from Accounts where UserNo = @userNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo));
        }

        public int DeleteAccounts(Condition condition)
        {
            string sql = "delete from Accounts where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public TagAccount GetTagAccount(int no)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView where No = @no";
            Connection.GetTable(tagAccountTable, sql, no);

            if (tagAccountTable.Rows.Count > 0) {
                return new TagAccount(tagAccountTable[0]);
            }

            return null;
        }

        public TagAccount GetTagAccountByName(string name)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView where Name = @name";
            Connection.GetTable(tagAccountTable, sql, name);

            if (tagAccountTable.Rows.Count > 0) {
                return new TagAccount(tagAccountTable[0]);
            }

            return null;
        }

        public TagAccountCollection GetTagAccountsByName(string name)
        {
            return GetTagAccountsByName(name, null);
        }

        public TagAccountCollection GetTagAccountsByIsArchived(bool isArchived)
        {
            return GetTagAccountsByIsArchived(isArchived, null);
        }

        public TagAccountCollection GetTagAccountsByCreatedTime(DateTime createdTime)
        {
            return GetTagAccountsByCreatedTime(createdTime, null);
        }

        public TagAccountCollection GetTagAccountsByAmount(decimal amount)
        {
            return GetTagAccountsByAmount(amount, null);
        }

        public TagAccountCollection GetTagAccountsByName(string name, SortOrder sortOrder)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView where Name = @name";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountTable, sql, name);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }

        public TagAccountCollection GetTagAccountsByIsArchived(bool isArchived, SortOrder sortOrder)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView where IsArchived = @isArchived";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountTable, sql, isArchived);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }

        public TagAccountCollection GetTagAccountsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountTable, sql, createdTime);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }

        public TagAccountCollection GetTagAccountsByAmount(decimal amount, SortOrder sortOrder)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView where Amount = @amount";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountTable, sql, amount);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }

        public TagAccountCollection GetTagAccounts()
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from TagAccountsView";
            Connection.GetTable(tagAccountTable, sql);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }
        public TagAccountCollection GetTagAccounts(Condition condition)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from [TagAccountsView] where " + condition.ToSqlString();
            Connection.GetTable(tagAccountTable, sql);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }
        public TagAccountCollection GetTagAccounts(SortOrder sortOrder)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from [TagAccountsView] order by " + sortOrder.ToSqlString();
            Connection.GetTable(tagAccountTable, sql);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }
        public TagAccountCollection GetTagAccounts(Condition condition, SortOrder sortOrder)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            string sql = "select No, Name, IsArchived, CreatedTime, Amount from [TagAccountsView] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(tagAccountTable, sql);
            return TagAccountCollection.CreateTagAccountCollection(tagAccountTable);
        }
        public DataTable GetTagAccountsTable(params string[] sColumns)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [TagAccountsView];");
            Connection.GetTable(tagAccountTable, sql.ToString());
            return tagAccountTable;
        }
        public DataTable GetTagAccountsTable(Condition condition, params string[] sColumns)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [TagAccountsView] where " + condition.ToSqlString());
            Connection.GetTable(tagAccountTable, sql.ToString());
            return tagAccountTable;
        }
        public DataTable GetTagAccountsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            TagAccountTable tagAccountTable = new TagAccountTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [TagAccountsView] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(tagAccountTable, sql.ToString());
            return tagAccountTable;
        }

        public int CountTagAccounts()
        {
            string sql = "select count(*) from TagAccountsView";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountTagAccountsByNo(int no)
        {
            string sql = "select count(*) from TagAccountsView where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountTagAccountsByName(string name)
        {
            string sql = "select count(*) from TagAccountsView where Name = @name";
            return Convert.ToInt32(Connection.GetScalar(sql, name));
        }

        public int CountTagAccountsByIsArchived(bool isArchived)
        {
            string sql = "select count(*) from TagAccountsView where IsArchived = @isArchived";
            return Convert.ToInt32(Connection.GetScalar(sql, isArchived));
        }

        public int CountTagAccountsByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from TagAccountsView where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountTagAccountsByAmount(decimal amount)
        {
            string sql = "select count(*) from TagAccountsView where Amount = @amount";
            return Convert.ToInt32(Connection.GetScalar(sql, amount));
        }

        public int CountTagAccounts(Condition condition)
        {
            string sql = "select count(*) from TagAccountsView where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(TagAccount tagAccount)
        {
            if (tagAccount.No == 0) {
                string sql = "insert into [TagAccounts] (Name, IsArchived, CreatedTime) values (@name, @isArchived, @createdTime)";

                Connection.Execute(sql, tagAccount.Name, tagAccount.IsArchived, tagAccount.CreatedTime);
                tagAccount.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [TagAccounts] set Name=@name, IsArchived=@isArchived, CreatedTime=@createdTime where [No] = @no";

                int affectedRows = Connection.Execute(sql, tagAccount.Name, tagAccount.IsArchived, tagAccount.CreatedTime, tagAccount.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("TagAccount " + tagAccount.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteTagAccount(int no)
        {
            string sql = "delete from TagAccounts where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllTagAccounts()
        {
            string sql = "delete from TagAccounts";
            return Convert.ToInt32(Connection.Execute(sql));
        }


        public bool DeleteTagAccountByName(string name)
        {
            string sql = "delete from TagAccounts where Name = @name";
            return Convert.ToInt32(Connection.Execute(sql, name)) == 1;
        }
        public int DeleteTagAccounts(Condition condition)
        {
            string sql = "delete from TagAccounts where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public AccountTag GetAccountTag(int no)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where No = @no";
            Connection.GetTable(accountTagTable, sql, no);

            if (accountTagTable.Rows.Count > 0) {
                return new AccountTag(accountTagTable[0]);
            }

            return null;
        }

        public AccountTagCollection GetAccountTagsByTagAccountNoAndAccountNo(int tagAccountNo, int accountNo)
        {
            return GetAccountTagsByTagAccountNoAndAccountNo(tagAccountNo, accountNo, null);
        }

        public AccountTagCollection GetAccountTagsByTagAccountNoAndAccountNo(int tagAccountNo, int accountNo, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where TagAccountNo = @tagAccountNo and AccountNo = @accountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, tagAccountNo, accountNo);

            if (accountTagTable.Rows.Count > 0) {
                return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
            }

            return new AccountTagCollection();
        }

        public AccountTagCollection GetAccountTagsByTagAccountNo(int tagAccountNo)
        {
            return GetAccountTagsByTagAccountNo(tagAccountNo, null);
        }

        public AccountTagCollection GetAccountTagsByAccountNo(int accountNo)
        {
            return GetAccountTagsByAccountNo(accountNo, null);
        }

        public AccountTagCollection GetAccountTagsByType(AccountTagType type)
        {
            return GetAccountTagsByType(type, null);
        }

        public AccountTagCollection GetAccountTagsByMoneyValue(decimal moneyValue)
        {
            return GetAccountTagsByMoneyValue(moneyValue, null);
        }

        public AccountTagCollection GetAccountTagsByRelativeValue(decimal relativeValue)
        {
            return GetAccountTagsByRelativeValue(relativeValue, null);
        }

        public AccountTagCollection GetAccountTagsByTagAccountName(string tagAccountName)
        {
            return GetAccountTagsByTagAccountName(tagAccountName, null);
        }

        public AccountTagCollection GetAccountTagsByAmount(decimal amount)
        {
            return GetAccountTagsByAmount(amount, null);
        }

        public AccountTagCollection GetAccountTagsByTagAccountNo(int tagAccountNo, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where TagAccountNo = @tagAccountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, tagAccountNo);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTagsByAccountNo(int accountNo, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where AccountNo = @accountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, accountNo);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTagsByType(AccountTagType type, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where Type = @type";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, type);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTagsByMoneyValue(decimal moneyValue, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where MoneyValue = @moneyValue";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, moneyValue);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTagsByRelativeValue(decimal relativeValue, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where RelativeValue = @relativeValue";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, relativeValue);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTagsByTagAccountName(string tagAccountName, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where TagAccountName = @tagAccountName";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, tagAccountName);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTagsByAmount(decimal amount, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView where Amount = @amount";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTagTable, sql, amount);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }

        public AccountTagCollection GetAccountTags()
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from AccountTagsView";
            Connection.GetTable(accountTagTable, sql);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }
        public AccountTagCollection GetAccountTags(Condition condition)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from [AccountTagsView] where " + condition.ToSqlString();
            Connection.GetTable(accountTagTable, sql);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }
        public AccountTagCollection GetAccountTags(SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from [AccountTagsView] order by " + sortOrder.ToSqlString();
            Connection.GetTable(accountTagTable, sql);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }
        public AccountTagCollection GetAccountTags(Condition condition, SortOrder sortOrder)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            string sql = "select No, TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue, TagAccountName, Amount from [AccountTagsView] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(accountTagTable, sql);
            return AccountTagCollection.CreateAccountTagCollection(accountTagTable);
        }
        public DataTable GetAccountTagsTable(params string[] sColumns)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountTagsView];");
            Connection.GetTable(accountTagTable, sql.ToString());
            return accountTagTable;
        }
        public DataTable GetAccountTagsTable(Condition condition, params string[] sColumns)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountTagsView] where " + condition.ToSqlString());
            Connection.GetTable(accountTagTable, sql.ToString());
            return accountTagTable;
        }
        public DataTable GetAccountTagsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            AccountTagTable accountTagTable = new AccountTagTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountTagsView] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(accountTagTable, sql.ToString());
            return accountTagTable;
        }

        public int CountAccountTags()
        {
            string sql = "select count(*) from AccountTagsView";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountAccountTagsByNo(int no)
        {
            string sql = "select count(*) from AccountTagsView where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountAccountTagsByTagAccountNo(int tagAccountNo)
        {
            string sql = "select count(*) from AccountTagsView where TagAccountNo = @tagAccountNo";
            return Convert.ToInt32(Connection.GetScalar(sql, tagAccountNo));
        }

        public int CountAccountTagsByAccountNo(int accountNo)
        {
            string sql = "select count(*) from AccountTagsView where AccountNo = @accountNo";
            return Convert.ToInt32(Connection.GetScalar(sql, accountNo));
        }

        public int CountAccountTagsByType(AccountTagType type)
        {
            string sql = "select count(*) from AccountTagsView where Type = @type";
            return Convert.ToInt32(Connection.GetScalar(sql, type));
        }

        public int CountAccountTagsByMoneyValue(decimal moneyValue)
        {
            string sql = "select count(*) from AccountTagsView where MoneyValue = @moneyValue";
            return Convert.ToInt32(Connection.GetScalar(sql, moneyValue));
        }

        public int CountAccountTagsByRelativeValue(decimal relativeValue)
        {
            string sql = "select count(*) from AccountTagsView where RelativeValue = @relativeValue";
            return Convert.ToInt32(Connection.GetScalar(sql, relativeValue));
        }

        public int CountAccountTagsByTagAccountName(string tagAccountName)
        {
            string sql = "select count(*) from AccountTagsView where TagAccountName = @tagAccountName";
            return Convert.ToInt32(Connection.GetScalar(sql, tagAccountName));
        }

        public int CountAccountTagsByAmount(decimal amount)
        {
            string sql = "select count(*) from AccountTagsView where Amount = @amount";
            return Convert.ToInt32(Connection.GetScalar(sql, amount));
        }

        public int CountAccountTags(Condition condition)
        {
            string sql = "select count(*) from AccountTagsView where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(AccountTag accountTag)
        {
            if (accountTag.No == 0) {
                string sql = "insert into [AccountTags] (TagAccountNo, AccountNo, Type, MoneyValue, RelativeValue) values (@tagAccountNo, @accountNo, @type, @moneyValue, @relativeValue)";

                Connection.Execute(sql, accountTag.TagAccountNo, accountTag.AccountNo, accountTag.Type, accountTag.MoneyValue, accountTag.RelativeValue);
                accountTag.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [AccountTags] set TagAccountNo=@tagAccountNo, AccountNo=@accountNo, Type=@type, MoneyValue=@moneyValue, RelativeValue=@relativeValue where [No] = @no";

                int affectedRows = Connection.Execute(sql, accountTag.TagAccountNo, accountTag.AccountNo, accountTag.Type, accountTag.MoneyValue, accountTag.RelativeValue, accountTag.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("AccountTag " + accountTag.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteAccountTag(int no)
        {
            string sql = "delete from AccountTags where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllAccountTags()
        {
            string sql = "delete from AccountTags";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteAccountTagsByTagAccountNo(int tagAccountNo)
        {
            string sql = "delete from AccountTags where TagAccountNo = @tagAccountNo";
            return Convert.ToInt32(Connection.Execute(sql, tagAccountNo));
        }

        public int DeleteAccountTagsByAccountNo(int accountNo)
        {
            string sql = "delete from AccountTags where AccountNo = @accountNo";
            return Convert.ToInt32(Connection.Execute(sql, accountNo));
        }


        public bool DeleteAccountTagsByTagAccountNoAndAccountNo(int tagAccountNo, int accountNo)
        {
            string sql = "delete from AccountTags where TagAccountNo = @tagAccountNo and AccountNo = @accountNo";
            return Convert.ToInt32(Connection.Execute(sql, tagAccountNo, accountNo)) == 1;
        }
        public int DeleteAccountTags(Condition condition)
        {
            string sql = "delete from AccountTags where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public Verification GetVerification(int no)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications where No = @no";
            Connection.GetTable(verificationTable, sql, no);

            if (verificationTable.Rows.Count > 0) {
                return new Verification(verificationTable[0]);
            }

            return null;
        }

        public Verification GetVerificationByYearAndSerialNo(int year, int serialNo)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications where Year = @year and SerialNo = @serialNo";
            Connection.GetTable(verificationTable, sql, year, serialNo);

            if (verificationTable.Rows.Count > 0) {
                return new Verification(verificationTable[0]);
            }

            return null;
        }

        public VerificationCollection GetVerificationsByYear(int year)
        {
            return GetVerificationsByYear(year, null);
        }

        public VerificationCollection GetVerificationsBySerialNo(int serialNo)
        {
            return GetVerificationsBySerialNo(serialNo, null);
        }

        public VerificationCollection GetVerificationsByDate(DateTime date)
        {
            return GetVerificationsByDate(date, null);
        }

        public VerificationCollection GetVerificationsByAccountingDate(DateTime accountingDate)
        {
            return GetVerificationsByAccountingDate(accountingDate, null);
        }

        public VerificationCollection GetVerificationsByYear(int year, SortOrder sortOrder)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications where Year = @year";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(verificationTable, sql, year);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }

        public VerificationCollection GetVerificationsBySerialNo(int serialNo, SortOrder sortOrder)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications where SerialNo = @serialNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(verificationTable, sql, serialNo);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }

        public VerificationCollection GetVerificationsByDate(DateTime date, SortOrder sortOrder)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications where Date = @date";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(verificationTable, sql, date);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }

        public VerificationCollection GetVerificationsByAccountingDate(DateTime accountingDate, SortOrder sortOrder)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications where AccountingDate = @accountingDate";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(verificationTable, sql, accountingDate);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }

        public VerificationCollection GetVerifications()
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from Verifications";
            Connection.GetTable(verificationTable, sql);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }
        public VerificationCollection GetVerifications(Condition condition)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from [Verifications] where " + condition.ToSqlString();
            Connection.GetTable(verificationTable, sql);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }
        public VerificationCollection GetVerifications(SortOrder sortOrder)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from [Verifications] order by " + sortOrder.ToSqlString();
            Connection.GetTable(verificationTable, sql);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }
        public VerificationCollection GetVerifications(Condition condition, SortOrder sortOrder)
        {
            VerificationTable verificationTable = new VerificationTable();
            string sql = "select No, Year, SerialNo, Date, AccountingDate from [Verifications] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(verificationTable, sql);
            return VerificationCollection.CreateVerificationCollection(verificationTable);
        }
        public DataTable GetVerificationsTable(params string[] sColumns)
        {
            VerificationTable verificationTable = new VerificationTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Verifications];");
            Connection.GetTable(verificationTable, sql.ToString());
            return verificationTable;
        }
        public DataTable GetVerificationsTable(Condition condition, params string[] sColumns)
        {
            VerificationTable verificationTable = new VerificationTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Verifications] where " + condition.ToSqlString());
            Connection.GetTable(verificationTable, sql.ToString());
            return verificationTable;
        }
        public DataTable GetVerificationsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            VerificationTable verificationTable = new VerificationTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [Verifications] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(verificationTable, sql.ToString());
            return verificationTable;
        }

        public int CountVerifications()
        {
            string sql = "select count(*) from Verifications";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountVerificationsByNo(int no)
        {
            string sql = "select count(*) from Verifications where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountVerificationsByYear(int year)
        {
            string sql = "select count(*) from Verifications where Year = @year";
            return Convert.ToInt32(Connection.GetScalar(sql, year));
        }

        public int CountVerificationsBySerialNo(int serialNo)
        {
            string sql = "select count(*) from Verifications where SerialNo = @serialNo";
            return Convert.ToInt32(Connection.GetScalar(sql, serialNo));
        }

        public int CountVerificationsByDate(DateTime date)
        {
            string sql = "select count(*) from Verifications where Date = @date";
            return Convert.ToInt32(Connection.GetScalar(sql, date));
        }

        public int CountVerificationsByAccountingDate(DateTime accountingDate)
        {
            string sql = "select count(*) from Verifications where AccountingDate = @accountingDate";
            return Convert.ToInt32(Connection.GetScalar(sql, accountingDate));
        }

        public int CountVerifications(Condition condition)
        {
            string sql = "select count(*) from Verifications where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(Verification verification)
        {
            if (verification.No == 0) {
                string sql = "insert into [Verifications] (Year, SerialNo, Date, AccountingDate) values (@year, @serialNo, @date, @accountingDate)";

                Connection.Execute(sql, verification.Year, verification.SerialNo, verification.Date, verification.AccountingDate);
                verification.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [Verifications] set Year=@year, SerialNo=@serialNo, Date=@date, AccountingDate=@accountingDate where [No] = @no";

                int affectedRows = Connection.Execute(sql, verification.Year, verification.SerialNo, verification.Date, verification.AccountingDate, verification.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("Verification " + verification.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteVerification(int no)
        {
            string sql = "delete from Verifications where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllVerifications()
        {
            string sql = "delete from Verifications";
            return Convert.ToInt32(Connection.Execute(sql));
        }


        public bool DeleteVerificationByYearAndSerialNo(int year, int serialNo)
        {
            string sql = "delete from Verifications where Year = @year and SerialNo = @serialNo";
            return Convert.ToInt32(Connection.Execute(sql, year, serialNo)) == 1;
        }
        public int DeleteVerifications(Condition condition)
        {
            string sql = "delete from Verifications where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public AccountTransaction GetAccountTransaction(int no)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where No = @no";
            Connection.GetTable(accountTransactionTable, sql, no);

            if (accountTransactionTable.Rows.Count > 0) {
                return new AccountTransaction(accountTransactionTable[0]);
            }

            return null;
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(int userNo, int accountNo, int verificationNo)
        {
            return GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(userNo, accountNo, verificationNo, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNoAndAccountNoAndVerificationNo(int userNo, int accountNo, int verificationNo, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where UserNo = @userNo and AccountNo = @accountNo and VerificationNo = @verificationNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, userNo, accountNo, verificationNo);

            if (accountTransactionTable.Rows.Count > 0) {
                return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
            }

            return new AccountTransactionCollection();
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNo(int userNo)
        {
            return GetAccountTransactionsByUserNo(userNo, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountNo(int accountNo)
        {
            return GetAccountTransactionsByAccountNo(accountNo, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationNo(int verificationNo)
        {
            return GetAccountTransactionsByVerificationNo(verificationNo, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByAmount(decimal amount)
        {
            return GetAccountTransactionsByAmount(amount, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByNote(string note)
        {
            return GetAccountTransactionsByNote(note, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByCreatedTime(DateTime createdTime)
        {
            return GetAccountTransactionsByCreatedTime(createdTime, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            return GetAccountTransactionsByVerificationSerialNo(verificationSerialNo, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByTransactionTime(DateTime transactionTime)
        {
            return GetAccountTransactionsByTransactionTime(transactionTime, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountingDate(DateTime accountingDate)
        {
            return GetAccountTransactionsByAccountingDate(accountingDate, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountName(string accountName)
        {
            return GetAccountTransactionsByAccountName(accountName, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserName(string userName)
        {
            return GetAccountTransactionsByUserName(userName, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByShowInDiagram(bool showInDiagram)
        {
            return GetAccountTransactionsByShowInDiagram(showInDiagram, null);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserNo(int userNo, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where UserNo = @userNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, userNo);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountNo(int accountNo, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where AccountNo = @accountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, accountNo);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationNo(int verificationNo, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where VerificationNo = @verificationNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, verificationNo);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByAmount(decimal amount, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where Amount = @amount";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, amount);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByNote(string note, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where Note = @note";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, note);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, createdTime);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByVerificationSerialNo(int verificationSerialNo, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where VerificationSerialNo = @verificationSerialNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, verificationSerialNo);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByTransactionTime(DateTime transactionTime, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where TransactionTime = @transactionTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, transactionTime);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountingDate(DateTime accountingDate, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where AccountingDate = @accountingDate";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, accountingDate);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByAccountName(string accountName, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where AccountName = @accountName";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, accountName);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByUserName(string userName, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where UserName = @userName";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, userName);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactionsByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView where ShowInDiagram = @showInDiagram";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(accountTransactionTable, sql, showInDiagram);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }

        public AccountTransactionCollection GetAccountTransactions()
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from AccountTransactionsView";
            Connection.GetTable(accountTransactionTable, sql);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }
        public AccountTransactionCollection GetAccountTransactions(Condition condition)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from [AccountTransactionsView] where " + condition.ToSqlString();
            Connection.GetTable(accountTransactionTable, sql);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }
        public AccountTransactionCollection GetAccountTransactions(SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from [AccountTransactionsView] order by " + sortOrder.ToSqlString();
            Connection.GetTable(accountTransactionTable, sql);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }
        public AccountTransactionCollection GetAccountTransactions(Condition condition, SortOrder sortOrder)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            string sql = "select No, UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, AccountName, UserName, ShowInDiagram from [AccountTransactionsView] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(accountTransactionTable, sql);
            return AccountTransactionCollection.CreateAccountTransactionCollection(accountTransactionTable);
        }
        public DataTable GetAccountTransactionsTable(params string[] sColumns)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountTransactionsView];");
            Connection.GetTable(accountTransactionTable, sql.ToString());
            return accountTransactionTable;
        }
        public DataTable GetAccountTransactionsTable(Condition condition, params string[] sColumns)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountTransactionsView] where " + condition.ToSqlString());
            Connection.GetTable(accountTransactionTable, sql.ToString());
            return accountTransactionTable;
        }
        public DataTable GetAccountTransactionsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            AccountTransactionTable accountTransactionTable = new AccountTransactionTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [AccountTransactionsView] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(accountTransactionTable, sql.ToString());
            return accountTransactionTable;
        }

        public int CountAccountTransactions()
        {
            string sql = "select count(*) from AccountTransactionsView";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountAccountTransactionsByNo(int no)
        {
            string sql = "select count(*) from AccountTransactionsView where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountAccountTransactionsByUserNo(int userNo)
        {
            string sql = "select count(*) from AccountTransactionsView where UserNo = @userNo";
            return Convert.ToInt32(Connection.GetScalar(sql, userNo));
        }

        public int CountAccountTransactionsByAccountNo(int accountNo)
        {
            string sql = "select count(*) from AccountTransactionsView where AccountNo = @accountNo";
            return Convert.ToInt32(Connection.GetScalar(sql, accountNo));
        }

        public int CountAccountTransactionsByVerificationNo(int verificationNo)
        {
            string sql = "select count(*) from AccountTransactionsView where VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.GetScalar(sql, verificationNo));
        }

        public int CountAccountTransactionsByAmount(decimal amount)
        {
            string sql = "select count(*) from AccountTransactionsView where Amount = @amount";
            return Convert.ToInt32(Connection.GetScalar(sql, amount));
        }

        public int CountAccountTransactionsByNote(string note)
        {
            string sql = "select count(*) from AccountTransactionsView where Note = @note";
            return Convert.ToInt32(Connection.GetScalar(sql, note));
        }

        public int CountAccountTransactionsByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from AccountTransactionsView where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountAccountTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            string sql = "select count(*) from AccountTransactionsView where VerificationSerialNo = @verificationSerialNo";
            return Convert.ToInt32(Connection.GetScalar(sql, verificationSerialNo));
        }

        public int CountAccountTransactionsByTransactionTime(DateTime transactionTime)
        {
            string sql = "select count(*) from AccountTransactionsView where TransactionTime = @transactionTime";
            return Convert.ToInt32(Connection.GetScalar(sql, transactionTime));
        }

        public int CountAccountTransactionsByAccountingDate(DateTime accountingDate)
        {
            string sql = "select count(*) from AccountTransactionsView where AccountingDate = @accountingDate";
            return Convert.ToInt32(Connection.GetScalar(sql, accountingDate));
        }

        public int CountAccountTransactionsByAccountName(string accountName)
        {
            string sql = "select count(*) from AccountTransactionsView where AccountName = @accountName";
            return Convert.ToInt32(Connection.GetScalar(sql, accountName));
        }

        public int CountAccountTransactionsByUserName(string userName)
        {
            string sql = "select count(*) from AccountTransactionsView where UserName = @userName";
            return Convert.ToInt32(Connection.GetScalar(sql, userName));
        }

        public int CountAccountTransactionsByShowInDiagram(bool showInDiagram)
        {
            string sql = "select count(*) from AccountTransactionsView where ShowInDiagram = @showInDiagram";
            return Convert.ToInt32(Connection.GetScalar(sql, showInDiagram));
        }

        public int CountAccountTransactions(Condition condition)
        {
            string sql = "select count(*) from AccountTransactionsView where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(AccountTransaction accountTransaction)
        {
            if (accountTransaction.No == 0) {
                string sql = "insert into [AccountTransactions] (UserNo, AccountNo, VerificationNo, Amount, Note, CreatedTime) values (@userNo, @accountNo, @verificationNo, @amount, @note, @createdTime)";

                Connection.Execute(sql, accountTransaction.UserNo, accountTransaction.AccountNo, accountTransaction.VerificationNo, accountTransaction.Amount, accountTransaction.Note, accountTransaction.CreatedTime);
                accountTransaction.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [AccountTransactions] set UserNo=@userNo, AccountNo=@accountNo, VerificationNo=@verificationNo, Amount=@amount, Note=@note, CreatedTime=@createdTime where [No] = @no";

                int affectedRows = Connection.Execute(sql, accountTransaction.UserNo, accountTransaction.AccountNo, accountTransaction.VerificationNo, accountTransaction.Amount, accountTransaction.Note, accountTransaction.CreatedTime, accountTransaction.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("AccountTransaction " + accountTransaction.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteAccountTransaction(int no)
        {
            string sql = "delete from AccountTransactions where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllAccountTransactions()
        {
            string sql = "delete from AccountTransactions";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteAccountTransactionsByUserNo(int userNo)
        {
            string sql = "delete from AccountTransactions where UserNo = @userNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo));
        }

        public int DeleteAccountTransactionsByAccountNo(int accountNo)
        {
            string sql = "delete from AccountTransactions where AccountNo = @accountNo";
            return Convert.ToInt32(Connection.Execute(sql, accountNo));
        }

        public int DeleteAccountTransactionsByVerificationNo(int verificationNo)
        {
            string sql = "delete from AccountTransactions where VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.Execute(sql, verificationNo));
        }


        public bool DeleteAccountTransactionsByUserNoAndAccountNoAndVerificationNo(int userNo, int accountNo, int verificationNo)
        {
            string sql = "delete from AccountTransactions where UserNo = @userNo and AccountNo = @accountNo and VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo, accountNo, verificationNo)) == 1;
        }
        public int DeleteAccountTransactions(Condition condition)
        {
            string sql = "delete from AccountTransactions where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public TagAccountSnapshot GetTagAccountSnapshot(int no)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where No = @no";
            Connection.GetTable(tagAccountSnapshotTable, sql, no);

            if (tagAccountSnapshotTable.Rows.Count > 0) {
                return new TagAccountSnapshot(tagAccountSnapshotTable[0]);
            }

            return null;
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNoAndTagAccountNo(int userNo, int tagAccountNo)
        {
            return GetTagAccountSnapshotsByUserNoAndTagAccountNo(userNo, tagAccountNo, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNoAndTagAccountNo(int userNo, int tagAccountNo, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where UserNo = @userNo and TagAccountNo = @tagAccountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, userNo, tagAccountNo);

            if (tagAccountSnapshotTable.Rows.Count > 0) {
                return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
            }

            return new TagAccountSnapshotCollection();
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNo(int userNo)
        {
            return GetTagAccountSnapshotsByUserNo(userNo, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByTagAccountNo(int tagAccountNo)
        {
            return GetTagAccountSnapshotsByTagAccountNo(tagAccountNo, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByBalanceAmount(decimal balanceAmount)
        {
            return GetTagAccountSnapshotsByBalanceAmount(balanceAmount, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByReason(TagAccountSnapshotReason reason)
        {
            return GetTagAccountSnapshotsByReason(reason, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByCreatedTime(DateTime createdTime)
        {
            return GetTagAccountSnapshotsByCreatedTime(createdTime, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByAccountName(string accountName)
        {
            return GetTagAccountSnapshotsByAccountName(accountName, null);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByUserNo(int userNo, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where UserNo = @userNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, userNo);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByTagAccountNo(int tagAccountNo, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where TagAccountNo = @tagAccountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, tagAccountNo);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByBalanceAmount(decimal balanceAmount, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where BalanceAmount = @balanceAmount";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, balanceAmount);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByReason(TagAccountSnapshotReason reason, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where Reason = @reason";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, reason);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, createdTime);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshotsByAccountName(string accountName, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView where AccountName = @accountName";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(tagAccountSnapshotTable, sql, accountName);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }

        public TagAccountSnapshotCollection GetTagAccountSnapshots()
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from TagAccountSnapshotsView";
            Connection.GetTable(tagAccountSnapshotTable, sql);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }
        public TagAccountSnapshotCollection GetTagAccountSnapshots(Condition condition)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from [TagAccountSnapshotsView] where " + condition.ToSqlString();
            Connection.GetTable(tagAccountSnapshotTable, sql);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }
        public TagAccountSnapshotCollection GetTagAccountSnapshots(SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from [TagAccountSnapshotsView] order by " + sortOrder.ToSqlString();
            Connection.GetTable(tagAccountSnapshotTable, sql);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }
        public TagAccountSnapshotCollection GetTagAccountSnapshots(Condition condition, SortOrder sortOrder)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            string sql = "select No, UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime, AccountName from [TagAccountSnapshotsView] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(tagAccountSnapshotTable, sql);
            return TagAccountSnapshotCollection.CreateTagAccountSnapshotCollection(tagAccountSnapshotTable);
        }
        public DataTable GetTagAccountSnapshotsTable(params string[] sColumns)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [TagAccountSnapshotsView];");
            Connection.GetTable(tagAccountSnapshotTable, sql.ToString());
            return tagAccountSnapshotTable;
        }
        public DataTable GetTagAccountSnapshotsTable(Condition condition, params string[] sColumns)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [TagAccountSnapshotsView] where " + condition.ToSqlString());
            Connection.GetTable(tagAccountSnapshotTable, sql.ToString());
            return tagAccountSnapshotTable;
        }
        public DataTable GetTagAccountSnapshotsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            TagAccountSnapshotTable tagAccountSnapshotTable = new TagAccountSnapshotTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [TagAccountSnapshotsView] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(tagAccountSnapshotTable, sql.ToString());
            return tagAccountSnapshotTable;
        }

        public int CountTagAccountSnapshots()
        {
            string sql = "select count(*) from TagAccountSnapshotsView";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountTagAccountSnapshotsByNo(int no)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountTagAccountSnapshotsByUserNo(int userNo)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where UserNo = @userNo";
            return Convert.ToInt32(Connection.GetScalar(sql, userNo));
        }

        public int CountTagAccountSnapshotsByTagAccountNo(int tagAccountNo)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where TagAccountNo = @tagAccountNo";
            return Convert.ToInt32(Connection.GetScalar(sql, tagAccountNo));
        }

        public int CountTagAccountSnapshotsByBalanceAmount(decimal balanceAmount)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where BalanceAmount = @balanceAmount";
            return Convert.ToInt32(Connection.GetScalar(sql, balanceAmount));
        }

        public int CountTagAccountSnapshotsByReason(TagAccountSnapshotReason reason)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where Reason = @reason";
            return Convert.ToInt32(Connection.GetScalar(sql, reason));
        }

        public int CountTagAccountSnapshotsByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountTagAccountSnapshotsByAccountName(string accountName)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where AccountName = @accountName";
            return Convert.ToInt32(Connection.GetScalar(sql, accountName));
        }

        public int CountTagAccountSnapshots(Condition condition)
        {
            string sql = "select count(*) from TagAccountSnapshotsView where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(TagAccountSnapshot tagAccountSnapshot)
        {
            if (tagAccountSnapshot.No == 0) {
                string sql = "insert into [TagAccountSnapshots] (UserNo, TagAccountNo, BalanceAmount, Reason, CreatedTime) values (@userNo, @tagAccountNo, @balanceAmount, @reason, @createdTime)";

                Connection.Execute(sql, tagAccountSnapshot.UserNo, tagAccountSnapshot.TagAccountNo, tagAccountSnapshot.BalanceAmount, tagAccountSnapshot.Reason, tagAccountSnapshot.CreatedTime);
                tagAccountSnapshot.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [TagAccountSnapshots] set UserNo=@userNo, TagAccountNo=@tagAccountNo, BalanceAmount=@balanceAmount, Reason=@reason, CreatedTime=@createdTime where [No] = @no";

                int affectedRows = Connection.Execute(sql, tagAccountSnapshot.UserNo, tagAccountSnapshot.TagAccountNo, tagAccountSnapshot.BalanceAmount, tagAccountSnapshot.Reason, tagAccountSnapshot.CreatedTime, tagAccountSnapshot.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("TagAccountSnapshot " + tagAccountSnapshot.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteTagAccountSnapshot(int no)
        {
            string sql = "delete from TagAccountSnapshots where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllTagAccountSnapshots()
        {
            string sql = "delete from TagAccountSnapshots";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteTagAccountSnapshotsByUserNo(int userNo)
        {
            string sql = "delete from TagAccountSnapshots where UserNo = @userNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo));
        }

        public int DeleteTagAccountSnapshotsByTagAccountNo(int tagAccountNo)
        {
            string sql = "delete from TagAccountSnapshots where TagAccountNo = @tagAccountNo";
            return Convert.ToInt32(Connection.Execute(sql, tagAccountNo));
        }


        public bool DeleteTagAccountSnapshotsByUserNoAndTagAccountNo(int userNo, int tagAccountNo)
        {
            string sql = "delete from TagAccountSnapshots where UserNo = @userNo and TagAccountNo = @tagAccountNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo, tagAccountNo)) == 1;
        }
        public int DeleteTagAccountSnapshots(Condition condition)
        {
            string sql = "delete from TagAccountSnapshots where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public CashBookTransaction GetCashBookTransaction(int no)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where No = @no";
            Connection.GetTable(cashBookTransactionTable, sql, no);

            if (cashBookTransactionTable.Rows.Count > 0) {
                return new CashBookTransaction(cashBookTransactionTable[0]);
            }

            return null;
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(int userNo, int categoryNo, int verificationNo)
        {
            return GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(userNo, categoryNo, verificationNo, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(int userNo, int categoryNo, int verificationNo, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where UserNo = @userNo and CategoryNo = @categoryNo and VerificationNo = @verificationNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, userNo, categoryNo, verificationNo);

            if (cashBookTransactionTable.Rows.Count > 0) {
                return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
            }

            return new CashBookTransactionCollection();
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNo(int userNo)
        {
            return GetCashBookTransactionsByUserNo(userNo, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryNo(int categoryNo)
        {
            return GetCashBookTransactionsByCategoryNo(categoryNo, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationNo(int verificationNo)
        {
            return GetCashBookTransactionsByVerificationNo(verificationNo, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAmount(decimal amount)
        {
            return GetCashBookTransactionsByAmount(amount, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByNote(string note)
        {
            return GetCashBookTransactionsByNote(note, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCreatedTime(DateTime createdTime)
        {
            return GetCashBookTransactionsByCreatedTime(createdTime, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            return GetCashBookTransactionsByVerificationSerialNo(verificationSerialNo, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByTransactionTime(DateTime transactionTime)
        {
            return GetCashBookTransactionsByTransactionTime(transactionTime, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAccountingDate(DateTime accountingDate)
        {
            return GetCashBookTransactionsByAccountingDate(accountingDate, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryName(string categoryName)
        {
            return GetCashBookTransactionsByCategoryName(categoryName, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByShowInDiagram(bool showInDiagram)
        {
            return GetCashBookTransactionsByShowInDiagram(showInDiagram, null);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByUserNo(int userNo, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where UserNo = @userNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, userNo);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryNo(int categoryNo, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where CategoryNo = @categoryNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, categoryNo);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationNo(int verificationNo, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where VerificationNo = @verificationNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, verificationNo);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAmount(decimal amount, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where Amount = @amount";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, amount);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByNote(string note, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where Note = @note";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, note);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCreatedTime(DateTime createdTime, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where CreatedTime = @createdTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, createdTime);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByVerificationSerialNo(int verificationSerialNo, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where VerificationSerialNo = @verificationSerialNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, verificationSerialNo);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByTransactionTime(DateTime transactionTime, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where TransactionTime = @transactionTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, transactionTime);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByAccountingDate(DateTime accountingDate, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where AccountingDate = @accountingDate";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, accountingDate);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByCategoryName(string categoryName, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where CategoryName = @categoryName";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, categoryName);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactionsByShowInDiagram(bool showInDiagram, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView where ShowInDiagram = @showInDiagram";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBookTransactionTable, sql, showInDiagram);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }

        public CashBookTransactionCollection GetCashBookTransactions()
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from CashBookTransactionsView";
            Connection.GetTable(cashBookTransactionTable, sql);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }
        public CashBookTransactionCollection GetCashBookTransactions(Condition condition)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from [CashBookTransactionsView] where " + condition.ToSqlString();
            Connection.GetTable(cashBookTransactionTable, sql);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }
        public CashBookTransactionCollection GetCashBookTransactions(SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from [CashBookTransactionsView] order by " + sortOrder.ToSqlString();
            Connection.GetTable(cashBookTransactionTable, sql);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }
        public CashBookTransactionCollection GetCashBookTransactions(Condition condition, SortOrder sortOrder)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            string sql = "select No, UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime, VerificationSerialNo, TransactionTime, AccountingDate, CategoryName, ShowInDiagram from [CashBookTransactionsView] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(cashBookTransactionTable, sql);
            return CashBookTransactionCollection.CreateCashBookTransactionCollection(cashBookTransactionTable);
        }
        public DataTable GetCashBookTransactionsTable(params string[] sColumns)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [CashBookTransactionsView];");
            Connection.GetTable(cashBookTransactionTable, sql.ToString());
            return cashBookTransactionTable;
        }
        public DataTable GetCashBookTransactionsTable(Condition condition, params string[] sColumns)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [CashBookTransactionsView] where " + condition.ToSqlString());
            Connection.GetTable(cashBookTransactionTable, sql.ToString());
            return cashBookTransactionTable;
        }
        public DataTable GetCashBookTransactionsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            CashBookTransactionTable cashBookTransactionTable = new CashBookTransactionTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [CashBookTransactionsView] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(cashBookTransactionTable, sql.ToString());
            return cashBookTransactionTable;
        }

        public int CountCashBookTransactions()
        {
            string sql = "select count(*) from CashBookTransactionsView";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountCashBookTransactionsByNo(int no)
        {
            string sql = "select count(*) from CashBookTransactionsView where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountCashBookTransactionsByUserNo(int userNo)
        {
            string sql = "select count(*) from CashBookTransactionsView where UserNo = @userNo";
            return Convert.ToInt32(Connection.GetScalar(sql, userNo));
        }

        public int CountCashBookTransactionsByCategoryNo(int categoryNo)
        {
            string sql = "select count(*) from CashBookTransactionsView where CategoryNo = @categoryNo";
            return Convert.ToInt32(Connection.GetScalar(sql, categoryNo));
        }

        public int CountCashBookTransactionsByVerificationNo(int verificationNo)
        {
            string sql = "select count(*) from CashBookTransactionsView where VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.GetScalar(sql, verificationNo));
        }

        public int CountCashBookTransactionsByAmount(decimal amount)
        {
            string sql = "select count(*) from CashBookTransactionsView where Amount = @amount";
            return Convert.ToInt32(Connection.GetScalar(sql, amount));
        }

        public int CountCashBookTransactionsByNote(string note)
        {
            string sql = "select count(*) from CashBookTransactionsView where Note = @note";
            return Convert.ToInt32(Connection.GetScalar(sql, note));
        }

        public int CountCashBookTransactionsByCreatedTime(DateTime createdTime)
        {
            string sql = "select count(*) from CashBookTransactionsView where CreatedTime = @createdTime";
            return Convert.ToInt32(Connection.GetScalar(sql, createdTime));
        }

        public int CountCashBookTransactionsByVerificationSerialNo(int verificationSerialNo)
        {
            string sql = "select count(*) from CashBookTransactionsView where VerificationSerialNo = @verificationSerialNo";
            return Convert.ToInt32(Connection.GetScalar(sql, verificationSerialNo));
        }

        public int CountCashBookTransactionsByTransactionTime(DateTime transactionTime)
        {
            string sql = "select count(*) from CashBookTransactionsView where TransactionTime = @transactionTime";
            return Convert.ToInt32(Connection.GetScalar(sql, transactionTime));
        }

        public int CountCashBookTransactionsByAccountingDate(DateTime accountingDate)
        {
            string sql = "select count(*) from CashBookTransactionsView where AccountingDate = @accountingDate";
            return Convert.ToInt32(Connection.GetScalar(sql, accountingDate));
        }

        public int CountCashBookTransactionsByCategoryName(string categoryName)
        {
            string sql = "select count(*) from CashBookTransactionsView where CategoryName = @categoryName";
            return Convert.ToInt32(Connection.GetScalar(sql, categoryName));
        }

        public int CountCashBookTransactionsByShowInDiagram(bool showInDiagram)
        {
            string sql = "select count(*) from CashBookTransactionsView where ShowInDiagram = @showInDiagram";
            return Convert.ToInt32(Connection.GetScalar(sql, showInDiagram));
        }

        public int CountCashBookTransactions(Condition condition)
        {
            string sql = "select count(*) from CashBookTransactionsView where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(CashBookTransaction cashBookTransaction)
        {
            if (cashBookTransaction.No == 0) {
                string sql = "insert into [CashBookTransactions] (UserNo, CategoryNo, VerificationNo, Amount, Note, CreatedTime) values (@userNo, @categoryNo, @verificationNo, @amount, @note, @createdTime)";

                Connection.Execute(sql, cashBookTransaction.UserNo, cashBookTransaction.CategoryNo, cashBookTransaction.VerificationNo, cashBookTransaction.Amount, cashBookTransaction.Note, cashBookTransaction.CreatedTime);
                cashBookTransaction.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [CashBookTransactions] set UserNo=@userNo, CategoryNo=@categoryNo, VerificationNo=@verificationNo, Amount=@amount, Note=@note, CreatedTime=@createdTime where [No] = @no";

                int affectedRows = Connection.Execute(sql, cashBookTransaction.UserNo, cashBookTransaction.CategoryNo, cashBookTransaction.VerificationNo, cashBookTransaction.Amount, cashBookTransaction.Note, cashBookTransaction.CreatedTime, cashBookTransaction.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("CashBookTransaction " + cashBookTransaction.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteCashBookTransaction(int no)
        {
            string sql = "delete from CashBookTransactions where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllCashBookTransactions()
        {
            string sql = "delete from CashBookTransactions";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteCashBookTransactionsByUserNo(int userNo)
        {
            string sql = "delete from CashBookTransactions where UserNo = @userNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo));
        }

        public int DeleteCashBookTransactionsByCategoryNo(int categoryNo)
        {
            string sql = "delete from CashBookTransactions where CategoryNo = @categoryNo";
            return Convert.ToInt32(Connection.Execute(sql, categoryNo));
        }

        public int DeleteCashBookTransactionsByVerificationNo(int verificationNo)
        {
            string sql = "delete from CashBookTransactions where VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.Execute(sql, verificationNo));
        }


        public bool DeleteCashBookTransactionsByUserNoAndCategoryNoAndVerificationNo(int userNo, int categoryNo, int verificationNo)
        {
            string sql = "delete from CashBookTransactions where UserNo = @userNo and CategoryNo = @categoryNo and VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo, categoryNo, verificationNo)) == 1;
        }
        public int DeleteCashBookTransactions(Condition condition)
        {
            string sql = "delete from CashBookTransactions where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public LogItem GetLogItem(int no)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where No = @no";
            Connection.GetTable(logItemTable, sql, no);

            if (logItemTable.Rows.Count > 0) {
                return new LogItem(logItemTable[0]);
            }

            return null;
        }

        public LogItemCollection GetLogItemsByUserNoAndVerificationNoAndAccountNo(int? userNo, int? verificationNo, int? accountNo)
        {
            return GetLogItemsByUserNoAndVerificationNoAndAccountNo(userNo, verificationNo, accountNo, null);
        }

        public LogItemCollection GetLogItemsByUserNoAndVerificationNoAndAccountNo(int? userNo, int? verificationNo, int? accountNo, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where UserNo = @userNo and VerificationNo = @verificationNo and AccountNo = @accountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, userNo, verificationNo, accountNo);

            if (logItemTable.Rows.Count > 0) {
                return LogItemCollection.CreateLogItemCollection(logItemTable);
            }

            return new LogItemCollection();
        }

        public LogItemCollection GetLogItemsByUserNo(int? userNo)
        {
            return GetLogItemsByUserNo(userNo, null);
        }

        public LogItemCollection GetLogItemsByVerificationNo(int? verificationNo)
        {
            return GetLogItemsByVerificationNo(verificationNo, null);
        }

        public LogItemCollection GetLogItemsByAccountNo(int? accountNo)
        {
            return GetLogItemsByAccountNo(accountNo, null);
        }

        public LogItemCollection GetLogItemsByType(LogItemType type)
        {
            return GetLogItemsByType(type, null);
        }

        public LogItemCollection GetLogItemsByDescription(string description)
        {
            return GetLogItemsByDescription(description, null);
        }

        public LogItemCollection GetLogItemsByLogTime(DateTime logTime)
        {
            return GetLogItemsByLogTime(logTime, null);
        }

        public LogItemCollection GetLogItemsByUserNo(int? userNo, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where UserNo = @userNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, userNo);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }

        public LogItemCollection GetLogItemsByVerificationNo(int? verificationNo, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where VerificationNo = @verificationNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, verificationNo);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }

        public LogItemCollection GetLogItemsByAccountNo(int? accountNo, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where AccountNo = @accountNo";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, accountNo);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }

        public LogItemCollection GetLogItemsByType(LogItemType type, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where Type = @type";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, type);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }

        public LogItemCollection GetLogItemsByDescription(string description, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where Description = @description";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, description);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }

        public LogItemCollection GetLogItemsByLogTime(DateTime logTime, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems where LogTime = @logTime";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(logItemTable, sql, logTime);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }

        public LogItemCollection GetLogItems()
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from LogItems";
            Connection.GetTable(logItemTable, sql);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }
        public LogItemCollection GetLogItems(Condition condition)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from [LogItems] where " + condition.ToSqlString();
            Connection.GetTable(logItemTable, sql);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }
        public LogItemCollection GetLogItems(SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from [LogItems] order by " + sortOrder.ToSqlString();
            Connection.GetTable(logItemTable, sql);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }
        public LogItemCollection GetLogItems(Condition condition, SortOrder sortOrder)
        {
            LogItemTable logItemTable = new LogItemTable();
            string sql = "select No, UserNo, VerificationNo, AccountNo, Type, Description, LogTime from [LogItems] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(logItemTable, sql);
            return LogItemCollection.CreateLogItemCollection(logItemTable);
        }
        public DataTable GetLogItemsTable(params string[] sColumns)
        {
            LogItemTable logItemTable = new LogItemTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [LogItems];");
            Connection.GetTable(logItemTable, sql.ToString());
            return logItemTable;
        }
        public DataTable GetLogItemsTable(Condition condition, params string[] sColumns)
        {
            LogItemTable logItemTable = new LogItemTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [LogItems] where " + condition.ToSqlString());
            Connection.GetTable(logItemTable, sql.ToString());
            return logItemTable;
        }
        public DataTable GetLogItemsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            LogItemTable logItemTable = new LogItemTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [LogItems] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(logItemTable, sql.ToString());
            return logItemTable;
        }

        public int CountLogItems()
        {
            string sql = "select count(*) from LogItems";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountLogItemsByNo(int no)
        {
            string sql = "select count(*) from LogItems where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountLogItemsByUserNo(int? userNo)
        {
            string sql = "select count(*) from LogItems where UserNo = @userNo";
            return Convert.ToInt32(Connection.GetScalar(sql, userNo));
        }

        public int CountLogItemsByVerificationNo(int? verificationNo)
        {
            string sql = "select count(*) from LogItems where VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.GetScalar(sql, verificationNo));
        }

        public int CountLogItemsByAccountNo(int? accountNo)
        {
            string sql = "select count(*) from LogItems where AccountNo = @accountNo";
            return Convert.ToInt32(Connection.GetScalar(sql, accountNo));
        }

        public int CountLogItemsByType(LogItemType type)
        {
            string sql = "select count(*) from LogItems where Type = @type";
            return Convert.ToInt32(Connection.GetScalar(sql, type));
        }

        public int CountLogItemsByDescription(string description)
        {
            string sql = "select count(*) from LogItems where Description = @description";
            return Convert.ToInt32(Connection.GetScalar(sql, description));
        }

        public int CountLogItemsByLogTime(DateTime logTime)
        {
            string sql = "select count(*) from LogItems where LogTime = @logTime";
            return Convert.ToInt32(Connection.GetScalar(sql, logTime));
        }

        public int CountLogItems(Condition condition)
        {
            string sql = "select count(*) from LogItems where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(LogItem logItem)
        {
            if (logItem.No == 0) {
                string sql = "insert into [LogItems] (UserNo, VerificationNo, AccountNo, Type, Description, LogTime) values (@userNo, @verificationNo, @accountNo, @type, @description, @logTime)";

                Connection.Execute(sql, logItem.UserNo, logItem.VerificationNo, logItem.AccountNo, logItem.Type, logItem.Description, logItem.LogTime);
                logItem.No = Convert.ToInt32 (Connection.GetScalar("select @@identity"));
            }
            else {
                string sql = "update [LogItems] set UserNo=@userNo, VerificationNo=@verificationNo, AccountNo=@accountNo, Type=@type, Description=@description, LogTime=@logTime where [No] = @no";

                int affectedRows = Connection.Execute(sql, logItem.UserNo, logItem.VerificationNo, logItem.AccountNo, logItem.Type, logItem.Description, logItem.LogTime, logItem.No);
                if (affectedRows == 0) {
                    throw new DatabaseEntityDoesNotExistException("LogItem " + logItem.No + " does not exist in the database.");
                }
            }
        }

        public bool DeleteLogItem(int no)
        {
            string sql = "delete from LogItems where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllLogItems()
        {
            string sql = "delete from LogItems";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteLogItemsByUserNo(int? userNo)
        {
            string sql = "delete from LogItems where UserNo = @userNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo));
        }

        public int DeleteLogItemsByVerificationNo(int? verificationNo)
        {
            string sql = "delete from LogItems where VerificationNo = @verificationNo";
            return Convert.ToInt32(Connection.Execute(sql, verificationNo));
        }

        public int DeleteLogItemsByAccountNo(int? accountNo)
        {
            string sql = "delete from LogItems where AccountNo = @accountNo";
            return Convert.ToInt32(Connection.Execute(sql, accountNo));
        }


        public bool DeleteLogItemsByUserNoAndVerificationNoAndAccountNo(int? userNo, int? verificationNo, int? accountNo)
        {
            string sql = "delete from LogItems where UserNo = @userNo and VerificationNo = @verificationNo and AccountNo = @accountNo";
            return Convert.ToInt32(Connection.Execute(sql, userNo, verificationNo, accountNo)) == 1;
        }
        public int DeleteLogItems(Condition condition)
        {
            string sql = "delete from LogItems where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public CashBoxSettings GetCashBoxSettings(CashBoxSettingsNo no)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            string sql = "select No, AccountingYear from CashBoxSettings where No = @no";
            Connection.GetTable(cashBoxSettingsTable, sql, no);

            if (cashBoxSettingsTable.Rows.Count > 0) {
                return new CashBoxSettings(cashBoxSettingsTable[0]);
            }

            return null;
        }

        public CashBoxSettingsCollection GetCashBoxSettingsByAccountingYear(int accountingYear)
        {
            return GetCashBoxSettingsByAccountingYear(accountingYear, null);
        }

        public CashBoxSettingsCollection GetCashBoxSettingsByAccountingYear(int accountingYear, SortOrder sortOrder)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            string sql = "select No, AccountingYear from CashBoxSettings where AccountingYear = @accountingYear";

            if (sortOrder != null) {
                sql += " order by " + sortOrder.ToSqlString();
            }

            Connection.GetTable(cashBoxSettingsTable, sql, accountingYear);
            return CashBoxSettingsCollection.CreateCashBoxSettingsCollection(cashBoxSettingsTable);
        }

        public CashBoxSettingsCollection GetCashBoxSettings()
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            string sql = "select No, AccountingYear from CashBoxSettings";
            Connection.GetTable(cashBoxSettingsTable, sql);
            return CashBoxSettingsCollection.CreateCashBoxSettingsCollection(cashBoxSettingsTable);
        }
        public CashBoxSettingsCollection GetCashBoxSettings(Condition condition)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            string sql = "select No, AccountingYear from [CashBoxSettings] where " + condition.ToSqlString();
            Connection.GetTable(cashBoxSettingsTable, sql);
            return CashBoxSettingsCollection.CreateCashBoxSettingsCollection(cashBoxSettingsTable);
        }
        public CashBoxSettingsCollection GetCashBoxSettings(SortOrder sortOrder)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            string sql = "select No, AccountingYear from [CashBoxSettings] order by " + sortOrder.ToSqlString();
            Connection.GetTable(cashBoxSettingsTable, sql);
            return CashBoxSettingsCollection.CreateCashBoxSettingsCollection(cashBoxSettingsTable);
        }
        public CashBoxSettingsCollection GetCashBoxSettings(Condition condition, SortOrder sortOrder)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            string sql = "select No, AccountingYear from [CashBoxSettings] where " + condition.ToSqlString() + " order by " + sortOrder.ToSqlString();
            Connection.GetTable(cashBoxSettingsTable, sql);
            return CashBoxSettingsCollection.CreateCashBoxSettingsCollection(cashBoxSettingsTable);
        }
        public DataTable GetCashBoxSettingsTable(params string[] sColumns)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [CashBoxSettings];");
            Connection.GetTable(cashBoxSettingsTable, sql.ToString());
            return cashBoxSettingsTable;
        }
        public DataTable GetCashBoxSettingsTable(Condition condition, params string[] sColumns)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [CashBoxSettings] where " + condition.ToSqlString());
            Connection.GetTable(cashBoxSettingsTable, sql.ToString());
            return cashBoxSettingsTable;
        }
        public DataTable GetCashBoxSettingsTable(Condition condition, SortOrder sortOrder, params string[] sColumns)
        {
            CashBoxSettingsTable cashBoxSettingsTable = new CashBoxSettingsTable();
            StringBuilder sql = new StringBuilder("select ");

            if (sColumns.Length > 0) {
                string comma = "";
                foreach (string sColumn in sColumns) {
                    sql.Append(comma);
                    sql.Append(sColumn);
                    comma = ",";
                }
            }
            else {
                sql.Append("*");
            }

            sql.Append(" from [CashBoxSettings] where " + condition.ToSqlString());
            sql.Append(" order by " + sortOrder.ToSqlString());
            Connection.GetTable(cashBoxSettingsTable, sql.ToString());
            return cashBoxSettingsTable;
        }

        public int CountCashBoxSettings()
        {
            string sql = "select count(*) from CashBoxSettings";
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public int CountCashBoxSettingsByNo(CashBoxSettingsNo no)
        {
            string sql = "select count(*) from CashBoxSettings where No = @no";
            return Convert.ToInt32(Connection.GetScalar(sql, no));
        }

        public int CountCashBoxSettingsByAccountingYear(int accountingYear)
        {
            string sql = "select count(*) from CashBoxSettings where AccountingYear = @accountingYear";
            return Convert.ToInt32(Connection.GetScalar(sql, accountingYear));
        }

        public int CountCashBoxSettings(Condition condition)
        {
            string sql = "select count(*) from CashBoxSettings where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.GetScalar(sql));
        }

        public void Save(CashBoxSettings cashBoxSettings)
        {
            string sql = "update [CashBoxSettings] set AccountingYear=@accountingYear where [No] = @no";

            int affectedRows = Connection.Execute(sql, cashBoxSettings.AccountingYear, cashBoxSettings.No);

            if (affectedRows == 0) {
                sql = "insert into [CashBoxSettings] (No, AccountingYear) values (@no, @accountingYear)";
                Connection.Execute(sql, cashBoxSettings.No, cashBoxSettings.AccountingYear);
            }
        }

        public bool DeleteCashBoxSettings(CashBoxSettingsNo no)
        {
            string sql = "delete from CashBoxSettings where No = @no";
            return Convert.ToInt32(Connection.Execute(sql, no)) > 0;
        }

        public int DeleteAllCashBoxSettings()
        {
            string sql = "delete from CashBoxSettings";
            return Convert.ToInt32(Connection.Execute(sql));
        }

        public int DeleteCashBoxSettings(Condition condition)
        {
            string sql = "delete from CashBoxSettings where " + condition.ToSqlString();
            return Convert.ToInt32(Connection.Execute(sql));
        }
    }
}
