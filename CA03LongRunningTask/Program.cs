namespace CA03LongRunningTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * you can distinguish between a short-running and a long-running threads based on their startup time and execution time.
             * start up time: Thread require (memory + cpu time) to start
             * startup-time > execution-time => short-running thread so use thread pool
             * execution-time > startup-time => long-running thread so don't use thread pool
            */

            // The task is marked as long-running, so it will not use a thread pool thread
            // you can use TaskCreationOptions.LongRunning to indicate that the task is expected to be long-running.
            Task.Factory.StartNew(RunLongTask, TaskCreationOptions.LongRunning); // Thread Id: 7, Is PooledThread: False, Is Background: True

            Task.Run(RunLongTask); // Thread Id: 7, Is PooledThread: True, Is Background: True

            Console.ReadKey();
        }

        static void RunLongTask()
        {
            Thread.Sleep(3000); // Simulate a long-running task

            ShowThreadInfo(Thread.CurrentThread);

            Console.WriteLine("Long-running task completed.");
        }

        private static void ShowThreadInfo(Thread currentThread)
        {
            Console.WriteLine($"Thread Id: {currentThread.ManagedThreadId}, Is PooledThread: {currentThread.IsThreadPoolThread}, Is Background: {currentThread.IsBackground}");
        }
    }
}
