using MediatR;

namespace BlogAppCore.Application.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest
    {
        public string Name { get; set; }
    }
}