
namespace RimuCloud.Domain.Entities.ErrorModel
{
    public sealed record ValidationError(IEnumerable<string> Errors);
}
