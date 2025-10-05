namespace CA01ThreadVsTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The Task data type represents an asynchronous operation.
            // A task is basically a “promise” that the operation to be performed will not necessarily be completed immediately, but that it will be completed in the future.

            /*
             * Difference Between Thread and Task:
               * Thread is low level => More Details, Task is abstraction over Thread => Less Details
               * Thread not using thread pool, Task using thread pool => Performance because thread pool reusable and it's managed by CLR
               * Task is background by default (thread pool), Thread is foreground by default =>  Process will not end until all foreground threads are finished
               * Thread don't return value, Task return value => [Task<T>]
               * Difficult to chain two threads, easy to chain two tasks => [Task<T>.ContinueWith(t => ...)]
               * Exception Propagation using thread is difficult to catch in caller thread, Exception Propagation using task is easy to catch in caller thread
               * Task can be cancelled, thread cannot be cancelled => Save resources
               * Task used for parallelism, Thread used for concurrency
            */

            var thread1 = new Thread(() => Display("Using thread!")); // thread id: 5, is PooledThread: false, is background: false
            thread1.Start();
            thread1.Join(); // wait for the thread to finish

            Task.Run(() => Display("Using task!")).Wait(); // thread id: 6, is PooledThread: true, is background: true 
                                                           // .wait() is used to block the main thread until the task is completed
                                                           // without .wait() the main thread will continue to execute and may finish before the task is completed

            Console.WriteLine("Task is completed!");

            Console.ReadKey();
        }

        static void Display(string message)
        {
            Console.WriteLine($"{message}");

            ShowThreadInfo(Thread.CurrentThread);

            Console.WriteLine(new string('-', 100));
        }

        private static void ShowThreadInfo(Thread currentThread)
        {
            Console.WriteLine($"Thread Id: {currentThread.ManagedThreadId}, Is PooledThread: {currentThread.IsThreadPoolThread}, Is Background: {currentThread.IsBackground}");
        }
    }
}
