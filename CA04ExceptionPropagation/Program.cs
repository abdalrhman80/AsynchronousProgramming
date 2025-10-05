namespace CA04ExceptionPropagation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ///1- Handled exception => Because the exception is thrown in the main thread that handles it
                ///an exception propagates from method to method, up the call stack, until it is caught in the main thread
                //ThrowException();

                ///2- Unhandled exception => Because the main thread responsible for handling the exception, and the exception is thrown in a different thread
                //var th1 = new Thread(ThrowException);
                //th1.Start();
                //th1.Join();

                ///3- Handled exception => Because the exception is thrown in the same thread that handles it
                //var th2 = new Thread(ThrowExceptionWithTryCatch);
                //th2.Start();
                //th2.Join();
                //Console.WriteLine("Main Thread Is Completed!");


                /// 4- Handled exception
                Task.Run(ThrowException).Wait();
            }
            catch
            {
                Console.WriteLine($"Exception Is Thrown in Main: {Environment.CurrentManagedThreadId}!");
            }

            Console.ReadKey();
        }

        static void ThrowException()
        {
            throw new NullReferenceException();
        }

        static void ThrowExceptionWithTryCatch()
        {
            try
            {
                throw new NullReferenceException();
            }
            catch
            {
                Console.WriteLine($"Exception Is Thrown in {Environment.CurrentManagedThreadId}!");
                throw;
            }
        }
    }
}
