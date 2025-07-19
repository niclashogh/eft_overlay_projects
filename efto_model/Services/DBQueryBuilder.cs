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
                // Set Property type
                string type = property.Type switch
                {
                    SQLPropertyTypes.nVARCHAR => "nVARCHAR(200)",
                    _ => property.Type.ToString()
                };

                // Set Property notation
                string notation = property.Notation switch
                {
                    SQLPropertyNotations.PrimaryKeyId => " PRIMARY KEY AUTOINCREMENT",
                    SQLPropertyNotations.PrimaryKeyName => " PRIMARY KEY",
                    SQLPropertyNotations.Unique => " NOT NULL UNIQUE",
                    SQLPropertyNotations.NotNull => " NOT NULL",
                    _ => "" // Nullable & ForeignKey has no end-notations
                };

                columnDefinitions.Add($"{property.Name} {type}{notation}"); //Note: Missing space between {type} and {notation} is correct

                // Set ForeignKey references
                if (property.Notation == SQLPropertyNotations.ForeignKey)
                {
                    if (property.ForeignKeyReference.HasValue)
                    {
                        var foreignKey = property.ForeignKeyReference.Value;
                        foreignKeys.Add($"FOREIGN KEY ({property.Name}) REFERENCES {foreignKey.table}({foreignKey.property})");
                    }
                    else throw new Exception("DBQueryBuilder.CreateTable: Syntax error in ForeignKey");
                }
            }

            string allDefinitions = string.Join(", ", columnDefinitions.Concat(foreignKeys));
            return $"CREATE TABLE IF NOT EXISTS {tableName} ({allDefinitions});";
        }

        public static string Insert(List<PropertyInfo> properties, string tableName)
        {
            string columnNames = string.Join(", ", properties.Select(property => property.Name));
            string parameterMarks = string.Join(", ", properties.Select(_ => "?"));

            return $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterMarks})";
        }

        public static string UpdateById(List<PropertyInfo> properties, string tableName)
        {
            string columns = string.Join(", ", properties.Select(property => property.Name + " = ?"));
            return $"UPDATE {tableName} SET {columns} WHERE Id = ?";
        }

        public static string UpdateByKey(List<PropertyInfo> properties, string key, string tableName)
        {
            string columns = string.Join(", ", properties.Select(property => property.Name + " = ?"));
            return $"UPDATE {tableName} SET {columns} WHERE {key} = ?";
        }

        public static string DeleteById(string tableName) => $"DELETE FROM {tableName} WHERE Id = ?";
        public static string DeleteByKey(string propertyName, string tableName) => $"DELETE FROM {tableName} WHERE {propertyName} = ?";

        public static string LoadAll(string tableName) => $"SELECT * FROM {tableName}";

        public static string LoadSingleById(string tableName) => $"SELECT * FROM {tableName} WHERE Id = ?";
        public static string LoadSingleByKey(string propertyName, string tableName) => $"SELECT * FROM {tableName} WHERE {propertyName} = ?";

        public static string LoadLastById(string tableName) => $"SELECT * FROM {tableName} ORDER BY Id DESC LIMIT 1";
        public static string LoadLastByKey(string propertyName, string tableName) => $"SELECT * FROM {tableName} ORDER BY {propertyName} DESC LIMIT 1";

        public static string FindBySearchWord(string propertyName, string tableName) => $"SELECT * FROM {tableName} WHERE {propertyName} LIKE ?";

        public static string LoadById(string tableName) => $"SELECT * FROM {tableName} WHERE Id = ?";
        public static string LoadByKey(string propertyName, string tableName) => $"SELECT * FROM {tableName} WHERE {propertyName} = ?";

        public static string DeleteAllByKey(string propertyName, string tableName) => $"DELETE FROM {tableName} WHERE {propertyName} = ?";
    }
}
