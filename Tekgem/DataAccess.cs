using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Tekgem
{
    class DataAccess : IDataAccess
    {
        public ICollection<string> GetCities()
        {
            string path = $"{Environment.CurrentDirectory}\\worldcities.csv";

            List<string> cities = new List<string>();

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                //csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    cities.Add(fields[0].ToUpper());
                }
            }
            return cities;
        }
    }
}
