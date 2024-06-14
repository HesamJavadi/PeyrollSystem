using PayrollSystem.Domain.Core.ValueObjects.Bases;
using PayrollSystem.Domain.Core.ValueObjects.Management.WebServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Core.ValueObjects.Management.Setting
{
    public class LogoPath : BaseValueObject<LogoPath>
    {
        public string Value { get; private set; }

        public static LogoPath FromString(string value) => new LogoPath(value);
        private LogoPath(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        #region Operator Overloading
        public static explicit operator string(LogoPath title) => title.Value;
        public static implicit operator LogoPath(string value) => new(value);
        #endregion

        #region Methods
        public override string ToString() => Value;

        #endregion
    }
}
