using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class WhatsAppApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Body { get; }

        public WhatsAppApiException(HttpStatusCode status, string body)
            : base($"WhatsApp API error {status}: {body}")
        {
            StatusCode = status;
            Body = body;
        }
    }

}
