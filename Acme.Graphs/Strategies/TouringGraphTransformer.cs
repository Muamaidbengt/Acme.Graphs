using Acme.Graphs.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs.Strategies {
    public static class TouringGraphTransformer {
        public static DirectedGraph Transform(DirectedGraph originalGraph, IEnumerable<Path> primePaths,
            NodeIdentity start, NodeIdentity end) {
            ArgumentHelpers.ThrowIfNull(() => originalGraph);
            ArgumentHelpers.ThrowIfNull(() => primePaths);

            var nodes = new List<NodeIdentity> { start, end };
            var edges = new List<DirectedEdge>();
            var nodePathLookup = new Dictionary<NodeIdentity, Path>();
            nodePathLookup.Add(start, Path.Of(start));
            nodePathLookup.Add(end, Path.Of(end));

            foreach (var path in primePaths) {
                var node = NodeIdentity.Of(Describe(path));
                nodes.Add(node);
                nodePathLookup.Add(node, path);
            }

            foreach (var firstPathKey in nodes) {
                if (firstPathKey == end) {
                    continue;
                }
                var firstPath = nodePathLookup[firstPathKey];

                foreach (var secondPathKey in nodes) {
                    if (secondPathKey == firstPathKey || secondPathKey == start) {
                        continue;
                    }
                    var secondPath = nodePathLookup[secondPathKey];

                    var maybeEdge = firstPath.Extend(secondPath, originalGraph);
                    if (Path.IsEmpty(maybeEdge)) {
                        continue;
                    }

                    if (!ContainsAnyOtherPrimePath(primePaths, maybeEdge, firstPath, secondPath)) {
                        edges.Add(DirectedEdge.Between(firstPathKey, secondPathKey));
                    }
                }
            }

            return DirectedGraph.Of(edges, nodes);
        }

        private static bool ContainsAnyOtherPrimePath(IEnumerable<Path> primePaths,
            Path pathToCheck, params Path[] excludedPaths) {
            foreach (var otherPrime in primePaths) {
                if (!excludedPaths.Contains(otherPrime) && pathToCheck.Contains(otherPrime)) {
                    return true;
                }
            }
            return false;
        }

        private static string Describe(Path path) {
            /*return string.Format("{0}: {1}-{2} ({3})", 
                Guid.NewGuid().ToString("N").Substring(0, 8),
                path.Start,
                path.End,
                path.Length); */
            return "P{ " + string.Join("", path.Select(p => p.Identity)) + " }";
        }
    }
}
