using System;
using System.Collections.Generic;

namespace SXKT.Infrastructure.Databases.Base.Conditions
{
    public interface ICondition
    {
        IReadOnlyDictionary<string, Tuple<Type, object>> Conditions { get; }
    }
}