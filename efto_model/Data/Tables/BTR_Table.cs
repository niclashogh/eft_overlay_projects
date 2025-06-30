using efto_model.Models.Base;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class BTR_Table : DBContext
    {
        public BTR_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(DBQueryBuilder.CreateTable(BTR_SQLContext.BTR_Table, BTR_SQLContext.BTR_Table_Name));
            }
        }
    }
}
