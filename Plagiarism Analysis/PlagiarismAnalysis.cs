using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class PlagiarismAnalysis
    {
        #region YOUR CODE IS HERE
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given an UNDIRECTED Graph of submission IDs and matching pairs with their similarity scores (%), calculate the average similarity percentage of the component containing the given "startVertex"
        /// </summary>
        /// <param name="vertices">array of submission IDs</param>
        /// <param name="edges">array of matching pairs and their similarity score</param>
        /// <param name="startVertex">start vertex to analyze its component</param>
        /// <returns>average similarity score (%) of the component containing the startVertex</returns>
        public static float AnalyzeMatchingScore(string[] vertices, Tuple<string, string, float>[] edges, string startVertex)
        {
            // Build adjacency list and set of visited vertices
            var adjList = new Dictionary<string, List<Tuple<string, float>>>();
            var visited = new HashSet<string>();
            foreach (var vertex in vertices)
            {
                adjList[vertex] = new List<Tuple<string, float>>();
                visited.Add(vertex);
            }
            foreach (var edge in edges)
            {
                adjList[edge.Item1].Add(new Tuple<string, float>(edge.Item2, edge.Item3));
                adjList[edge.Item2].Add(new Tuple<string, float>(edge.Item1, edge.Item3));
            }

            // Find connected component containing start vertex using DFS
            var component = new HashSet<string>();
            var stack = new Stack<string>();
            stack.Push(startVertex);
            visited.Remove(startVertex);
            while (stack.Count > 0)
            {
                var vertex = stack.Pop();
                component.Add(vertex);
                foreach (var neighbor in adjList[vertex])
                {
                    if (visited.Contains(neighbor.Item1))
                    {
                        stack.Push(neighbor.Item1);
                        visited.Remove(neighbor.Item1);
                    }
                }
            }

            // Calculate average similarity score
            var totalScore = 0f;
            var numPairs = 0;
            foreach (var vertex in component)
            {
                foreach (var neighbor in adjList[vertex])
                {
                    if (component.Contains(neighbor.Item1))
                    {
                        totalScore += neighbor.Item2;
                        numPairs++;
                    }
                }
            }
            return totalScore / numPairs;
        }

        #endregion
    }
}
