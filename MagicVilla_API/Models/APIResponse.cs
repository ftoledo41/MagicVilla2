using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode{ get; set; }
        public bool IsExitoso { get; set; } = true;
        public List<string> ErroreMessages { get; set; }
        public object Resultado { get; set; } 

    }
}
