using SXKT.Infrastructure.Databases.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SXKT.Infrastructure.Databases.Base.Conditions
{
    public class Condition : ICondition
    {
        private Dictionary<string, Tuple<Type, object>> _conditions;

        private void CreateConditions()
        {
            _conditions = new Dictionary<string, Tuple<Type, object>>();
            var props = GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name != "Conditions" && prop.GetCustomAttribute<Ignore>() == null)
                {
                    var val = prop.GetValue(this);
                    _conditions.Add("_" + prop.Name.ToLower(), new Tuple<Type, object>(prop.PropertyType, val));
                }
            }
        }

        public IReadOnlyDictionary<string, Tuple<Type, object>> Conditions
        {
            get
            {
                if (_conditions == null) CreateConditions();
                    return _conditions;
            }
        }
    }
}