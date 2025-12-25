using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VClinic.Application.Common.Exceptions
{
    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base(message, StatusCodes.Status404NotFound)
        {
        }
    }
}
