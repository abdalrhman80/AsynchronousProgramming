namespace CA05TaskContinuation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task<int> countPrimesTask = Task.Run(() => CountPrimeNumberInRange(2, 5_000_000));

            /// 1 . Wait for the task to complete
            //Console.WriteLine("Using Result object");
            //Console.WriteLine(countPrimesTask.Result); // bad it blocks the thread
            //Console.WriteLine("Task is completed"); // this line will not be executed until the task is completed

            /// 2 . Use awaiter
            //Console.WriteLine("Using GetAwaiter: TaskAwaiter<Result> class");
            //var awaiter = countPrimesTask.GetAwaiter(); // GetAwaiter() => returns an awaiter object that can be used to wait for the task to complete
            //awaiter.OnCompleted(() =>
            //{
            //    Console.WriteLine(awaiter.GetResult()); // GetResult() => block the thread but task is completed (OnCompleted()!!)
            //});
            //Console.WriteLine("Task is completed"); // this line will be executed before the countPrimesTask


            /// 3. Using ContinueWith
            Console.WriteLine("Using ContinueWith");
            countPrimesTask.ContinueWith(t => Console.WriteLine(t.Result)); // this will not block the thread  
            Console.WriteLine("Task is completed"); // this line will be executed before the thread is not blocked

            Console.ReadKey();
        }

        static int CountPrimeNumberInRange(int lowerBound, int upperBound)
        {
            int count = 0;

            for (int i = lowerBound; i <= upperBound; i++)
            {
                var j = lowerBound;
                var isPrime = true;

                while (j <= (int)Math.Sqrt(i))
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }

                    j++;
                }

                if (isPrime)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
