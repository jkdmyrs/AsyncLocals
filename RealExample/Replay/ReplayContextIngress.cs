namespace AsyncLocals.RealExample.Replay
{
    internal static class ReplayContextIngress
    {
        public static ReplayContext Capture(string replayId, string replayOf)
        {
            var context = new ReplayContext
            {
                ReplayId = replayId,
                ReplayOf = replayOf
            };
            ReplayContext.Current = context;
            return context;
        }
    }
}
