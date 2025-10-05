using System.Threading.Tasks;

namespace CA02TaskReturnsValue
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Main Thread Id: {Environment.CurrentManagedThreadId}\n");

            Task<DateTime> task = Task.Run(GetDateTime);
            Console.WriteLine("1=>" + task.Result + "\n"); // task.Result => This will block the main thread until the task is completed

            Task<DateTime> task2 = Task.Run(GetDateTime);
            Console.WriteLine("2=>" + task2.GetAwaiter().GetResult() + "\n"); // This will block the main thread until the task is completed

            PrintGetDateTimeAsync();
            Console.WriteLine("After PrintGetDateTimeAsync");

            Console.ReadKey();
        }

        static DateTime GetDateTime()
        {
            Console.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
            return DateTime.Now;
        }

        static async Task<DateTime> GetDateTimeAsync()
        {
            Console.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");
            await Task.Delay(2000);
            return DateTime.Now;
        }

        static async Task PrintGetDateTimeAsync()
        {
            DateTime task3 = await Task.Run(GetDateTimeAsync); // await => This will not block the main thread, it will return the control to the caller until the task is completed
            Console.WriteLine("3=>" + task3);
        }
    }
}
