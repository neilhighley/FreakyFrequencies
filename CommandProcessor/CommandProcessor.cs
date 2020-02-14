using System;
using System.Collections.Generic;

namespace CommandLibraries
{
    public class CommandProcessor
    {
        private Dictionary<int, int> currentState = new Dictionary<int, int>();

        private Dictionary<int, HashSet<int>> frequencies = new Dictionary<int, HashSet<int>>();

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
                    if(IsValidQuery(q))
                    commands[q[0]](
                        q[1],
                        results);
                });

            return results;
        }

        private bool IsValidQuery(List<int> query)
        {
            if (query.Count != 2) return false;

            if (query[0] < 1) return false;

            if (query[0] > 3) return false;

            return true;
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
                    frequencies[freq + 1] = new HashSet<int>(){ valueToAdd };
                }
            }
            else
            {
                currentState[valueToAdd] = 1;

                if (frequencies.ContainsKey(1))
                {
                    frequencies[1].Add(valueToAdd);
                }
                else
                {
                    frequencies[1] = new HashSet<int> { valueToAdd };
                }
            }
        }

        private void RemoveValue(int valueToRemove, List<int> results)
        {
            int freq;

            if (currentState.TryGetValue(valueToRemove, out freq))
            {
                if (freq > 0)
                {
                    frequencies[freq].Remove(valueToRemove);

                    if (freq > 1)
                    {
                        currentState[valueToRemove] = freq - 1;
                    }
                    else
                    {
                        currentState.Remove(valueToRemove);
                    }

                    if (freq > 1)
                    {
                        if (frequencies.ContainsKey(freq - 1))
                            frequencies[freq - 1].Add(valueToRemove);
                        else
                            frequencies[freq - 1] = new HashSet<int> {valueToRemove};
                    }
                }
            }    
        }
        //Check if any integer is present whose frequency is exactly x. If yes, print 1 else 0.
        private void QueryValue(int freqToMatch, List<int> results)
        {
            HashSet<int> values;

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
