using System.Collections.Immutable;
using NUnit.Framework;
using System.Linq;
using CommandLibraries;
using System.Collections.Generic;

namespace Tests
{
  
    public class FreqTests
    {

        [Test]
        public void Test1()
        {
            var input = new List<List<int>>{
                    new List<int>() { 1, 1 },
                    new List<int>() { 2, 2 },
                    new List<int>() { 3, 2 },
                    new List<int>() { 1, 1 },
                    new List<int>() { 1, 1 },
                    new List<int>() { 2, 1 },
                    new List<int>() { 3, 2 }
                };

            var expOutput = new List<int> { 0, 1 };

            var processor = new CommandProcessor();

            CollectionAssert.AreEqual(expOutput, processor.ProcessQueries(input));
        }
    }
}