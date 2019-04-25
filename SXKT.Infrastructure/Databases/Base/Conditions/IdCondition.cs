namespace SXKT.Infrastructure.Databases.Base.Conditions
{
    public class IdCondition<T> : Condition
    {
        public T Id { get; set; }
    }
}