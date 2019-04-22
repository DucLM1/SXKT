using SXKT.Infrastructure.Databases.Base.Attributes;

namespace SXKT.Infrastructure.Databases.Base.Entities
{
    public class DbEntity<T> : DbEntityBase<T>, IDbEntity<T>
    {
        [PrimaryKey]
        public virtual T Id { get; set; }
    }

    public class DbEntity : DbEntity<int>, IDbEntity
    {
    }
}