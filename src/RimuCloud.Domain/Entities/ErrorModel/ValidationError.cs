
namespace RimuCloud.Domain.Entity.ErrorModel
{
    public sealed record ValidationError(IEnumerable<string> Errors);
}
