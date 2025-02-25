using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorReservas.Domain.Validation
{
    public class ReservaException : Exception
    {
        public ReservaException(string message): base(message) { }
    }
}
