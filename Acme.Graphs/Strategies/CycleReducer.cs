using Acme.Graphs.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs.Strategies {
    public static class CycleReducer {
        public static (DirectedGraph GraphWithoutCycles, IDictionary<NodeIdentity, Path> Cycles) RemoveCycles(DirectedGraph graphWithCycles) {
            var cycles = new Dictionary<NodeIdentity, Path>();
            var edges = graphWithCycles.Edges.ToList();
            var nodes = graphWithCycles.Nodes.ToHashSet();
            var cycle = FindFirstCycle(edges);
            while (!Path.IsEmpty(cycle)) {
                var cycleEdges = FindEdgesLeadingIntoOrOutOf(cycle, edges);
                var substituteNode = CreateSubstituteNode(cycle);
                cycles.Add(substituteNode, cycle);
                foreach (var node in cycle) {
                    nodes.Remove(node);
                }

                nodes.Add(substituteNode);
                foreach (var edge in cycleEdges.Into) {
                    edges.Remove(edge);
                    edges.Add(new DirectedEdge(edge.From, substituteNode));
                }
                foreach (var edge in cycleEdges.OutOf) {
                    edges.Remove(edge);
                    edges.Add(new DirectedEdge(substituteNode, edge.To));
                }
                foreach (var edge in cycleEdges.Inside) {
                    edges.Remove(edge);
                }
                cycle = FindFirstCycle(edges);
            }
            return (DirectedGraph.Of(edges, nodes), cycles);
        }

        private static NodeIdentity CreateSubstituteNode(Path cycle) {
            return NodeIdentity.Of("Substituted cycle: " + cycle.GetRefId().ToString());
        }

        private static Path FindFirstCycle(IEnumerable<DirectedEdge> edges) {
            var edgesByStart = edges.GroupBy(e => e.From).ToDictionary(kv => kv.Key, kv => kv.ToList());

            var pathsToConsider = new Queue<List<DirectedEdge>>();
            foreach (var edge in edges) {
                pathsToConsider.Enqueue(new List<DirectedEdge> { edge });
            }

            while (pathsToConsider.Any()) {
                var p = pathsToConsider.Dequeue();
                if (p.DescribesCycle()) {
                    return Path.Of(p);
                }

                if (edgesByStart.TryGetValue(p.Last().To, out var continuations)) {
                    var possibleExtensions = continuations
                        .Where(ext => !p.Reaches(ext.To));
                    foreach (var possibleExtension in possibleExtensions) {
                        var next = new List<DirectedEdge>(p);
                        next.Add(possibleExtension);
                        pathsToConsider.Enqueue(next);
                    }
                }
            }
            return Path.Empty;
        }

        private static (IEnumerable<DirectedEdge> Into,
            IEnumerable<DirectedEdge> OutOf,
            IEnumerable<DirectedEdge> Inside) FindEdgesLeadingIntoOrOutOf(Path cycle, IEnumerable<DirectedEdge> edges) {
            var into = new List<DirectedEdge>();
            var outOf = new List<DirectedEdge>();
            var inside = new List<DirectedEdge>();
            var nodes = cycle.ToHashSet();

            foreach (var edge in edges) {
                var startsInside = nodes.Contains(edge.From);
                var endsInside = nodes.Contains(edge.To);
                if (startsInside && endsInside) {
                    inside.Add(edge);
                } else if (!startsInside && endsInside) {
                    into.Add(edge);
                } else if (startsInside && !endsInside) {
                    outOf.Add(edge);
                }
            }
            return (into, outOf, inside);
        }
    }
}
