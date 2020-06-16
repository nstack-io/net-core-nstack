using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NStack.Repositories
{
    public interface INStackRepository
    {
        internal Task<T> DoRequest<T>(IRestRequest request, Action<HttpStatusCode> errorHandling = null) where T : class;
    }
}
