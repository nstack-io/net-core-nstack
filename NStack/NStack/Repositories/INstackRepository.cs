using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NStack.Repositories
{
    public interface INstackRepository
    {
        internal Task<T> DoRequest<T>(IRestRequest request, Action<HttpStatusCode> errorHandling = null) where T : class;
    }
}
