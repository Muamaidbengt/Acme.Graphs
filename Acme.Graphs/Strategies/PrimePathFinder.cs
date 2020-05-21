using Acme.Graphs.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs.Strategies {
    public class PrimePathFinder {
        public IEnumerable<Path> FindPrimePaths(DirectedGraph graph) {
            ArgumentHelpers.ThrowIfNull(() => graph);

            var pathsOfLength = new Dictionary<int, List<List<DirectedEdge>>>();

            var pathsToConsider = new Queue<List<DirectedEdge>>();
            foreach (var edge in graph.Edges) {
                pathsToConsider.Enqueue(new List<DirectedEdge> { edge });
            }

            void markAsCandidate(List<DirectedEdge> path) {
                var length = path.Count();
                if (!pathsOfLength.TryGetValue(length, out var paths)) {
                    paths = new List<List<DirectedEdge>>();
                    pathsOfLength.Add(length, paths);
                }
                paths.Add(path);
            }

            while (pathsToConsider.Any()) {
                var p = pathsToConsider.Dequeue();
                if (p.DescribesCycle()) {
                    markAsCandidate(p);
                    continue;
                }

                var possibleExtensions = graph.EdgesFrom(p.Last().To)
                    .Where(ext => !p.Reaches(ext.To));
                if (!possibleExtensions.Any()) {
                    markAsCandidate(p);
                    continue;
                }

                foreach (var possibleExtension in possibleExtensions) {
                    var next = new List<DirectedEdge>(p);
                    next.Add(possibleExtension);
                    pathsToConsider.Enqueue(next);
                }
            }

            var primePaths = new List<Path>();
            foreach (var possiblyPrime in pathsOfLength.OrderByDescending(kv => kv.Key).SelectMany(kv => kv.Value)) {
                var possiblyPrimePath = Path.Of(possiblyPrime);
                if (primePaths.All(prime => !prime.Contains(possiblyPrimePath))) {
                    primePaths.Add(Path.Of(possiblyPrime));
                }
            }
            return primePaths;
        }
    }
}
