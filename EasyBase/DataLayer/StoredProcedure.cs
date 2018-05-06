using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.DataLayer.Kernel
{
    internal abstract class StoredProcedure
    {
        public abstract string SqlString
        {
            get;
        }

        public SqlUpdateCommand CreateStoredProcedureSql
        {
            get {
                List<String> sqls = new List<string>();

                sqls.Add("SET ANSI_NULLS ON");
                sqls.Add("SET QUOTED_IDENTIFIER ON");

                sqls.Add(string.Format(@"
                    create procedure {0}
                    as
                    begin
                        set nocount on;
                        {1};
                    end", System.Reflection.Assembly.GetAssembly(this.GetType()).GetName(), SqlString));

                return new SqlUpdateCommand(sqls.ToArray());
            }
        }

        public override string ToString()
        {
            return SqlString;
        }
    }
}
