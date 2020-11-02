using System;
using System.Collections.Generic;
using CitySearch;

namespace Tekgem
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";

            ICityFinder cityFinder = new CityFinder(new DataAccess());

            while (input != "EXIT")
            {
                Console.WriteLine("\nEnter name of the city you are looking for.");
                Console.WriteLine("To exit the program, type EXIT.\n");

                input = Console.ReadLine().ToUpper();

                var result = cityFinder.Search(input);

                Console.WriteLine($"You current input: {input}");

                foreach (var e in result.NextLetters)
                    Console.WriteLine(e);

                foreach (var e in result.NextCities)
                    Console.WriteLine(e);
            }

        }
    }
}
