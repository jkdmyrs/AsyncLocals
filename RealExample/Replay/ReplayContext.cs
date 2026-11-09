namespace AsyncLocals.RealExample.Replay
{
    internal class ReplayContext
    {
        public string ReplayId { get; set; } = string.Empty;
        public string ReplayOf { get; set; } = string.Empty;

        private static readonly AsyncLocal<ReplayContext> _current = new();

        public static ReplayContext Current
        {
            get => _current.Value ??= new ReplayContext();
            set => _current.Value = value;
        }
    }
}
