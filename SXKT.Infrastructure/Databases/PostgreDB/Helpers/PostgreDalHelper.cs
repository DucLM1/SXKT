using Npgsql;
using SXKT.Infrastructure.Databases.Base;
using SXKT.Infrastructure.Databases.Base.Conditions;
using SXKT.Infrastructure.Databases.Base.DTO;
using SXKT.Infrastructure.Databases.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SXKT.Infrastructure.Databases.PostgreDB.Helpers
{
    public class PostgreDalHelper
    {
        //private static readonly ILoggerEs _loggerES = DVGServiceLocator.Current.GetInstance<ILoggerEs>();

        public static string GetTableName<TEntity>() where TEntity : class
        {
            return typeof(TEntity).Name.Replace("Entity", string.Empty);
        }

        public static string GetDtoName<TDto>() where TDto : IDto
        {
            return typeof(TDto).Name.Replace("Dto", string.Empty).ToLower();
        }

        public static void ExecuteCommandEntityStore(string storeName, ICondition condition, IDbContext commandDbContext)
        {
            try
            {
                PostgresSQL db = commandDbContext as PostgresSQL;
                using (NpgsqlCommand command = db.StoreProcedureWithCurrentTransaction(storeName, condition))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, storeName, condition, commandDbContext);
            }
        }

        internal static void ExecuteCommandEntityStore<TId>(string storeName, TId id, IDbContext commandDbContext)
        {
            try
            {
                PostgresSQL db = commandDbContext as PostgresSQL;
                using (NpgsqlCommand command = db.StoreProcedureWithCurrentTransaction(storeName))
                {
                    db.AddParameter(command, "_id", id, PostgresSQL.TypeMap[id.GetType()]);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, storeName, id, commandDbContext);
            }
        }

        public static void ExecuteCommandEntityStore<TEntity, TId>(string storeName, TEntity entity, IDbContext commandDbContext) where TEntity : IDbEntity<TId>
        {
            try
            {
                PostgresSQL db = commandDbContext as PostgresSQL;
                using (NpgsqlCommand command = db.StoreProcedureWithCurrentTransaction(storeName, entity))
                {
                    command.ExecuteNonQuery();
                    //  result = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, storeName, entity, commandDbContext);
            }
        }

        public static int ExecuteScalarEntityStore<TEntity, TId>(string storeName, TEntity entity, IDbContext commandDbContext) where TEntity : IDbEntity<TId>
        {
            int result = 0;
            try
            {
                PostgresSQL db = commandDbContext as PostgresSQL;
                using (NpgsqlCommand command = db.StoreProcedureWithCurrentTransaction(storeName, entity))
                {
                    result = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // _loggerES.WriteLogExeption(ex, storeName, entity, commandDbContext);
            }
            return result;
        }

        public static IEnumerable<TEntity> GetAll<TEntity>(string tableName) where TEntity : class
        {
            try
            {
                using (PostgresSQL db = new PostgresSQL(PostgresSQL.DBPosition.Slave))
                {
                    using (NpgsqlCommand command = db.CreateCommand("select * from " + tableName))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var entities = db.Mapper<TEntity>(reader);
                                reader.Close();
                                return entities;
                            }
                        }
                    }
                }

                return new List<TEntity>();
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, tableName);
                return new List<TEntity>();
            }
        }

        public static TEntity GetById<TEntity, TId>(string tableName, TId id, string idName) where TEntity : class, IDbEntity<TId>
        {
            try
            {
                using (PostgresSQL db = new PostgresSQL(PostgresSQL.DBPosition.Slave))
                {
                    using (NpgsqlCommand command = db.CreateCommand("select * from " + tableName + " where " + idName + " = " + id))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var entities = db.Mapper<TEntity>(reader);
                                reader.Close();
                                return entities.FirstOrDefault();
                            }
                        }
                    }
                }

                return default(TEntity);
            }
            catch (Exception ex)
            {
                // _loggerES.WriteLogExeption(ex, tableName, id, idName);
                return default(TEntity);
            }
        }

        public static TEntity GetById<TEntity, TId>(string tableName, TId id, string idName, PostgresSQL.DBPosition dbPosition) where TEntity : class, IDbEntity<TId>
        {
            try
            {
                using (PostgresSQL db = new PostgresSQL(dbPosition))
                {
                    using (NpgsqlCommand command = db.CreateCommand("select * from " + tableName + " where " + idName + " = " + id))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var entities = db.Mapper<TEntity>(reader);
                                reader.Close();
                                return entities.FirstOrDefault();
                            }
                        }
                    }
                }

                return default(TEntity);
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, tableName, id, idName, dbPosition);
                return default(TEntity);
            }
        }

        public static IEnumerable<T> List<T>(string storeName, ICondition condition) where T : class, IDto
        {
            IEnumerable<T> entities = new List<T>();
            try
            {
                using (PostgresSQL db = new PostgresSQL(PostgresSQL.DBPosition.Slave))
                {
                    using (NpgsqlCommand command = db.StoreProcedure(storeName, condition))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                entities = db.Mapper<T>(reader);
                                reader.Close();
                            }
                        }

                        //Logger.WriteLog(Logger.LogType.Info, "List<T> 2: " + ""+ "ms");
                    }
                }
                return entities;
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, storeName, condition);
                return entities;
            }
        }

        public static IEnumerable<T> List<T>(string storeName, ICondition condition, PostgresSQL.DBPosition dbPosition) where T : class, IDto
        {
            IEnumerable<T> entities = new List<T>();
            try
            {
                using (PostgresSQL db = new PostgresSQL(dbPosition))
                {
                    using (NpgsqlCommand command = db.StoreProcedure(storeName, condition))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                entities = db.Mapper<T>(reader);

                                reader.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, storeName, condition);
            }
            return entities;
        }

        public static int CountTotalRecord(string storeName, ICondition condition)
        {
            try
            {
                using (PostgresSQL db = new PostgresSQL(PostgresSQL.DBPosition.Slave))
                {
                    using (NpgsqlCommand command = db.StoreProcedure(storeName, condition))
                    {
                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                //_loggerES.WriteLogExeption(ex, storeName, condition);
                return 0;
            }
        }

        /// <summary>
        /// Gọi postgre function và trả ra danh sách phần tử
        /// </summary>
        /// <typeparam name="T">Kiểu của phần tử trong danh sách trả về</typeparam>
        /// <param name="spName"></param>
        /// <param name="condition"></param>
        /// <param name="connectionString"></param>
        /// <returns>>Danh sách phần tử</returns>
        public static IEnumerable<T> QuerySP<T>(string spName, object condition, string connectionString) where T : class
        {
            IEnumerable<T> entities = new List<T>();
            try
            {
                using (PostgresSQL db = new PostgresSQL(connectionString))
                {
                    using (NpgsqlCommand command = db.StoreProcedure(spName, condition))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                entities = db.Mapper<T>(reader);

                                reader.Close();
                            }
                        }
                    }
                }
                return entities;
            }
            catch (Exception ex)
            {
                //Logger.FatalLog(ex);
                //return entities;
                throw;
            }
        }

        /// <summary>
        /// Gọi postgre function thực thi một cái gì đó và trả về số bản ghi bị thay đổi
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="condition"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int ExecuteSP(string spName, object condition, string connectionString)
        {
            try
            {
                using (PostgresSQL db = new PostgresSQL(connectionString))
                {
                    using (NpgsqlCommand command = db.StoreProcedure(spName, condition))
                    {
                        int result = command.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteLog(LogType.Error, ex);
                //return 0;
                throw;
            }
        }

        /// <summary>
        /// Gọi postgre function và trả về giá trị của cột đầu tiên của hàng đầu tiên của danh sách phần tử (thường dùng trong các hàm count, hoặc trả về id vừa được insert...)
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="condition"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static object ExecuteScalarSP(string spName, object condition, string connectionString)
        {
            try
            {
                using (PostgresSQL db = new PostgresSQL(connectionString))
                {
                    using (NpgsqlCommand command = db.StoreProcedure(spName, condition))
                    {
                        var result = command.ExecuteScalar();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteLog(LogType.Error, ex);
                //return 0;
                throw;
            }
        }
    }
}