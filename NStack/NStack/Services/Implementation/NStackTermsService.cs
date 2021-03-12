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

        public Task<DataWrapper<IEnumerable<TermsEntry>>> GetAllTermsAsync(string language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest("api/v2/content/terms", Method.GET);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequestAsync<DataWrapper<IEnumerable<TermsEntry>>>(request);
        }

        public Task<DataWrapper<IEnumerable<Terms>>> GetTermsVersionsAsync(string termsId, string userId, string language)
        {
            if (termsId == null)
                throw new ArgumentNullException(nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/{termsId}/versions");
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequestAsync<DataWrapper<IEnumerable<Terms>>>(request);
        }

        public Task<DataWrapper<TermsWithContent>> GetNewestTermsAsync(string termsId, string userId, string language)
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

            return _repository.DoRequestAsync<DataWrapper<TermsWithContent>>(request);
        }

        public Task<DataWrapper<TermsWithContent>> GetTermsAsync(int termsId, string userId, string language)
        {
            if (termsId < 0)
                throw new ArgumentException($"Expected an ID of 0 or higher. Got {termsId}", nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/versions/{termsId}", Method.GET);
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequestAsync<DataWrapper<TermsWithContent>>(request);
        }

        public async Task<bool> MarkReadAsync(int termsId, string userId, string language)
        {
            if (termsId < 0)
                throw new ArgumentException($"Expected an ID of 0 or higher. Got {termsId}", nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/versions/views", Method.POST);
            request.AddJsonBody(new
            {
                term_version_id = termsId,
                guid = userId,
                identifier = userId,
                locale = language
            }, "application/x-www-form-urlencoded");
            request.AddHeader("Accept-Language", language);

            var data = await _repository.DoRequestAsync<object>(request);

            return data != null;
        }
    }
}
