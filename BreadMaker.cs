namespace AsyncLocals
{
    internal static class BreadMaker
    {
        // These values are shared by every bake job.
        static string _breadType = string.Empty;
        static int _bake_time = 0;


        public static async Task BakeBread(string breadType, int bakeTime)
        {
            await Task.Delay(Random.Shared.Next(10));

            _breadType = breadType;
            _bake_time = bakeTime;

            // Bake() reads from the shared values above.
            await Bake();
        }

        private static async Task Bake()
        {
            await Task.Delay(_bake_time);

            // If two jobs overlap, one job can change these values while the other job is waiting.
            // That can cause the output to show the wrong bread/time pair.
            Console.WriteLine($"Bread Type: {_breadType}, Bake Time: {_bake_time}");
        }
    }
}
