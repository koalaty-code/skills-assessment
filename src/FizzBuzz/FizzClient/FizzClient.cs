using System;
using Autofac;
using FizzClient.DependencyModule;
using FizzLib.Services;

namespace FizzClient
{
    public class FizzClient
    {
        static void Main(string[] args)
        {
            var container = Bootstrapper.Bootstrap();

            var fizzBuzz = container.Resolve<IFizzBuzz>();

            writeRange(1, 100);
            fizzBuzz.GetFizzBuzz();

            writeRange(1, int.MaxValue);
            fizzBuzz.GetFizzBuzz(max: int.MaxValue);

            writeRange(int.MinValue, int.MaxValue);
            fizzBuzz.GetFizzBuzz(int.MinValue, int.MaxValue);

            Console.WriteLine("Press any key to quit.");
            Console.ReadLine();
        }

        private static void writeRange(int min, int max)
        {
            Console.WriteLine($"min: {min}, max: {max}");
        }
    }
}
