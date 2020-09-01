using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Vano.Tools.Azure.RDFE
{
    public class AdministrationMessageInspector<T> : IClientMessageInspector where T : class
    {
        private const string HTTP_AUTHORIZATION_HEADER = "Authorization";
        private const string HTTP_ACCEPT_LANGUAGE_HEADER = "Accept-Language";

        private AdministrationClientBase<T> Client;

        public AdministrationMessageInspector(AdministrationClientBase<T> client)
        {
            this.Client = client;
        }

        private string GetAuthorizationInfo()
        {
            string ret;
            if (Client.ClientCredentials.ClientCertificate.Certificate == null)
            {
                string authInfo = Client.ClientCredentials.UserName.UserName + ":" + Client.ClientCredentials.UserName.Password;
                authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
                ret = "Basic " + authInfo;
            }
            else
            {
                ret = Client.AuthCertThumbprint;
            }

            return ret;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            HttpResponseMessageProperty httpReplyMessageProperty;
            object httpReplyMessageObject;

            if (reply.Properties.TryGetValue(HttpResponseMessageProperty.Name, out httpReplyMessageObject))
            {
                httpReplyMessageProperty = httpReplyMessageObject as HttpResponseMessageProperty;

                if (httpReplyMessageObject == null)
                {
                    return;
                }

                // Get the continuation token from the header
                Client.Marker = httpReplyMessageProperty.Headers["x-ms-continuation-Marker"];

                Client.Warning = httpReplyMessageProperty.Headers["Warning"];

                if (Client.Tracing)
                {
                    foreach (var item in httpReplyMessageProperty.Headers.AllKeys)
                    {
                        if (item.StartsWith("TracingEvent_"))
                        {
                            Client.AddTraceInformation(httpReplyMessageProperty.Headers[item]);
                        }
                    }

                    // We are adding trace with null here to have an empty new line at the end of the tracing messages in Console.
                    Client.AddTraceInformation(null);
                }

                if (Client.TraceRequestReply)
                {
                    Console.WriteLine("### REPLY");
                    reply = TraceOutMessage(reply, null);
                }
            }
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestMessageProperty;
            object httpRequestMessageObject = null;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessageProperty = httpRequestMessageObject as HttpRequestMessageProperty;
                if (string.IsNullOrEmpty(httpRequestMessageProperty.Headers[HTTP_AUTHORIZATION_HEADER]))
                {
                    httpRequestMessageProperty.Headers[HTTP_AUTHORIZATION_HEADER] = GetAuthorizationInfo();
                }
            }
            else
            {
                httpRequestMessageProperty = new HttpRequestMessageProperty();
                httpRequestMessageProperty.Headers.Add(HTTP_AUTHORIZATION_HEADER, GetAuthorizationInfo());
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessageProperty);
            }

            httpRequestMessageProperty.Headers.Add("x-ms-version", this.Client.ApiVersion);

            if (Client.RequestId != Guid.Empty)
            {
                httpRequestMessageProperty.Headers.Add("x-ms-request-id", Client.RequestId.ToString());
            }

            if (Client.Tracing)
            {
                httpRequestMessageProperty.Headers.Add("x-ms-tracing", true.ToString());
            }

            if (!String.IsNullOrEmpty(Client.StampName))
            {
                httpRequestMessageProperty.Headers.Add("x-ms-stamp", Client.StampName);
            }

            if (!String.IsNullOrEmpty(Client.UserAgent))
            {
                httpRequestMessageProperty.Headers.Add("User-Agent", Client.UserAgent);
            }

            if (!string.IsNullOrEmpty(Client.AcceptLanguage))
            {
                httpRequestMessageProperty.Headers.Add(HTTP_ACCEPT_LANGUAGE_HEADER, Client.AcceptLanguage);
            }

            // Add extra headers
            foreach (var item in Client.ExtraHeaders)
            {
                httpRequestMessageProperty.Headers.Add(item.Key, item.Value);
            }

            if (Client.TraceRequestReply)
            {
                Console.WriteLine("### REQUEST");
                request = TraceOutMessage(request, httpRequestMessageProperty);
            }

            return null;
        }

        private static Message TraceOutMessage(System.ServiceModel.Channels.Message request, HttpRequestMessageProperty httpRequestMessageProperty)
        {
            var messageBuffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = messageBuffer.CreateMessage();

            if (httpRequestMessageProperty != null)
            {
                var url = (request.Headers).To.OriginalString;
                Console.WriteLine("URI: " + url);
                Console.WriteLine("Method: " + httpRequestMessageProperty.Method);
            }

            StringBuilder sb = new StringBuilder();
            using (System.IO.TextWriter tw = new System.IO.StringWriter(sb))
            {
                using (System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(tw))
                {
                    xw.Formatting = System.Xml.Formatting.Indented;
                    request.WriteMessage(xw);
                }
            }

            Console.WriteLine(sb.ToString());
            Console.WriteLine();

            request = messageBuffer.CreateMessage();

            return request;
        }
    }

}
