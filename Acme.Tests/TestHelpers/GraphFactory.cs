using Acme.Graphs;
using System.Collections.Generic;

namespace Acme.Tests.TestHelpers {
    static class GraphFactory {
        public static DirectedGraph BuildGraph(params string[] edgeDescriptions) {
            var edges = new List<DirectedEdge>();
            var nodes = new HashSet<NodeIdentity>();

            foreach(var description in edgeDescriptions) {
                if (description.Contains("-")) {
                    var edge = CreateEdge(description);
                    edges.Add(edge);
                    nodes.Add(edge.From);
                    nodes.Add(edge.To);
                } else {
                    nodes.Add(CreateNode(description));
                }
            }
            return DirectedGraph.Of(edges, nodes);
        }

        public static DirectedEdge CreateEdge(string description) {
            var parts = description.Split("-");
            var from = CreateNode(parts[0]);
            var to = CreateNode(parts[1]);
            return DirectedEdge.Between(from, to);
        }

        public static NodeIdentity CreateNode(string description) {
            return NodeIdentity.Of(description);
        }

        public static IEnumerable<DirectedEdge> CreateEdges(params string[] descriptions) {
            foreach (var d in descriptions) {
                yield return CreateEdge(d);
            }
        }

        public static IEnumerable<NodeIdentity> CreateNodes(params string[] descriptions) {
            foreach (var d in descriptions) {
                yield return CreateNode(d);
            }
        }
    }
}
