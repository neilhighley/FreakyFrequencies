using CommandLibraries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace Tests
{

    public class FreqTests
    {

        [Test]
        public void BigDataTests()
        {
            var inputs = readInputResource(bigDataInputResourceName);

            var expectedOutputs = readOutputResource(bigDataOutputResourceName);

            var processor = new CommandProcessor();

            CollectionAssert.AreEqual(expectedOutputs, processor.ProcessQueries(inputs));
        }

        [Test]
        public void TestScenario00()
        {
            var inputs = readInputResource(input00ResourceName);

            var expectedOutputs = readOutputResource(output00ResourceName);

            var processor = new CommandProcessor();

            CollectionAssert.AreEqual(expectedOutputs, processor.ProcessQueries(inputs));
        }

        [Test]
        public void TestScenario01()
        {
            var inputs = readInputResource(input01ResourceName);

            var expectedOutputs = readOutputResource(output01ResourceName);

            var processor = new CommandProcessor();

            CollectionAssert.AreEqual(expectedOutputs, processor.ProcessQueries(inputs));
        }

        [Test]
        public void TestScenario14()
        {
            var inputs = readInputResource(input14ResourceName);

            var expectedOutputs = readOutputResource(output14ResourceName);

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

        private const string bigDataInputResourceName = @"NUnitTestProject1.biginput.txt";
        private const string bigDataOutputResourceName = @"NUnitTestProject1.bigoutput.txt";
        private const string input00ResourceName = @"NUnitTestProject1.TestData.input00.txt";
        private const string output00ResourceName = @"NUnitTestProject1.TestData.output00.txt";
        private const string input01ResourceName = @"NUnitTestProject1.TestData.input01.txt";
        private const string output01ResourceName = @"NUnitTestProject1.TestData.output01.txt";
        private const string input14ResourceName = @"NUnitTestProject1.TestData.input14.txt";
        private const string output14ResourceName = @"NUnitTestProject1.TestData.output14.txt";

        private List<int> readOutputResource(string resourceName)
        {
            var outputs = new List<int>();

            foreach (var line in readResource(resourceName))
            {
                outputs
                    .Add(
                        Int32.Parse(line));
            }

            return outputs;
        }

        private List<List<int>> readInputResource(string resourceName)
        {
            var commandsAndValues = new List<List<int>>();

            foreach (var line in readResource(resourceName))
            {
                var splitLine = line.Split(' ');

                commandsAndValues
                    .Add(splitLine
                        .Select(elem => Int32.Parse(elem))
                        .ToList()
                    );
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