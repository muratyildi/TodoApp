using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Web.Constants
{
    public class ApiResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        public ApiResult()
        {

        }

        public ApiResult(string status, string message = null, object data = null)
        {
            Status = status;
            Data = data;
            Message = message;
        }
    }

    public class ApiResult<T>
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public ApiResult()
        {

        }

        public ApiResult(string status, string message, T data)
        {
            Status = status;
            Data = data;
            Message = message;
        }
    }
}
