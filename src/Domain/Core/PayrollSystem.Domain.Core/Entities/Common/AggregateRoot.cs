using PayrollSystem.Domain.Core.Entities.Common;
using System.Reflection;

namespace PayrollSystem.Domain.Core.Entities.Common;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId> where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    
}

public abstract class AggregateRoot : AggregateRoot<long>
{

}