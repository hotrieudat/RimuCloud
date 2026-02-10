using Mediator;
using RimuCloud.Domain.Entity.ErrorModel;
using System.Diagnostics.CodeAnalysis;


namespace RimuCloud.Application.Abstractions
{
    public interface IValidate : IMessage
    {
        bool IsValid([NotNullWhen(false)] out ValidationError? error);
    }
}
