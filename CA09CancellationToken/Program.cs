using System.Threading.Tasks;

namespace CA09CancellationToken
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var cancellationTokenSource = new CancellationTokenSource();

            ////var key1 = DoCheck01(cancellationTokenSource);
            //var key1 = await DoCheck01(cancellationTokenSource);
            //Console.WriteLine($"\nYou Entered the key {key1}");

            //var key2 = await DoCheck02(cancellationTokenSource);
            //Console.WriteLine($"\nYou Entered the key {key2}");

            var key3 = await DoCheck03(cancellationTokenSource);
            Console.WriteLine($"\nYou Entered the key {key3}");

            Console.ReadKey();
        }

        static async Task<ConsoleKey> DoCheck01(CancellationTokenSource cancellationTokenSource)
        {
            ConsoleKeyInfo input = default;

            /*await*/
            // Run in background 
            Task.Run(() =>
            {
                do
                {
                    input = Console.ReadKey();

                    if (input.Key == ConsoleKey.Q)
                    {
                        cancellationTokenSource.Cancel();
                        Console.WriteLine("\nTask has been canceled");
                    }
                } while (input.Key != ConsoleKey.Q);
            });

            while (!cancellationTokenSource.Token.IsCancellationRequested) // true if cancellationTokenSource.Cancel() was called
            {
                Console.WriteLine("Checking...");
                await Task.Delay(4000);
                Console.WriteLine($"Completed on {DateTime.Now}\n");
            }

            Console.WriteLine("Check was terminated!");

            //cancellationTokenSource.Dispose(); // moved to Main method with 'using' statement

            return input.Key;
        }

        static async Task<ConsoleKey> DoCheck02(CancellationTokenSource cancellationTokenSource)
        {
            ConsoleKeyInfo input = default; 

            Task.Run(() =>
            {
                do
                {
                    input = Console.ReadKey();

                    if (input.Key == ConsoleKey.Q)
                    {
                        cancellationTokenSource.Cancel();
                        Console.WriteLine("\nTask has been canceled");
                    }
                } while (input.Key != ConsoleKey.Q);
            });


            while (true)
            {
                Console.WriteLine("Checking...");

                // Any asynchronous operation that accepts a CancellationToken can be canceled
                // This Task.Delay method will throw an OperationCanceledException when the cancellationTokenSource is canceled
                // The operation will be canceled immediately, without waiting for the delay to complete
                await Task.Delay(4000, cancellationTokenSource.Token);
                Console.WriteLine($"Completed on {DateTime.Now}\n");
            }

            Console.WriteLine("Check was terminated");

            //cancellationTokenSource.Dispose();

            return input.Key;
        }

        static async Task<ConsoleKey> DoCheck03(CancellationTokenSource cancellationTokenSource)
        {
            ConsoleKeyInfo input = default;

            Task.Run(() =>
            {
                do
                {
                    input = Console.ReadKey();

                    if (input.Key == ConsoleKey.Q)
                    {
                        cancellationTokenSource.Cancel();
                        Console.WriteLine("\nTask has been canceled");
                    }
                } while (input.Key != ConsoleKey.Q);
            });

            try
            {
                while (true)
                {
                    // This will throw an OperationCanceledException when the cancellationTokenSource is canceled
                    // This method (ThrowIfCancellationRequested) will watch on the token and throw the exception when cancellation is requested
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    Console.WriteLine("Checking...");
                    await Task.Delay(4000);
                    Console.WriteLine($"Completed on {DateTime.Now}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            Console.WriteLine("Check was terminated");

            //cancellationTokenSource.Dispose();

            return input.Key;
        }
    }
}
