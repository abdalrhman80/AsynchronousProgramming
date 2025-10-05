namespace CA11TaskCombinators
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var has1000SubscriberTask = Task.Run(Has1000Subscriber);
            var has4000ViewHoursTask = Task.Run(Has4000ViewHours);

            Console.WriteLine("Using WhenAny()");
            Console.WriteLine("---------------");

            var any = await Task.WhenAny(has1000SubscriberTask, has4000ViewHoursTask);
            Console.WriteLine($"{any.Result}\n"); // has4000ViewHoursTask is completed first


            var has1000SubscriberTask2 = Task.Run(Has1000Subscriber);
            var has4000ViewHoursTask2 = Task.Run(Has4000ViewHours);

            Console.WriteLine("Using WhenAll()");
            Console.WriteLine("---------------");

            var all = await Task.WhenAll(has1000SubscriberTask2, has4000ViewHoursTask2);

            foreach (var t in all)
            {
                Console.WriteLine(t);
            }

            Console.ReadKey();
        }

        static Task<string> Has1000Subscriber()
        {
            Task.Delay(4000).Wait();
            return Task.FromResult("Congratulation! you have 1000 subscribers");
        }

        static Task<string> Has4000ViewHours()
        {
            Task.Delay(3000).Wait();
            return Task.FromResult("Congratulation! you have 4000 view hours");
        }

        static Task<string> Has1000Subscriber2()
        {
            Task.Delay(4000).Wait();
            return Task.FromResult("Congratulation! you have 1000 subscribers");
        }

        static Task<string> Has4000ViewHours2()
        {
            Task.Delay(3000).Wait();
            return Task.FromResult("Congratulation! you have 4000 view hours");
        }
    }
}