using System;
using System.Net;

namespace Vano.Tools.Azure.Model
{
    internal class AzureClientException : Exception
    {
        public AzureClientException(HttpStatusCode statusCode, string response) 
            : base(response)
        {
            this.StatusCode = statusCode;
            this.Response = response;
        }

   
        public HttpStatusCode StatusCode { get; set; }

        public string Response { get; set; }
    }
}
