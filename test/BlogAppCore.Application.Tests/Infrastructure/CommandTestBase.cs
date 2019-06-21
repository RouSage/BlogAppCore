using System;
using BlogAppCore.Persistence;

namespace BlogAppCore.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly BlogAppCoreDbContext _context;

        public CommandTestBase()
        {
            _context = BlogAppCoreContextFactory.Create();
        }

        public void Dispose()
        {
            BlogAppCoreContextFactory.Destroy(_context);
        }
    }
}