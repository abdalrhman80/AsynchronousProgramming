namespace CA10ReportProgress
{
    public class DataProcessor
    {
        public static async Task ProcessLargeDatasetAsync(int itemCount, IProgress<int> progress, CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < itemCount; i++)
            {
                // Check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                // Simulate processing work
                await Task.Delay(100, cancellationToken);

                // Report progress
                int percentComplete = ((i + 1) * 100) / itemCount;
                progress?.Report(percentComplete);
            }
        }

        // Usage example with cancellation
        public static async Task Example()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            // Cancel after 3 seconds
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(3));

            var progress = new Progress<int>(percent =>
            {
                Console.WriteLine($"Processed: {percent}%");
            });

            try
            {
                await ProcessLargeDatasetAsync(100, progress, cancellationTokenSource.Token);
                Console.WriteLine("Processing completed!");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Processing was cancelled.");
            }
        }
    }

}
