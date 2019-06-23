using System;
using AutoMapper;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public BlogAppCoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = BlogAppCoreContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public void Dispose()
        {
            BlogAppCoreContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}