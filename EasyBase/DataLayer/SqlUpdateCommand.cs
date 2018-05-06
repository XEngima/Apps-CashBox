using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBase.Classes;
using System.Data.Common;

namespace EasyBase.DataLayer
{
    public class SqlUpdateCommand
    {
        public enum CommandType
        {
            SqlString,
            Function
        }

        public SqlUpdateCommand(string sqlCommand)
        {
            Type = CommandType.SqlString;

            List<string> sqlCommands = new List<string>();
            sqlCommands.Add(sqlCommand);
            SqlCommands = sqlCommands.ToArray();
        }

        public SqlUpdateCommand(string[] sqlCommands)
        {
            Type = CommandType.SqlString;
            SqlCommands = sqlCommands;
        }

        public SqlUpdateCommand(UpdateFunction updateFunction)
        {
            Type = CommandType.Function;
            Function = updateFunction;
        }

        public CommandType Type
        {
            get;
            private set;
        }

        public string[] SqlCommands
        {
            get;
            private set;
        }

        public void DoUpdate(EasyBaseSqlConnection connection)
        {
            if (Type == CommandType.SqlString) {
                foreach (string sql in SqlCommands) {
                    connection.Execute(sql);
                }
            }
            else {
                // Run the update function
                Function(connection);
            }
        }

        public delegate void UpdateFunction(EasyBaseSqlConnection connection);

        private UpdateFunction Function
        {
            get;
            set;
        }
    }
}
