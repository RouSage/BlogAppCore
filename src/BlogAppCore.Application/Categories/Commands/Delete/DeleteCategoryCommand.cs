using MediatR;

namespace BlogAppCore.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}