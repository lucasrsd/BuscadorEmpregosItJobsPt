using System.Net;
using System.Net.Http;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Application.Base
{
    public class BaseResponse
    {
        public BaseResponse (bool ok, string errorMessage)
        {
            this.Ok = ok;
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
        public bool Ok { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse (bool ok, string errorMessage) : base (ok, errorMessage) { }
        public BaseResponse (T data) : base (true, null)
        {
            this.Data = data;
            this.Ok = true;
        }
        public T Data { get; set; }
    }
}