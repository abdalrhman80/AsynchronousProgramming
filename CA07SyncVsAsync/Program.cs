namespace CA07SyncVsAsync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * Synchronous: 
                * Operations are executed sequentially, and the calling thread waits (blocks) until the operation completes before moving to the next line of code.
                * The thread is blocked during long-running operations (e.g., file I/O, network requests), which can lead to performance bottlenecks or UI freezes in applications
                * Suitable for short, quick tasks or when simplicity is prioritized over performance (CPU-bound tasks)
             
             * Asynchronous:
                * Operations are executed without blocking the calling thread, allowing it to perform other tasks while waiting for the operation to complete.
                * Improves responsiveness and scalability, especially for I/O-bound tasks (e.g., database calls, HTTP requests).
                * Non-blocking, the thread is freed to do other work while the task runs in the background.
                * Use async/await for I/O-bound work: Avoid blocking with .Result or .Wait(), as it can cause deadlocks.
            */

            ShowThreadInfo(Thread.CurrentThread, 20);
            CallSynchronous();

            ShowThreadInfo(Thread.CurrentThread, 23);
            CallAsynchronous();

            ShowThreadInfo(Thread.CurrentThread, 26);

            /*
                Line#: 20, Thread Id: 1, IsPooled: False, IsBackground: False
                Line#: 33, Thread Id: 1, IsPooled: False, IsBackground: False
                ++++++++++ Synchronous +++++++++++
                Line#: 23, Thread Id: 1, IsPooled: False, IsBackground: False
                Line#: 39, Thread Id: 1, IsPooled: False, IsBackground: False
                Line#: 26, Thread Id: 1, IsPooled: False, IsBackground: False
                Line#: 42, Thread Id: 6, IsPooled: True, IsBackground: True
                ++++++++++ Asynchronous +++++++++++
            */

            Console.ReadKey();
        }

        static void CallSynchronous()
        {
            Thread.Sleep(5000);
            ShowThreadInfo(Thread.CurrentThread, 33);
            Task.Run(() => Console.WriteLine("++++++++++ Synchronous +++++++++++")).Wait();
        }

        static void CallAsynchronous()
        {
            ShowThreadInfo(Thread.CurrentThread, 39);
            Task.Delay(5000).GetAwaiter().OnCompleted(() =>
            {
                ShowThreadInfo(Thread.CurrentThread, 42);
                Console.WriteLine("++++++++++ Asynchronous +++++++++++");
            });
        }


        private static void ShowThreadInfo(Thread th, int line)
        {
            Console.WriteLine($"Line#: {line}, Thread Id: {th.ManagedThreadId}, IsPooled: {th.IsThreadPoolThread}, IsBackground: {th.IsBackground}");
        }
    }
}
