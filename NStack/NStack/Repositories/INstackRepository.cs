using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NStack.Repositories
{
    public interface INStackRepository
    {
        /// <summary>
        /// Perform a request to the NStack backend.
        /// </summary>
        /// <typeparam name="T">The expected type to get from NStack from this request.</typeparam>
        /// <param name="request">The request to NStack.</param>
        /// <param name="errorHandling">Custom error handling.</param>
        /// <returns></returns>
        internal Task<T> DoRequest<T>(IRestRequest request, Action<HttpStatusCode> errorHandling = null) where T : class;
    }
}
