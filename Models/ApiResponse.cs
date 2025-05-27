using System.Net;

namespace WebService.API.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public object? Result { get; set; }


        //public dynamic? Message { get; set; }

        //public IEnumerable<dynamic>? Errors { get; set; }
    }
}
