using Microsoft.AspNetCore.Http;
using SXKT.Infrastructure.Databases.PostgreDB.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SXKT.Infrastructure.Databases.PostgreDB.Dto
{
    public abstract class BaseDtoDal : IDtoDal
    {
        public readonly IHttpContextAccessor httpContextAccessor;
        public BaseDtoDal(
            IHttpContextAccessor httpContextAccessor
            )
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<T> QuerySP<T>(string spName, object condition) where T : class
        {
            var connectionString = GetConnectionString();
            var result = PostgreDalHelper.QuerySP<T>(spName, condition, connectionString).ToList();
            return result;
        }

        public List<T> QuerySP<T>(string spName, object condition, string connectionString) where T : class
        {
            var result = PostgreDalHelper.QuerySP<T>(spName, condition, connectionString).ToList();
            return result;
        }

        public int ExecuteSP(string spName, object param)
        {
            var connectionString = GetConnectionString();
            var result = PostgreDalHelper.ExecuteSP(spName, param, connectionString);
            return result;
        }

        public int ExecuteSP(string spName, object param, string connectionString)
        {
            var result = PostgreDalHelper.ExecuteSP(spName, param, connectionString);
            return result;
        }

        public object ExecuteScalarSP(string spName, object param)
        {
            var connectionString = GetConnectionString();
            var result = PostgreDalHelper.ExecuteScalarSP(spName, param, connectionString);
            return result;
        }

        public object ExecuteScalarSP(string spName, object param, string connectionString)
        {
            var result = PostgreDalHelper.ExecuteScalarSP(spName, param, connectionString);
            return result;
        }

        public abstract string GetConnectionString();
    }
}