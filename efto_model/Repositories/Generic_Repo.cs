using efto_model.Data;
using efto_model.Services;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace efto_model.Repositories
{
    public class Generic_Repo : DBContext
    {
        protected List<PropertyInfo> GetProperties<T>(T model, string[] propertyNames) where T : class => model.GetType().GetProperties().Where(property => propertyNames.Contains(property.Name)).ToList();

        #region CRUD
        protected void Add<T>(T model, string tableName, string database) where T : class
        {
            try
            {
                List<PropertyInfo> properties = model.GetType().GetProperties().Where(variable => variable.Name != "Id").ToList();
                string query = DBQueryBuilder.Insert(properties, tableName);
                object?[] propertiesAsObject = properties.Select(property => property.GetValue(model)).ToArray();

                if (propertiesAsObject != null && propertiesAsObject.Length > 0)
                {
                    using (SQLiteConnection db = SQLConnection(database))
                    {
                        db.Execute(query, propertiesAsObject);
                    }
                }
            }
            catch(Exception e) { Debug.WriteLine("Generic_Repo.Add: " + e); }
        }

        protected void Update<T>(T model, List<PropertyInfo> properties, string tableName, string database) where T : class
        {
            try
            {
                string query = DBQueryBuilder.Update(properties, tableName);
                object?[] propertiesAsObject = properties.Select(property => property.GetValue(model)).ToArray();

                if (propertiesAsObject != null || propertiesAsObject.Length > 0)
                {
                    PropertyInfo? idProperty = typeof(T).GetProperty("Id");

                    if (idProperty != null)
                    {
                        object? idAsObject = idProperty.GetValue(model);

                        if (idAsObject != null)
                        {
                            object[] parametersAsObject = propertiesAsObject.Concat(new object[] { idAsObject }).ToArray()!;

                            using (SQLiteConnection db = SQLConnection(database))
                            {
                                db.Execute(query, parametersAsObject);
                            }
                        }
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine("Generic_Repo.Update: " + e); }
        }

        protected void Delete(int id, string tableName, string database)
        {
            try
            {
                string query = DBQueryBuilder.Delete(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    db.Execute(query, id);
                }
            }
            catch(Exception e) { Debug.WriteLine("Generic_Repo.Delete: " + e); }
        }

        protected ObservableCollection<T> LoadAll<T>(string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadSingle(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    List<T> queryResult = db.Query<T>(query) ?? new List<T>();
                    return new ObservableCollection<T>(queryResult);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadAll: " + e);
                return new ObservableCollection<T>();
            }
        }

        protected T LoadSingle<T>(int id, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadSingle(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    return db.Query<T>(query, id).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadSingle: " + e);
                return new T();
            }
        }

        protected T LoadLast<T>(string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadLast(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    return db.Query<T>(query).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadLast: " + e);
                return new T();
            }
        }
        #endregion

        #region Get by parameter
        protected ObservableCollection<T> FindBySearchWord<T>(string propertyName, string searchWord, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.FindBySearchWord(propertyName, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    List<T>? queryResult = db.Query<T>(query, $"%{searchWord}%") ?? null;

                    if (queryResult != null && queryResult.Count > 0)
                    {
                        if (searchWord.Length == 1)
                        {
                            queryResult = queryResult.Where(item =>
                            {
                                return propertyName != null && propertyName.StartsWith(searchWord, StringComparison.OrdinalIgnoreCase);
                            }).ToList();
                        }

                        return new ObservableCollection<T>(queryResult);
                    }
                    else return new ObservableCollection<T>();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.Find: " + e);
                return new ObservableCollection<T>();
            }
        } 

        protected ObservableCollection<T> LoadById<T>(int id, string propertyName, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadById(propertyName, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    List<T> queryResult = db.Query<T>(query, id) ?? new List<T>();
                    return new ObservableCollection<T>(queryResult);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadById: " + e);
                return new ObservableCollection<T>();
            }
        }

        protected void DeleteAllById(int id, string propertyName, string tableName, string database)
        {
            try
            {
                string query = DBQueryBuilder.DeleteAllById(propertyName, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    db.Execute(query, id);
                }
            }
            catch (Exception e) { Debug.WriteLine("Generic_Repo.DeleteAllById: " + e); }
        }
        #endregion
    }
}
