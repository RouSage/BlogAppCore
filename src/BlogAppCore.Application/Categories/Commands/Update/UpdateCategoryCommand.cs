using MediatR;

namespace BlogAppCore.Application.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool UpdateSlug { get; set; }
    }
}