using NStack.SDK.Models;
using System;
using System.Threading.Tasks;

namespace NStack.SDK.Services
{
    public interface INStackTermsService
    {
        /// <summary>
        /// Get the latest version of the terms with the given <paramref name="termsId"/>.
        /// </summary>
        /// <param name="termsId">The ID of the terms to fetch</param>
        /// <param name="userId">The unique ID of the user who is going to read the terms.</param>
        /// <param name="language">The ISO language code to get the terms in e.g. en-GB.</param>
        /// <exception cref="ArgumentNullException"></exception>
        Task<DataWrapper<Terms>> GetNewestTerms(string termsId, string userId, string language);
    }
}
