using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities.Dto
{
    public class ReturnOption
    {
        public ReturnOption(string message, bool IsSuccess = true)
        {
            this.IsSuccess = IsSuccess;
            this.Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
