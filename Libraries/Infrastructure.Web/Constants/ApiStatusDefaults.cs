using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Web.Constants
{
    public class ApiStatusDefaults
    {
        public const string Success = "success";
        public const string Error = "error";
        public const string ValidationError = "validation-error";
        public const string Unauthorized = "unauthorized";
        public const string BadRequest = "bad-request";
        public const string NoContent = "no content";
    }
}
