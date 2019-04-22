using SXKT.Infrastructure.Databases.Base.Entities;

namespace SXKT.Infrastructure.Databases.Base.DAL
{
    public interface IEntityQueryDal<T, TId> where T : IDbEntity<TId>
    {
    }
    public interface IEntityQueryDal<T> : IEntityQueryDal<T, int> where T : IDbEntity<int>
    {
    }


}