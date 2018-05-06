using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBase.Classes
{
    [DatabaseTable("EasyBaseSystems")]
    public partial class EasyBaseSystem
    {
        public const string fNo = "No";
        public const string fDatabaseVersion = "DatabaseVersion";
        public const string fUpdatingToVersion = "UpdatingToVersion";

        internal EasyBaseSystem(EasyBaseSystemDataRow easyBaseSystemDataRow)
        {
            No = easyBaseSystemDataRow.No;
            DatabaseVersion = easyBaseSystemDataRow.DatabaseVersion;
            UpdatingToVersion = easyBaseSystemDataRow.UpdatingToVersion;
        }

        public EasyBaseSystem(EasyBaseSystemNo no, int databaseVersion, int updatingToVersion)
        {
            No = no;
            DatabaseVersion = databaseVersion;
            UpdatingToVersion = updatingToVersion;
        }

        [PrimaryKeyField("No", "int")]
        public EasyBaseSystemNo No
        {
            get;
            internal set;
        }

        [DatabaseField("DatabaseVersion", "int")]
        public int DatabaseVersion
        {
            get;
            set;
        }

        [DatabaseField("UpdatingToVersion", "int")]
        public int UpdatingToVersion
        {
            get;
            set;
        }

        public override string ToString()
        {
            System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod("OverridedToString");
            if (methodInfo != null) {
                return (string)methodInfo.Invoke(this, null);
            }
            else {
                return "No=" + No + ", " + "DatabaseVersion=" + DatabaseVersion + ", " + "UpdatingToVersion=" + UpdatingToVersion;
            }
        }
    }
}
