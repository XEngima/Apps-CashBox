using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EasyBase.Classes
{
    public class EasyBaseSystemDataRow : DataRow
    {
        internal EasyBaseSystemDataRow(DataRowBuilder builder)
            : base(builder)
        {
        }

        public EasyBaseSystemNo No
        {
            get { return (EasyBaseSystemNo)this["No"]; }
        }

        public int DatabaseVersion
        {
            get { return (int)this["DatabaseVersion"]; }
        }

        public int UpdatingToVersion
        {
            get { return (int)this["UpdatingToVersion"]; }
        }
    }

    public class EasyBaseSystemTable : DataTable
    {
        protected override Type GetRowType()
        {
            return typeof(EasyBaseSystemDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new EasyBaseSystemDataRow(builder);
        }

        public new EasyBaseSystemDataRow NewRow()
        {
            return (EasyBaseSystemDataRow)base.NewRow();
        }

        public EasyBaseSystemDataRow this[int index]
        {
            get { return (EasyBaseSystemDataRow)Rows[index]; }
        }
    }
}
