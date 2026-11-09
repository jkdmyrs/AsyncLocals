using AsyncLocals.RealExample.Replay;

namespace AsyncLocals.RealExample.Audit
{
    internal class AuditHandler
    {
        public async Task LogAuditRecordAsync(string eventData)
        {
            await Task.Delay(100);
            Console.WriteLine($"Event: {eventData}, ReplayId: {ReplayContext.Current.ReplayId}, ReplayOf: {ReplayContext.Current.ReplayOf}");
        }
    }
}
