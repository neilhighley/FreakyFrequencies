﻿using System;
using System.Collections.Generic;

namespace CommandLibraries
{
    public class CommandProcessor
    {
        private Dictionary<int, int> currentState = new Dictionary<int, int>();

        private Dictionary<int, List<int>> frequencies = new Dictionary<int, List<int>>();//[num,v]

        private Dictionary<int, int> dataFrequencies = new Dictionary<int, int>();//[v,num]

        private Dictionary<int, Action<int, List<int>>> commands;

        public CommandProcessor()
        {
            commands = new Dictionary<int, Action<int, List<int>>>()
                {
                    {
                        1, (v, r) => AddValue(
                            v,
                            r)
                    },
                    {
                        2, (v, r) => RemoveValue(
                            v,
                            r)
                    },
                    {
                        3, (v, r) => QueryValue(
                            v,
                            r)
                    }
                };
        }

        public List<int> ProcessQueries(List<List<int>> queries)
        {
            var results = new List<int>();

            queries.ForEach(
                q =>
                {
                    commands[q[0]](
                        q[1],
                        results);
                });

            return results;
        }


        private void AddValue(int valueToAdd, List<int> results)
        {
            int freq;

            if (currentState.TryGetValue(
                valueToAdd,
                out freq))
            {
                frequencies[freq].Remove(valueToAdd);
                currentState[valueToAdd] = freq + 1;

                if (frequencies.ContainsKey(freq + 1))
                {
                    frequencies[freq + 1].Add(valueToAdd);
                }
                else
                {
                    frequencies[freq + 1] = new List<int> { valueToAdd };
                }
            }
            else
            {
                currentState[valueToAdd] = 1;

                if (frequencies.ContainsKey(freq + 1))
                {
                    frequencies[freq + 1].Add(valueToAdd);
                }
                else
                {
                    frequencies[freq + 1] = new List<int> { valueToAdd };
                }
            }
        }

        private void RemoveValue(int valueToRemove, List<int> results)
        {
            int freq;

            if (currentState.TryGetValue(valueToRemove, out freq))
            {
                frequencies[freq].Remove(valueToRemove);

                currentState[valueToRemove] = freq - 1;

                if (frequencies.ContainsKey(freq - 1))
                {
                    frequencies[freq - 1].Add(valueToRemove);
                }
                else
                {
                    frequencies[freq - 1] = new List<int> { valueToRemove };
                }

            }
        }
        //Check if any integer is present whose frequency is exactly x. If yes, print 1 else 0.
        private void QueryValue(int freqToMatch, List<int> results)
        {
            List<int> values;

            if (frequencies.TryGetValue(
                freqToMatch,
                out values))
            {
                if (values.Count > 0)
                {
                    results.Add(1);
                }
                else
                {
                    results.Add(0);
                }
            }
            else
            {
                results.Add(0);
            }
        }
    }
}
