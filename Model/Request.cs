using System;

namespace Vano.Tools.Azure.Model
{
    class Request
    {
        public string Path { get; set; }
        public string Verb { get; set; }
        public string Body { get; set; }
        public string Response { get; set; }

        public Guid Id { get; set; }

        public string DisplayLabel
        {
            get
            {
                return string.Concat(Verb, " - ", Path);
            }
        }
    }
}
