using System;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using Microsoft.Extensions.Logging;

namespace StarWars.ExtraGraphQL
{
    public class MyDiagnosticEventListener : DiagnosticEventListener
    {
        private readonly ILogger _logger;

        public MyDiagnosticEventListener(ILogger<MyDiagnosticEventListener> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override IActivityScope ExecuteRequest(IRequestContext context)
        {
            _logger.LogInformation("ExecuteRequest ---");
            return base.ExecuteRequest(context);
        }
    }
}