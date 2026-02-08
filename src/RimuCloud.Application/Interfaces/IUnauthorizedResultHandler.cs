
using RimuCloud.Shared.CustomResult;

namespace RimuCloud.Application.Interfaces
{
    public interface IUnauthorizedResultHandler
    {
        Task<TResponse> Invoke<TResponse>(AuthorizationResult result);
    }
}
