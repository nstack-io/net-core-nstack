using NStack.SDK.Models;
using System;
using System.Threading.Tasks;

namespace NStack.SDK.Services
{
    public interface INStackAppService
    {
        Task AppOpen(NStackPlatform platform, string version, Guid userId, string environment = "production", bool developmentEnvironment = false, bool productionEnvironment = true);
    }
}
