using System;
using AutoMapper;
using BlogAppCore.Persistence;

namespace BlogAppCore.Application.Tests.Infrastructure
{
    public class CommandTestBase : IDisposable
    {
        protected readonly BlogAppCoreDbContext _context;
        protected readonly IMapper _mapper;

        public CommandTestBase()
        {
            _context = BlogAppCoreContextFactory.Create();
            _mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            BlogAppCoreContextFactory.Destroy(_context);
        }
    }
}