using System.Threading.Tasks;

namespace CA08AsyncFunctions
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /*
             * The async and await keywords are used to work with asynchronous programming in C#.
             * The async keyword is used to mark a method as asynchronous, and the await keyword is used to wait for the completion of an asynchronous operation.
             * await does not mean that the thread will have to be blocked waiting for the operation.
             * await means the thread is free to go to do another thing and then he will come back when this operation is done.
             * if we don't use the await keyword with async method the method will not be awaited for the operation to complete.
            */

            /// 1 (Using GetAwaiter and OnCompleted)
            //var task = Task.Run(() => GetDataFormUrl("https://www.scrapingcourse.com/ecommerce/page/1"));
            //var awaiter = task.GetAwaiter();
            //awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult()));
            //Console.WriteLine("Hello, World!");

            /// 2 (Using async and await keywords)
            Console.WriteLine(await GetDataFormUrlAsync("https://www.scrapingcourse.com/ecommerce/page/2"));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Thread Id:{Environment.CurrentManagedThreadId}");
            Console.ResetColor();
            Console.WriteLine("Hello, World!");


            /// 3
            //Console.WriteLine("Main Method Started......");
            //SomeMethod();
            //await SomeMethod();
            //Console.WriteLine("Main Method End");

            Console.ReadKey();
        }

        static Task<string> GetDataFormUrl(string url)
        {
            var stringTask = new HttpClient().GetStringAsync(url);
            return stringTask;
        }

        static async Task<string> GetDataFormUrlAsync(string url)
        {
            var stringTask = new HttpClient().GetStringAsync(url);
            Console.WriteLine("Do Working...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Thread Id:{Environment.CurrentManagedThreadId}");
            Console.ResetColor();

            var result = await stringTask;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Thread Id:{Environment.CurrentManagedThreadId}");
            Console.ResetColor();

            return result;
        }

        async static /*void*/ Task SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

            // if we don’t use the await operator while calling the OperationToWait method, the OperationToWait method is not going to wait for this operation to finish
            //OperationToWait(); // This will not wait for the operation to complete because it is not awaited

            await OperationToWait(); // This will wait for the operation to complete
            Console.WriteLine("Some Method End");
        }

        static async Task OperationToWait()
        {
            await Task.Delay(5000);
            Console.WriteLine("\n5 Seconds wait Completed\n");
        }
    }
}
