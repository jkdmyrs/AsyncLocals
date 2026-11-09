namespace AsyncLocals
{
    internal static class BreadMakerAsyncLocal
    {
        // Same idea as BreadMaker, but each async job gets its own values.
        // So parallel jobs don't overwrite each other's data.
        static AsyncLocal<string> _breadType = new();
        static AsyncLocal<int> _bake_time = new();


        public static async Task BakeBread(string breadType, int bakeTime)
        {
            await Task.Delay(Random.Shared.Next(10));

            _breadType.Value = breadType;
            _bake_time.Value = bakeTime;

            // Bake() reads from AsyncLocal values for the current job.
            await Bake();
        }

        private static async Task Bake()
        {
            await Task.Delay(_bake_time.Value);

            Console.WriteLine($"Bread Type: {_breadType.Value}, Bake Time: {_bake_time.Value}");
        }
    }
}
