using SXKT.Infrastructure.Databases.Base.Entities;

namespace SXKT.Infrastructure.Databases.Base.DAL
{
    public interface ICommandDal<T, TId> where T : IDbEntity<TId>
    {
    }

    public interface ICommandDal<T> : ICommandDal<T, int> where T : IDbEntity
    {
    }
}