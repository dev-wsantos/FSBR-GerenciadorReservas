﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorReservas.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(string? message) : base(message)
        {
        }

        public static void When(bool hasError, string message)
        {
            if (hasError)
                throw new DomainExceptionValidation(message);
        }
    }
}
