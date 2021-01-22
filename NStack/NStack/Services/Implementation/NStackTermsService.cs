using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
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

        public async Task<DataWrapper<IEnumerable<TermsEntry>>> GetAllTerms(string language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest("api/v2/content/terms", Method.GET);
            request.AddHeader("Accept-Language", language);

            return await _repository.DoRequest<DataWrapper<IEnumerable<TermsEntry>>>(request);
        }

        public async Task<DataWrapper<Terms>> GetNewestTerms(string termsId, string userId, string language)
        {
            if (termsId == null)
                throw new ArgumentNullException(nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/{termsId}/versions/newest", Method.GET);
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return await _repository.DoRequest<DataWrapper<Terms>>(request);
        }
    }
}
