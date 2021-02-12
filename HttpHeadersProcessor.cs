using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Vano.Tools.Azure
{
    public class HttpHeadersProcessor
    {
        private List<Tuple<string, string>> _requestHeaders;
        private List<Tuple<string, string>> _responseHeaders;

        private string _statusCode;
        private string _host;

        private readonly HashSet<string> _headersToExclude = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Authorization" };

        private IList<Tuple<string, string>> RequestHeaders 
        {
            get
            {
                if (_requestHeaders == null)
                {
                    _requestHeaders = new List<Tuple<string, string>>();
                }

                return _requestHeaders;
            }
        }

        public IList<Tuple<string, string>> ResponseHeaders
        {
            get
            {
                if (_responseHeaders == null)
                {
                    _responseHeaders = new List<Tuple<string, string>>();
                }

                return _responseHeaders;
            }
        }

        public void CaptureRequest(string host, WebHeaderCollection requestHeaders)
        {
            _host = host;
            CaptureHeaders(requestHeaders, RequestHeaders);
        }

        public void CaptureResponse(HttpStatusCode statusCode, WebHeaderCollection responseHeaders)
        {
            _statusCode = string.Format("{0} ({1})", ((int)statusCode).ToString(), statusCode.ToString());
            CaptureHeaders(responseHeaders, ResponseHeaders);
        }

        private IList<Tuple<string, string>> CaptureHeaders(WebHeaderCollection headers, IList<Tuple<string, string>> target)
        {
            target.Clear();

            for (int i = 0; i < headers.Count; ++i)
            {
                string header = headers.GetKey(i);

                if (_headersToExclude.Contains(header))
                {
                    continue;
                }

                foreach (string value in headers.GetValues(i))
                {
                    target.Add(new Tuple<string, string>(header, value));
                }
            }

            return target;
        }

        public string GetFormattedRequestHeaders()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Host: {0}", _host ?? string.Empty));

            return GetFormattedHeaders(sb, RequestHeaders);
        }

        public string GetFormattedResponseHeaders()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Status: {0}", _statusCode ?? "0"));

            return GetFormattedHeaders(sb, ResponseHeaders);
        }

        private string GetFormattedHeaders(StringBuilder sb, IList<Tuple<string, string>> headersDictionary)
        {
            foreach (var kvp in headersDictionary)
            {
                sb.AppendLine(string.Format("{0}: {1}", kvp.Item1, kvp.Item2));
            }

            return sb.ToString();
        }
    }
}
