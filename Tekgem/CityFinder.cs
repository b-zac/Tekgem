using CitySearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Tekgem
{
    public class CityFinder : ICityFinder
    {
        IDataAccess DataAccess;

        private List<string> filteredCities = new List<string>();

        private List<string> cachedCities = new List<string>();

        private string PreviousSearchString { get; set; }


        public CityFinder(IDataAccess dataAcess)
        {
            DataAccess = dataAcess;
        }

        public CityFinder()
        {
            DataAccess = new DataAccess();
        }

        public ICityResult Search(string searchString)
        {
            if (searchString == "")
                throw new Exception("Please provide input.");

            searchString = searchString.ToUpper();

            if (cachedCities.Count == 0)
            {
                cachedCities = DataAccess.GetCities().ToList();
            }

            ICityResult result = new CityResult
            {
                NextCities = new List<string>(),
                NextLetters = new List<string>(),
            };

            int indexOfLastInputChar = searchString.Length-1;

            if (PreviousSearchString != null && searchString.StartsWith(PreviousSearchString))
            {
                for (int i = filteredCities.Count-1; i >= 0; i--)
                {
                    if (filteredCities[i].StartsWith(searchString))
                    {
                        result.NextCities.Add(filteredCities[i]);

                        if (filteredCities[i].Length > searchString.Length && !result.NextLetters.Contains(filteredCities[i]))
                            result.NextLetters.Add(filteredCities[i][indexOfLastInputChar + 1].ToString());
                    }
                    else
                    {
                        filteredCities.RemoveAt(i);
                    }
                }
            }
            else
            {
                filteredCities.Clear();

                foreach (string city in cachedCities)
                {
                    if (city.StartsWith(searchString))
                    {
                        result.NextCities.Add(city);
                        filteredCities.Add(city);
                        if (city.Length > searchString.Length && !result.NextLetters.Contains(city[indexOfLastInputChar + 1].ToString()))
                            result.NextLetters.Add(city[indexOfLastInputChar + 1].ToString());
                    }
                }
            }

            PreviousSearchString = searchString;

            return result;
        }
    }
}
