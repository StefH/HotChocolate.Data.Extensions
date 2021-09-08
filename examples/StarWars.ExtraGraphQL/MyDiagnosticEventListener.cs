using System;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using Microsoft.Extensions.Logging;

namespace StarWars.ExtraGraphQL
{
    public class MyDiagnosticEventListener : ExecutionDiagnosticEventListener
    {
        private readonly ILogger<MyDiagnosticEventListener> _logger;

        public MyDiagnosticEventListener(ILogger<MyDiagnosticEventListener> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override IDisposable ExecuteRequest(IRequestContext context)
        {
            _logger.LogInformation("ExecuteRequest ---");

            try
            {
                return base.ExecuteRequest(context);
            }
            catch (Exception e)
            {
                string s = e.Message;
                throw;
            }
        }
    }
}