#nullable disable
namespace RimuCloud.Shared.Request
{
    public record CreateTodoRequest
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string Pass { get; set; }
    }
}
