using System.Collections.Generic;

namespace Acme.Graphs {
    public class DirectedGraph {

        private readonly HashSet<DirectedEdge> _edges;
        private readonly HashSet<NodeIdentity> _nodes = new HashSet<NodeIdentity>();
        DirectedGraph(IEnumerable<DirectedEdge> edges, IEnumerable<NodeIdentity> nodes) {
            ArgumentHelpers.ThrowIfNull(() => edges);
            ArgumentHelpers.ThrowIfNull(() => nodes);

            foreach (var node in nodes) {
                if (!_nodes.Add(node)) {
                    throw new DuplicateNodeException(nameof(nodes));
                }
            }

            _edges = new HashSet<DirectedEdge>(edges);
        }

        public int EdgeCount => _edges.Count;

        public static DirectedGraph Of(IEnumerable<DirectedEdge> edges, IEnumerable<NodeIdentity> nodes) 
            => new DirectedGraph(edges, nodes);
    }
}
