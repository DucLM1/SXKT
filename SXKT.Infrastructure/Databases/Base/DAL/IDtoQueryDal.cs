using SXKT.Infrastructure.Databases.Base.DTO;

namespace SXKT.Infrastructure.Databases.Base.DAL
{
    public interface IDtoQueryDal<T, TId> where T : IDto
    {
    }

    public interface IDtoQueryDal<T> : IDtoQueryDal<T, int> where T : IDto
    {
    }
}