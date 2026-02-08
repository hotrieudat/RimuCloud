using RimuCloud.Application.Interfaces;
using RimuCloud.Shared.CustomResult;

namespace RimuCloud.Authorizations.CustomResult.Requirements
{
    public class TestRequiment : IAuthorizationRequirement
    {
        // this is input
        public string Pass { get; set; } = default!;

        class TestRequimentHandler : IAuthorizationHandler<TestRequiment>
        {
            //private readonly IRepositoryManager _repositoryManager;

            //private readonly ICurrentUser _currentUser;

            public TestRequimentHandler(/*ICurrentUser currentUser, IRepositoryManager repositoryManager*/)
            {
                //_currentUser = currentUser;
                //_repositoryManager = repositoryManager;
            }


            public Task<AuthorizationResult> Handle(TestRequiment request, CancellationToken cancellationToken)
            {
                // actual use _repositoryManager.IsAbcxyz(Pass)
                if (request.Pass == "1111")
                {
                    return Task.FromResult(AuthorizationResult.Succeed());
                }
                return Task.FromResult(AuthorizationResult.Fail(new Error("ERR_00001", "Authorizor Failed")));
            }
        }
    }
}
