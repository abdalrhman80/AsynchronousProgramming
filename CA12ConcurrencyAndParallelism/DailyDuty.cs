namespace CA12ConcurrencyAndParallelism
{
    class DailyDuty(string title)
    {
        public string Title { get; private set; } = title;
        public bool Processed { get; private set; }

        public void Process()
        {
            Console.WriteLine($"TID: {Environment.CurrentManagedThreadId}, " + $"ProcessorId: {Thread.GetCurrentProcessorId()}");
            Task.Delay(100).Wait();
            Processed = true;
        }
    }
}