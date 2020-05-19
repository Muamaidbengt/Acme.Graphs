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

        public static bool ContainsExactly(this Path path, IEnumerable<DirectedEdge> edges) {
            var needle = Path.Of(edges);
            var idx = 0;
            foreach (var step in path) {
                if (step == needle[idx]) {
                    if (++idx == needle.Count()) {
                        return true;
                    }
                } else { 
                    idx = 0; 
                }
            }
            return false;
        }
    }
}
