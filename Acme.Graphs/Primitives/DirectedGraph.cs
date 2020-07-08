using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs {
    public class DirectedGraph {

        private readonly Dictionary<NodeIdentity, List<DirectedEdge>> _edges = new Dictionary<NodeIdentity, List<DirectedEdge>>();
        private readonly HashSet<NodeIdentity> _nodes = new HashSet<NodeIdentity>();
        DirectedGraph(IEnumerable<DirectedEdge> edges, IEnumerable<NodeIdentity> nodes) {
            ArgumentHelpers.ThrowIfNull(() => edges);
            ArgumentHelpers.ThrowIfNull(() => nodes);

            foreach (var node in nodes) {
                if (!_nodes.Add(node)) {
                    throw new DuplicateNodeException(nameof(nodes));
                }
            }

            _edges = edges.GroupBy(e => e.From)
                .ToDictionary(kv => kv.Key, kv => kv.ToList());
        }

        public IEnumerable<DirectedEdge> EdgesFrom(NodeIdentity node) {
            if (_edges.TryGetValue(node, out var found)) {
                return found;
            }
            return new List<DirectedEdge>();
        }

        public int EdgeCount => Edges.Count();

        public IEnumerable<DirectedEdge> Edges => _edges.SelectMany(fromEdge => fromEdge.Value);
        public IEnumerable<NodeIdentity> Nodes => _nodes;

        public static DirectedGraph Of(IEnumerable<DirectedEdge> edges, IEnumerable<NodeIdentity> nodes)
            => new DirectedGraph(edges, nodes);
    }
}
