namespace CA10ReportProgress
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
             * When working with long-running asynchronous operations in C#, it's important to provide feedback to users about the progress of the operation. 
             * Here's how to effectively report progress from async methods.
               * The recommended approach is to use the IProgress<T> interface along with the Progress<T> class: 
                 * IProgress<T> Interface: Provides a callback mechanism for reporting progress updates. 
                                           The type parameter T defines what information is reported (commonly int for percentages, but can be any type).
                 * Progress<T> Class: The concrete implementation that captures the current synchronization context,
                                      ensuring progress callbacks execute on the original thread (important for UI updates).

               * Best Practices:
                 * Make progress optional: Always use progress?.Report() with null-conditional operator to allow callers to pass null if they don't need progress updates
                 * Don't report too frequently: Excessive progress updates can impact performance, especially in UI applications. Consider throttling updates
                 * Use meaningful progress data: Create custom classes when you need to report more than just a percentage
                 * Consider cancellation: Often progress reporting goes hand-in-hand with cancellation support via CancellationToken
                 * Keep callbacks lightweight: Progress callbacks should be fast since they may execute on the UI thread
            */

            /// Example of reporting progress using Action<T>
            Console.WriteLine("Example of reporting progress using Action<T>");
            Action<int> progress = (percent) =>
            {
                //Console.Clear();
                Console.WriteLine($"Progress Now: {percent}%");
            };

            await Copy(progress);
            Console.WriteLine("\nCopy completed!");


            Console.WriteLine(new string('-', 100) + '\n');


            /// Example of reporting progress using IProgress<T>
            Console.WriteLine("Example of reporting progress using IProgress<T>");
            await ProgressExample.RunExample();


            Console.WriteLine(new string('-', 100) + '\n');


            /// Example of reporting progress with cancellation support
            Console.WriteLine("Example of reporting progress with cancellation support");
            await DataProcessor.Example();


            Console.ReadKey();
        }

        static Task Copy(Action<int> onProgressPercentChanged)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i <= 100; i += 10)
                {
                    //Task.Delay(50).Wait();
                    Task.Delay(500).Wait();

                    //if (i % 10 == 0)
                    //{
                    onProgressPercentChanged(i);
                    //}
                }
            });
        }
    }
}