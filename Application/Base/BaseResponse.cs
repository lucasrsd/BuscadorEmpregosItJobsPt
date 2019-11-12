using System.Net;
using System.Net.Http;
using RestSharp;

namespace Crawler_ItJobs_Portugal.Application.Base
{
    public class BaseResponse 
    {

    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse (T data)
        {
            this.Data = data;
        }
        
        public T Data { get; set; }
    }
}