﻿using System.Linq;
using System.Collections.Generic;

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

        public int EdgeCount => _edges
            .SelectMany(fromEdge => fromEdge.Value)
            .Count();

        public static DirectedGraph Of(IEnumerable<DirectedEdge> edges, IEnumerable<NodeIdentity> nodes) 
            => new DirectedGraph(edges, nodes);
    }
}
