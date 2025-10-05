namespace CA06TaskDelay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main thread id: {Environment.CurrentManagedThreadId}");

            //SleepUsingThread(5000);

            DelayUsingTask(5000);

            Console.ReadKey();
        }

        static void SleepUsingThread(int ms)
        {
            Console.WriteLine("Thread started.");

            Thread.Sleep(ms); // Blocks the current thread for a specified time

            Console.WriteLine($"Thread completed after Sleep {ms}, on thread {Environment.CurrentManagedThreadId}."); // This line executes after the delay
        }

        static void DelayUsingTask(int ms)
        {
            Console.WriteLine("Task started.");

            Task.Delay(ms); // Creates a Task that will complete after a time delay (Logical Delay)
          
            Console.WriteLine($"First Task completed immediately on thread {Environment.CurrentManagedThreadId}."); // This line executes immediately without waiting

            Task.Delay(ms).GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine($"Second Task completed after Delay {ms}, on thread {Environment.CurrentManagedThreadId}."); // This line executes after the delay
            });
        }
    }
}
