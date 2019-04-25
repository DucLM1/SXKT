using SXKT.Infrastructure.Databases.Base.Conditions;
using SXKT.Infrastructure.Databases.Base.DAL;
using SXKT.Infrastructure.Databases.Base.Entities;
using SXKT.Infrastructure.Databases.PostgreDB.Helpers;
using SXKT.Infrastructure.Utility;
using System;
using System.Collections.Generic;

namespace SXKT.Infrastructure.Databases.PostgreDB.Queries
{
    public class EntityQueryDal<TDbEntity, TId> : IEntityQueryDal<TDbEntity, TId> where TDbEntity : class, IDbEntity<TId>
    {
        private readonly string _tableName = PostgreDalHelper.GetTableName<TDbEntity>().ToLower();
        private readonly string _funcPrefix = AppSettings.Instance.GetString("FuncPrefix");

        public IEnumerable<TDbEntity> GetAll()
        {
            return PostgreDalHelper.GetAll<TDbEntity>(_tableName);
        }

        public TDbEntity GetById(TId id)
        {
            var idName = Activator.CreateInstance<TDbEntity>().IdName.ToLower();
            return PostgreDalHelper.GetById<TDbEntity, TId>(_tableName, id, idName);
        }

        public TDbEntity GetById(TId id, PostgresSQL.DBPosition dbPosition)
        {
            var idName = Activator.CreateInstance<TDbEntity>().IdName.ToLower();
            return PostgreDalHelper.GetById<TDbEntity, TId>(_tableName, id, idName, dbPosition);
        }

        public IEnumerable<TDbEntity> List(ICondition condition)
        {
            string conditionName = (condition is TDbEntity) ? string.Empty : condition.GetType().Name.ToLower().Replace(_tableName, string.Empty).Replace("condition", string.Empty);
            string storeName = $"{_funcPrefix}{_tableName}_getlist{(string.IsNullOrEmpty(conditionName) ? string.Empty : "_" + conditionName)}";

            return PostgreDalHelper.List<TDbEntity>(storeName, condition);
        }

        public IEnumerable<TDbEntity> List(ICondition condition, PostgresSQL.DBPosition dbPosition)
        {
            string conditionName = (condition is TDbEntity) ? string.Empty : condition.GetType().Name.ToLower().Replace(_tableName, string.Empty).Replace("condition", string.Empty);
            string storeName = $"{_funcPrefix}{_tableName}_getlist{(string.IsNullOrEmpty(conditionName) ? string.Empty : "_" + conditionName)}";

            return PostgreDalHelper.List<TDbEntity>(storeName, condition, dbPosition);
        }

        public int CountTotalRecord(ICondition condition)
        {
            string conditionName = (condition is TDbEntity) ? string.Empty : condition.GetType().Name.ToLower().Replace(_tableName, string.Empty).Replace("condition", string.Empty);
            string storeName = $"{_funcPrefix}{_tableName}_getcount{(string.IsNullOrEmpty(conditionName) ? string.Empty : "_" + conditionName)}";

            return PostgreDalHelper.CountTotalRecord(storeName, condition);
        }
    }

    public class EntityQueryDal<TDbEntity> : EntityQueryDal<TDbEntity, int>, IEntityQueryDal<TDbEntity, int> where TDbEntity : class, IDbEntity
    {
    }
}