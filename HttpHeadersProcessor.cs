using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Vano.Tools.Azure
{
    public class HttpHeadersProcessor
    {
        private List<Tuple<string, string>> _requestHeaders;
        private List<Tuple<string, string>> _responseHeaders;

        private string _statusCode;
        private string _host;

        private readonly HashSet<string> _sensitiveHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Authorization" };

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

        #region WebHeaders

        public void CaptureWebHeadersFromRequest(string host, WebHeaderCollection requestHeaders)
        {
            _host = host;
            CaptureWebHeaders(requestHeaders, RequestHeaders);
        }

        public void CaptureWebHeadersFromResponse(HttpStatusCode statusCode, WebHeaderCollection responseHeaders)
        {
            _statusCode = string.Format("{0} ({1})", ((int)statusCode).ToString(), statusCode.ToString());
            CaptureWebHeaders(responseHeaders, ResponseHeaders);
        }

        private IList<Tuple<string, string>> CaptureWebHeaders(WebHeaderCollection headers, IList<Tuple<string, string>> target)
        {
            target.Clear();

            for (int i = 0; i < headers.Count; ++i)
            {
                string header = headers.GetKey(i);

                if (_sensitiveHeaders.Contains(header))
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

        #endregion

        #region HttpHeaders

        public void CaptureHttpHeadersFromRequest(string host, HttpHeaders requestHeaders, bool displaySecrets)
        {
            _host = host;
            CaptureHttpHeaders(requestHeaders, RequestHeaders, displaySecrets);
        }

        public void CaptureHttpHeadersFromResponse(HttpStatusCode statusCode, HttpHeaders responseHeaders, bool displaySecrets)
        {
            _statusCode = string.Format("{0} ({1})", ((int)statusCode).ToString(), statusCode.ToString());
            CaptureHttpHeaders(responseHeaders, ResponseHeaders, displaySecrets);
        }

        private IList<Tuple<string, string>> CaptureHttpHeaders(HttpHeaders headers, IList<Tuple<string, string>> target, bool displaySecrets)
        {
            target.Clear();

            foreach(KeyValuePair<string, IEnumerable<string>> header in headers)
            {
                foreach (string value in header.Value)
                {
                    string postProcessedValue = _sensitiveHeaders.Contains(header.Key) && displaySecrets == false ? 
                        "●●●●●●●●" : 
                        value;
                    
                    target.Add(new Tuple<string, string>(header.Key, postProcessedValue));
                }
            }

            return target;
        }

        #endregion

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
