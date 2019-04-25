using SXKT.Infrastructure.Databases.Base;
using SXKT.Infrastructure.Databases.Base.Conditions;
using SXKT.Infrastructure.Databases.Base.DAL;
using SXKT.Infrastructure.Databases.Base.Entities;
using SXKT.Infrastructure.Databases.PostgreDB.Helpers;
using SXKT.Infrastructure.Utility;

namespace SXKT.Infrastructure.Databases.PostgreDB.Commands
{
    public class CommandDal<TEntity, TId> : ICommandDal<TEntity, TId> where TEntity : class, IDbEntity<TId>
    {
        private readonly string _tableName = PostgreDalHelper.GetTableName<TEntity>().ToLower();
        private IDbContext _commandDbContext;
        private string _funcPrefix = AppSettings.Instance.GetString("FuncPrefix");

        public virtual void Add(TEntity entity)
        {
            string storeName = _funcPrefix + _tableName + "_insert";
            PostgreDalHelper.ExecuteCommandEntityStore<TEntity, TId>(storeName, entity, _commandDbContext);
        }

        public virtual void Update(TEntity entity)
        {
            string storeName = _funcPrefix + _tableName + "_update";
            PostgreDalHelper.ExecuteCommandEntityStore<TEntity, TId>(storeName, entity, _commandDbContext);
        }

        public virtual void DeleteById(TId id)
        {
            string storeName = _funcPrefix + _tableName + "_delete";
            PostgreDalHelper.ExecuteCommandEntityStore<TId>(storeName, id, _commandDbContext);
        }

        public void SetWriteDbContext(IDbContext writeContext)
        {
            _commandDbContext = writeContext;
        }

        public void Delete(ICondition condition)
        {
            string conditionName = (condition is DbEntity) ? string.Empty : condition.GetType().Name.ToLower().Replace(_tableName, string.Empty)
                .Replace("condition", string.Empty);
            string storeName = _funcPrefix + _tableName + "_delete" + (string.IsNullOrEmpty(conditionName) ? string.Empty : "_" + conditionName);
            PostgreDalHelper.ExecuteCommandEntityStore(storeName, condition, _commandDbContext);
        }

        public int AddGetId(TEntity entity)
        {
            string storeName = _funcPrefix + _tableName + "_insert";
            return PostgreDalHelper.ExecuteScalarEntityStore<TEntity, TId>(storeName, entity, _commandDbContext);
        }
    }

    public class CommandDal<Tentity> : CommandDal<Tentity, int>, ICommandDal<Tentity> where Tentity : class, IDbEntity
    {

    }
}