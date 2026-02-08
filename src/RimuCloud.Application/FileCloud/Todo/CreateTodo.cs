using FluentValidation;
using MapsterMapper;
using Mediator;
using RimuCloud.Abstractions.Logger;
using RimuCloud.Application.Abstractions;
using RimuCloud.Authorizations.CustomResult.Requirements;
using RimuCloud.Domain.Entities.Models;
using RimuCloud.Domain.Interfaces.Repository;
using RimuCloud.Shared.CustomResult;
using RimuCloud.Shared.Request;
using RimuCloud.Shared.Response;

namespace RimuCloud.Application.Todo
{
    public sealed record CreateTodoCommand(CreateTodoRequest todo) : ICommand<Result<TodoDto>>;

    // Validate
    public class TodoCreationValidate : AbstractValidator<CreateTodoCommand>
    {
        public TodoCreationValidate()
        {
            RuleFor(x => x.todo.Title).Length(1, 10);
            RuleFor(x => x.todo.Text).Length(1, 10);
        }
    }

    // Authorizer
    public class TodoCreationAuthorizer : AbstractRequestAuthorizer<CreateTodoCommand>
    {
        public TodoCreationAuthorizer()
        {
        }

        public override void BuildPolicy(CreateTodoCommand req)
        {
            UseRequirement(new TestRequiment
            {
                Pass = req.todo.Pass
                //CourseId = 123,//request.CourseId,
                //UserId = "09"//_currentUserService.UserId
            });

            //UseRequirement(new DatRequiment() { Pass = "abcxyz" });
        }
    }

    public sealed class TodoItemCommandHandler : ICommandHandler<CreateTodoCommand, Result<TodoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        public TodoItemCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async ValueTask<Result<TodoDto>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
        {
            var todoEntity = _mapper.Map<TodoEntity>(command.todo);

            var result = await _unitOfWork.Todo.AddItem(todoEntity, cancellationToken);
            var todoDTO = _mapper.Map<TodoDto>(result);

            _logger.Information($"Todo item created with id: {todoDTO.Id}");

            return Result.Success(todoDTO);
        }
    }
}
