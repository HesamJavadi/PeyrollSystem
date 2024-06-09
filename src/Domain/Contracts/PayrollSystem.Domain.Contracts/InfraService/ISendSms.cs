using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.InfraService
{
    public interface ISendSms
    {
        void send(string text, string[] number);
    }
}
