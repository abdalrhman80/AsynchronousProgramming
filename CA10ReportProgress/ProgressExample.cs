namespace CA10ReportProgress
{
    public class ProgressExample
    {
        // Async method that reports progress
        public static async Task ProcessDataAsync(IProgress<int> progress)
        {
            for (int i = 0; i <= 100; i += 10)
            {
                // Simulate work
                await Task.Delay(500);

                // Report progress
                progress?.Report(i);
            }
        }

        // Usage example
        public static async Task RunExample()
        {
            var progress = new Progress<int>(percent =>
            {
                Console.WriteLine($"Progress: {percent}%");
            });

            await ProcessDataAsync(progress);
            Console.WriteLine("Complete!");
        }
    }

}