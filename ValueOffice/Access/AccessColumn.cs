using System;

namespace ValueOffice.Access
{
    public class AccessColumn
    {
        public String Name { get; set; }

        public ColumnType Type { get; set; }

        public Boolean PrimaryKey { get; set; }

        public override string ToString()
        {
            String primaryKey = PrimaryKey ? "primary key" : String.Empty;

            return String.Concat("[", this.Name, "]", " ", this.Type, " ", primaryKey);
        }
    }
}
