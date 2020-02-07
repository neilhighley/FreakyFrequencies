using System;
using System.Collections.Immutable;
using NUnit.Framework;
using System.Linq;
using CommandLibraries;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Tests
{
  
    public class FreqTests
    {

        [Test]
        public void BigDataTests()
        {
            var inputs = readInputResource();

            var expectedOutputs = readOutputResource();

            var processor = new CommandProcessor();

            CollectionAssert.AreEqual(expectedOutputs, processor.ProcessQueries(inputs));
        }

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

        private const string bigDataInputResourceName = @"NUnitTestProject1.input07.txt";
        private const string bigDataOutputResourceName = @"NUnitTestProject1.output07.txt";

        private List<int> readOutputResource()
        {
            var outputs = new List<int>();

            foreach (var line in readResource(bigDataOutputResourceName))
            {
                outputs
                    .Add(
                        Int32.Parse(line));
            }

            return outputs;
        }

        private List<List<int>> readInputResource()
        {
            var commandsAndValues = new List<List<int>>();

            foreach (var line in readResource(bigDataInputResourceName))
            {
                var splitLine = line.Split(' ');

                commandsAndValues
                    .Add(new List<int>
                    {
                        Int32.Parse(splitLine[0]),
                        Int32.Parse(splitLine[1])
                    });
            }

            return commandsAndValues;
        }

        private IEnumerable<string> readResource(string resourceName)
        {
            var resourceContents = new List<string>();

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        resourceContents.Add(line);
                    }
                }
            }

            return resourceContents.AsReadOnly();
        }
    }
}