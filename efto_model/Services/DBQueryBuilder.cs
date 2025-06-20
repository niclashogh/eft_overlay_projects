using efto_model.Models;
using efto_model.Models.Enums;
using System.Reflection;

namespace efto_model.Services
{
    public static class DBQueryBuilder
    {
        public static string CreateTable(List<SQLProperty> properties, string tableName)
        {
            List<string> columnDefinitions = new();
            List<string> foreignKeys = new();

            foreach (SQLProperty property in properties)
            {
                string type = property.Type switch
                {
                    SQLPropertyTypes.VARCHAR => "VARCHAR(200)",
                    SQLPropertyTypes.nVARCHAR => "nVARCHAR(200)",
                    _ => property.Type.ToString()
                };

                string notation = property.Notation switch
                {
                    SQLPropertyNotations.PrimaryKey => " PRIMARY KEY AUTOINCREMENT",
                    SQLPropertyNotations.NotNull => " NOT NULL",
                    _ => "" // Nullable & ForeignKey has not end-notations
                };

                columnDefinitions.Add($"{property.Name} {type}{notation}"); //Note: Missing space between {type} and {notation} is correct

                if (property.ForeignKeyReference.HasValue && property.Notation == SQLPropertyNotations.ForeignKey)
                {
                    var foreignKey = property.ForeignKeyReference.Value;
                    foreignKeys.Add($"FOREIGN KEY ({property.Name}) REFERENCES {foreignKey.table}({foreignKey.property})");
                }
                else throw new Exception("DBQueryBuilder.CreateTable: Syntax error in ForeignKey");
            }

            string allDefinitions = string.Join(", ", columnDefinitions.Concat(foreignKeys));
            return $"CREATE TABLE IF NOT EXISTS {tableName} ({allDefinitions});";
        }

        #region CRUD
        public static string Insert(List<PropertyInfo> properties, string tableName)
        {
            string columnNames = string.Join(", ", properties.Select(property => property.Name));
            string parameterMarks = string.Join(", ", properties.Select(_ => "?"));

            return $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterMarks})";
        }

        public static string Update(List<PropertyInfo> properties, string tableName)
        {
            string columns = string.Join(", ", properties.Select(property => property.Name + " = ?"));
            return $"UPDATE {tableName} SET {columns} WHERE Id = ?";
        }

        public static string Delete(string tableName) => $"DELETE FROM {tableName} WHERE Id = ?";

        public static string LoadAll(string tableName) => $"SELECT * FROM {tableName}";

        public static string LoadSingle(string tableName) => $"SELECT * FROM {tableName} WHERE Id = ?";

        public static string LoadLast(string tableName) => $"SELECT * FROM {tableName} ORDER BY Id DESC LIMIT 1";
        #endregion

        #region Get by parameter
        public static string FindBySearchWord(string propertyName, string tableName) => $"SELECT * FROM {tableName} WHERE {propertyName} LIKE ?";

        public static string LoadById(string propertyName, string tableName) => $"SELECT * FROM {tableName} WHERE {propertyName} = ?";

        public static string DeleteAllById(string propertyName, string tableName) => $"DELETE FROM {tableName} WHERE {propertyName} = ?";
        #endregion
    }
}
