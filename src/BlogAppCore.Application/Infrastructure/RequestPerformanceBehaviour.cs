using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BlogAppCore.Application.Infrastructure
{
    public class RequestPerformanceBehaviour<TRequest, TResposne> : IPipelineBehavior<TRequest, TResposne>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResposne> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResposne> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning("BlogAppCore Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                    requestName, _timer.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}
