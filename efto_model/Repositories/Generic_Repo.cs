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

        protected ObservableCollection<T> LoadAll<T>(string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadAll(tableName);

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

        #region Update
        protected void UpdateById<T>(T model, List<PropertyInfo> properties, string tableName, string database) where T : class
        {
            try
            {
                string query = DBQueryBuilder.UpdateById(properties, tableName);
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
            catch (Exception e) { Debug.WriteLine("Generic_Repo.UpdateById: " + e); }
        }

        protected void UpdateByKey<T>(T model, List<PropertyInfo> properties, string key, string tableName, string database) where T : class
        {
            try
            {
                string query = DBQueryBuilder.UpdateById(properties, tableName);
                object?[] propertiesAsObject = properties.Select(property => property.GetValue(model)).ToArray();

                if (propertiesAsObject != null || propertiesAsObject.Length > 0)
                {
                    PropertyInfo? keyProperty = typeof(T).GetProperty(key);

                    if (keyProperty != null)
                    {
                        object? keyAsObject = keyProperty.GetValue(model);

                        if (keyAsObject != null)
                        {
                            object[] parametersAsObject = propertiesAsObject.Concat(new object[] { keyAsObject }).ToArray()!;

                            using (SQLiteConnection db = SQLConnection(database))
                            {
                                db.Execute(query, parametersAsObject);
                            }
                        }
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine("Generic_Repo.UpdateByKey: " + e); }
        }
        #endregion

        #region Delete
        protected void DeleteById(int id, string tableName, string database)
        {
            try
            {
                string query = DBQueryBuilder.DeleteById(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    db.Execute(query, id);
                }
            }
            catch(Exception e) { Debug.WriteLine("Generic_Repo.DeleteById: " + e); }
        }

        protected void DeleteByKey((string value, string key) property, string tableName, string database)
        {
            try
            {
                string query = DBQueryBuilder.DeleteByKey(property.key, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    db.Execute(query, property.value);
                }
            }
            catch (Exception e) { Debug.WriteLine("Generic_Repo.DeleteByKey: " + e); }
        }
        #endregion

        #region LoadSingle
        protected T LoadSingleById<T>(int id, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadSingleById(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    return db.Query<T>(query, id).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadSingleById: " + e);
                return new T();
            }
        }

        protected T LoadSingleByKey<T>((string value, string key) property, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadSingleByKey(property.key, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    return db.Query<T>(query, property.value).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadSingleByKey: " + e);
                return new T();
            }
        }
        #endregion

        #region LoadLast
        protected T LoadLastById<T>(string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadLastById(tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    return db.Query<T>(query).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadLastById: " + e);
                return new T();
            }
        }

        protected T LoadLastByKey<T>(string key, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadLastByKey(key, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    return db.Query<T>(query).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadLastByKey: " + e);
                return new T();
            }
        }
        #endregion

        protected ObservableCollection<T> FindBySearchWord<T>((string searchWord, string key) property, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.FindBySearchWord(property.key, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    List<T>? queryResult = db.Query<T>(query, $"%{property.searchWord}%") ?? null;

                    if (queryResult != null && queryResult.Count > 0)
                    {
                        if (property.searchWord.Length == 1)
                        {
                            queryResult = queryResult.Where(item =>
                            {
                                return property.key != null && property.key.StartsWith(property.searchWord, StringComparison.OrdinalIgnoreCase);
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

        #region LoadBy
        protected ObservableCollection<T> LoadById<T>(int id, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadById(tableName);

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

        protected ObservableCollection<T> LoadByKey<T>((string value, string key) property, string tableName, string database) where T : class, new()
        {
            try
            {
                string query = DBQueryBuilder.LoadByKey(property.key, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    List<T> queryResult = db.Query<T>(query, property.value) ?? new List<T>();
                    return new ObservableCollection<T>(queryResult);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic_Repo.LoadByKey: " + e);
                return new ObservableCollection<T>();
            }
        }
        #endregion

        protected void DeleteAllByKey((int value, string key) property, string tableName, string database)
        {
            try
            {
                string query = DBQueryBuilder.DeleteAllByKey(property.key, tableName);

                using (SQLiteConnection db = SQLConnection(database))
                {
                    db.Execute(query, property.value);
                }
            }
            catch (Exception e) { Debug.WriteLine("Generic_Repo.DeleteAllByKey: " + e); }
        }
    }
}
