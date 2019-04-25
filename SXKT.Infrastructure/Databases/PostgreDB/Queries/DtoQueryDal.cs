using SXKT.Infrastructure.Databases.Base.Conditions;
using SXKT.Infrastructure.Databases.Base.DAL;
using SXKT.Infrastructure.Databases.Base.DTO;
using SXKT.Infrastructure.Databases.Base.Entities;
using SXKT.Infrastructure.Databases.PostgreDB.Helpers;
using SXKT.Infrastructure.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SXKT.Infrastructure.Databases.PostgreDB.Queries
{
    public class DtoQueryDal<TDto, TId> : IDtoQueryDal<TDto, TId> where TDto : class, IDto
    {
        private readonly string _dtoName = PostgreDalHelper.GetDtoName<TDto>();
        private string _funcPrefix = AppSettings.Instance.GetString("FuncPrefix");

        public IEnumerable<TDto> List(ICondition condition)
        {
            string conditionName = (condition is DbEntityBase) ? string.Empty : condition.GetType().Name.ToLower().Replace(_dtoName, string.Empty)
                .Replace("condition", string.Empty);
            string storeName = _funcPrefix + _dtoName + "_getlist" + (string.IsNullOrEmpty(conditionName) ? string.Empty : "_" + conditionName);
            return PostgreDalHelper.List<TDto>(storeName, condition);
        }

        public IEnumerable<TDto> List(ICondition condition, PostgresSQL.DBPosition position)
        {
            string conditionName = (condition is DbEntityBase) ? string.Empty : condition.GetType().Name.ToLower().Replace(_dtoName, string.Empty)
                .Replace("condition", string.Empty);
            string storeName = _funcPrefix + _dtoName + "_getlist" + (string.IsNullOrEmpty(conditionName) ? string.Empty : "_" + conditionName);
            return PostgreDalHelper.List<TDto>(storeName, condition, position);
        }

        public TDto GetById(TId id)
        {
            string storeName = _funcPrefix + _dtoName + "_getbyid";
            return PostgreDalHelper.List<TDto>(storeName, new IdCondition<TId>()
            {
                Id = id
            }).FirstOrDefault();
        }

        public TDto GetById(TId id, PostgresSQL.DBPosition position)
        {
            string storeName = _funcPrefix + _dtoName + "_getbyid";
            return PostgreDalHelper.List<TDto>(storeName, new IdCondition<TId>()
            {
                Id = id
            }, position).FirstOrDefault();
        }
    }

    public class DtoQueryDal<TDto> : DtoQueryDal<TDto, int>, IDtoQueryDal<TDto, int> where TDto : class, IDto
    {
    }
}