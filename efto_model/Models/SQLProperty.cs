using efto_model.Models.Enums;

namespace efto_model.Models
{
    public class SQLProperty
    {
        public string Name { get; set; }
        public SQLPropertyTypes Type { get; set; }
        public SQLPropertyNotations Notation { get; set; }
        public (string table, string property)? ForeignKeyReference { get; set; }

        public SQLProperty(string name, SQLPropertyTypes type, SQLPropertyNotations notation, (string name, string property) foreignKeyReference) : this(name, type, notation) => this.ForeignKeyReference = foreignKeyReference;

        public SQLProperty(string name, SQLPropertyTypes type, SQLPropertyNotations notation)
        {
            this.Name = name;
            this.Type = type;
            this.Notation = notation;
        }

        public SQLProperty() { }
    }
}
