using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs.Strategies {
    public static class DepthFirst {
        public static IEnumerable<NodeIdentity> VisitAll(DirectedGraph graph, NodeIdentity start) {
            ArgumentHelpers.ThrowIfNull(() => graph);
            ArgumentHelpers.ThrowIfNull(() => start);

            var unvisited = new Stack<NodeIdentity>();
            var spotted = new HashSet<NodeIdentity>();

            void addNeighbors(IEnumerable<DirectedEdge> edges) {
                foreach (var neighbor in edges) {
                    if (spotted.Add(neighbor.To)) {
                        unvisited.Push(neighbor.To);
                    }
                }
            }

            unvisited.Push(start);

            while (unvisited.Any()) {
                var current = unvisited.Pop();
                yield return current;

                addNeighbors(graph.EdgesFrom(current));
            }
        }
    }
}
