using AsyncLocals.RealExample.Audit;

namespace AsyncLocals.RealExample.Application
{
    internal class ExampleHandler
    {
        private readonly AuditHandler _auditHandler;

        public ExampleHandler(AuditHandler auditHandler)
        {
            _auditHandler = auditHandler;
        }

        public async Task ExecuteAsync(string eventData)
        {
            await Task.Delay(Random.Shared.Next(1000));
            await _auditHandler.LogAuditRecordAsync(eventData);
        }
    }
}
