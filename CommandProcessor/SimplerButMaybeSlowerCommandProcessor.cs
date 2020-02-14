using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLibraries
{
    public class SimplerButMaybeSlowerCommandProcessor
    {
        private Dictionary<int, int> currentState = new Dictionary<int, int>();

        private Dictionary<int, Action<int, List<int>>> commands;

        public SimplerButMaybeSlowerCommandProcessor()
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
                currentState[valueToAdd] = freq + 1;
            }
            else
            {
                currentState[valueToAdd] = 1;
            }
        }

        private void RemoveValue(int valueToRemove, List<int> results)
        {
            int freq;

            if (currentState.TryGetValue(valueToRemove, out freq))
            {
                currentState[valueToRemove] = freq - 1;
            }
        }
        //Check if any integer is present whose frequency is exactly x. If yes, print 1 else 0.
        private void QueryValue(int freqToMatch, List<int> results)
        {
            if (currentState.Values.Any(freq => freq.Equals(freqToMatch)))
            {
                results.Add(1);
            }
            else
            {
                results.Add(0);
            }
        }
    }
}