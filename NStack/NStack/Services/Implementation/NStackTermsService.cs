using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace NStack.SDK.Services.Implementation
{
    public class NStackTermsService : INStackTermsService
    {
        private readonly INStackRepository _repository;

        public NStackTermsService(INStackRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<DataWrapper<Terms>> GetNewestTerms(string termsId, string userId, string language)
        {
            var request = new RestRequest($"api/v2/content/terms/{termsId}/versions/newest", Method.GET);
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return await _repository.DoRequest<DataWrapper<Terms>>(request);
        }
    }
}
