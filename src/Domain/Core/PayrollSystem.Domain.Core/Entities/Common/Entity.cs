using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.Entities.Common
{
    public class Entity<TId> where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
    {
        public TId ID { get; set; }
        public DateTime RegDate { get => DateTime.Now; }
        public string? RegModifyDate { get; }
        public string RegUser { get => "1"; }
        public string? RegModifyUser { get; }
    }
}
