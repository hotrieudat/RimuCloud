
namespace RimuCloud.Application.Interfaces
{
    public interface IAuthorizer<T>
    {
        IEnumerable<IAuthorizationRequirement> Requirements { get; }
        void ClearRequirements();
        void BuildPolicy(T instance);
    }
}
