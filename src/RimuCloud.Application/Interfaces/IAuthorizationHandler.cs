
using RimuCloud.Shared.CustomResult;

namespace RimuCloud.Application.Interfaces
{
    public interface IAuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
    {
        Task<AuthorizationResult> Handle(TRequirement requirement, CancellationToken cancellationToken = default);
    }
}