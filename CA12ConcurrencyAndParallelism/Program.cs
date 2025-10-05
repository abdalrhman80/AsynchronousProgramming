namespace CA12ConcurrencyAndParallelism
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var duties = new List<DailyDuty>
            {
                new("Cleaning House"),
                new("Washing Dishes"),
                new("Doing Laundry"),
                new("Preparing Meals"),
                new("Checking Emails"),
                new("Cleaning House")
            };

            Console.WriteLine("Using Concurrent Processing");
            Console.WriteLine(new string('-', 30));
            await ProcessThingsInConcurrent(duties);
            /*
               Using Concurrent Processing
               ------------------------------
               TID: 1, ProcessorId: 2
               TID: 1, ProcessorId: 2
               TID: 1, ProcessorId: 2
               TID: 1, ProcessorId: 2
               TID: 1, ProcessorId: 2
               TID: 1, ProcessorId: 2
            */

            Console.WriteLine("\n" + new string('-', 100) + "\n");

            Console.WriteLine("Using Parallel Processing");
            Console.WriteLine(new string('-', 30));
            await ProcessThingsInParallel(duties);
            /*
               Using Parallel Processing
               ------------------------------
               TID: 1, ProcessorId: 2
               TID: 6, ProcessorId: 0
               TID: 4, ProcessorId: 4
               TID: 9, ProcessorId: 5
               TID: 12, ProcessorId: 3
               TID: 10, ProcessorId: 4
            */

            Console.ReadKey();
        }

        static Task ProcessThingsInConcurrent(IEnumerable<DailyDuty> duties)
        {
            foreach (var duty in duties)
            {
                duty.Process();
            }
            return Task.CompletedTask;
        }

        static Task ProcessThingsInParallel(IEnumerable<DailyDuty> duties)
        {
            Parallel.ForEach(duties, duty => duty.Process());
            return Task.CompletedTask;
        }
    }
}