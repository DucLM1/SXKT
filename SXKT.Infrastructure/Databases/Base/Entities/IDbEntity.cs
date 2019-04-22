using SXKT.Infrastructure.Databases.Base.Conditions;
using SXKT.Infrastructure.Databases.Base.DTO;

namespace SXKT.Infrastructure.Databases.Base.Entities
{
    public interface IDbEntity<T>:IDto,ICondition
    {
        T GetId();

        void SetId(T id);

        string IdName { get; }
    }

    public interface IDbEntity : IDbEntity<int>
    {
    }
}