using SXKT.Infrastructure.Databases.Base.Attributes;
using SXKT.Infrastructure.Databases.Base.Conditions;
using System;
using System.Reflection;

namespace SXKT.Infrastructure.Databases.Base.Entities
{
    [Serializable]
    public class DbEntityBase<T> : Condition,IDbEntity<T>
    {

        private PropertyInfo _idInfo;

        [Ignore]
        public string IdName => _idInfo == null ? null : _idInfo.Name;
       

        public T GetId()
        {
            if (_idInfo != null)
                return (T)_idInfo.GetValue(this);
            else
                return default(T);
        }

        public void SetId(T id)
        {
            if (_idInfo != null)
                _idInfo.SetValue(this, id);
        }

        public DbEntityBase()
        {
            if (_idInfo == null)
                _idInfo = GetIdInfo();
        }

        private PropertyInfo GetIdInfo()
        {
            if (_idInfo != null)
                return _idInfo;
            var props = this.GetType().GetProperties();
            foreach (var propertyInfo in props)
            {
                if (propertyInfo.GetCustomAttribute<PrimaryKey>() != null)
                {
                    _idInfo = propertyInfo;
                    break;
                }
            }

            return _idInfo;
        }        
    }
    [Serializable]
    public class DbEntityBase : DbEntityBase<int>, IDbEntity
    {
    }
}