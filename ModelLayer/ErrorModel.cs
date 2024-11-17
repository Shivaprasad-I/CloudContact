using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ErrorModel
    {
        public bool Success { get; set; } 
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
