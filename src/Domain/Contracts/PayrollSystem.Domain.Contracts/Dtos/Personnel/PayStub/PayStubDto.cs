using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.Dtos.Personnel.PayStub
{
    public class PayStubDto
    {
        public int PersonelCode { get; set; }
        public string FieldName { get; set; }
        public string Caption { get; set; }
        public string FieldValue { get; set; }
        public int FieldType { get; set; }
    }
}
