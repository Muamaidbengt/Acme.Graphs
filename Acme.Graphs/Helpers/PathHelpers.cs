using Acme.Graphs.Strategies;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs.Helpers {
    public static class PathHelpers {
        public static bool DescribesCycle(this IEnumerable<DirectedEdge> edges) {
            ArgumentHelpers.ThrowIfNull(() => edges);
            return edges.Count() >= 1 && edges.First().From == edges.Last().To;
        }

        public static bool Visits(this IEnumerable<DirectedEdge> edges, NodeIdentity node) {
            ArgumentHelpers.ThrowIfNull(() => edges);
            ArgumentHelpers.ThrowIfNull(() => node);

            return edges.First().From == node || Reaches(edges, node);
        }

        public static bool Reaches(this IEnumerable<DirectedEdge> edges, NodeIdentity node) {
            ArgumentHelpers.ThrowIfNull(() => edges);
            ArgumentHelpers.ThrowIfNull(() => node);
            return edges.Any(e => e.To == node);
        }

        public static bool Contains(this Path path, IEnumerable<DirectedEdge> edges) {
            ArgumentHelpers.ThrowIfNull(() => path);
            ArgumentHelpers.ThrowIfNull(() => edges);

            var other = Path.Of(edges);
            return path.Contains(other);
        }

        public static bool Contains(this Path path, Path other) {
            ArgumentHelpers.ThrowIfNull(() => path);
            ArgumentHelpers.ThrowIfNull(() => other);

            if (path.Length < other.Length) {
                return false;
            } else if (other.Length == 0) {
                return true;
            }

            for (var i = 0; i <= path.Length - other.Length; i++) {
                var j = 0;
                for (j = 0; j < other.Length; j++) {
                    if (path[i + j] != other[j]) {
                        break;
                    }
                }

                if (j == other.Length) {
                    return true;
                }
            }

            return false;
        }

        public static Path Extend(this Path firstPath, Path secondPath, DirectedGraph graph) {
            ArgumentHelpers.ThrowIfNull(() => firstPath);
            ArgumentHelpers.ThrowIfNull(() => secondPath);
            ArgumentHelpers.ThrowIfNull(() => graph);

            var overlap = OverlapFinder.FindOverlap(firstPath, secondPath);
            if (!Path.IsEmpty(overlap)) {
                return overlap;
            }
            var shortestPath = BreadthFirst.FindPath(graph, firstPath.End, node => node == secondPath.Start);
            if (Path.IsEmpty(shortestPath)) {
                return Path.Empty;
            }
            return Path.Join(firstPath, Path.Of(shortestPath.GetRange(1, shortestPath.Length - 2)), secondPath);
        }
    }
}
