using CitySearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tekgem.Tests
{
    class MockDataAcess : IDataAccess
    {
        string[] MockData = { "BANDUNG", "BANGUI", "BANGKOK", "BANGALORE", "LA PAZ", "LA PLATA", "LAGOS", "LEEDS", "ZARIA", "ZHUGHAI", "ZIBO" };

        public ICollection<string> GetCities()
        {
            return MockData;
        }
    }

    [TestClass]
    public class CityFinderTest
    {
        [TestMethod]
        public void Search_ForBang_Return3CitiesWith3NextLetters()
        {
            string[] expectedCities = { "BANGUI", "BANGKOK", "BANGALORE" };
            string[] expectedLetters = { "U", "K", "A" };

            ICityFinder cityFinder = new CityFinder(new MockDataAcess());

            var result = cityFinder.Search("Bang");

            CollectionAssert.AreEqual(expectedLetters, result.NextLetters.ToList());
            CollectionAssert.AreEqual(expectedCities, result.NextCities.ToList());
        }

        [TestMethod]
        public void Search_ForLa_Return3CitiesWith2NextLetters()
        {
            string[] expectedCities = { "LA PAZ", "LA PLATA", "LAGOS" };
            string[] expectedLetters = { " ", "G" };

            ICityFinder cityFinder = new CityFinder(new MockDataAcess());

            var result = cityFinder.Search("La");

            CollectionAssert.AreEqual(expectedLetters, result.NextLetters.ToList());
            CollectionAssert.AreEqual(expectedCities, result.NextCities.ToList());
        }

        [TestMethod]
        public void Search_ForZe_ReturnNoCitesAndNoLetters()
        {
            string[] expectedCities = { };
            string[] expectedLetters = { };

            ICityFinder cityFinder = new CityFinder(new MockDataAcess());

            var result = cityFinder.Search("Ze");

            CollectionAssert.AreEqual(expectedLetters, result.NextLetters.ToList());
            CollectionAssert.AreEqual(expectedCities, result.NextCities.ToList());
        }

        [TestMethod]
        public void Search_ForBangkok_ReturnBangkokWithNoLetters()
        {
            string[] expectedCities = { "BANGKOK" };
            string[] expectedLetters = { };

            ICityFinder cityFinder = new CityFinder(new MockDataAcess());

            var result = cityFinder.Search("Bangkok");

            CollectionAssert.AreEqual(expectedLetters, result.NextLetters.ToList());
            CollectionAssert.AreEqual(expectedCities, result.NextCities.ToList());
        }

        [TestMethod]
        public void Search_WithEmptyInput_ThrowException()
        {
            string[] expectedCities = { };
            string[] expectedLetters = { };

            ICityFinder cityFinder = new CityFinder(new MockDataAcess());

            Action result = () => cityFinder.Search("");

            Assert.ThrowsException<Exception>(result);
        }
}
