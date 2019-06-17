using System;

namespace BlogAppCore.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; private set; }

        public DateTime Created { get; private set; } = DateTime.UtcNow;
    }
}