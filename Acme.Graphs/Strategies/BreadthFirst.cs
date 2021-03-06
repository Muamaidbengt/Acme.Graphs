﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs {
    public static class BreadthFirst {
        public static IEnumerable<NodeIdentity> VisitAll(DirectedGraph graph, NodeIdentity start) {
            ArgumentHelpers.ThrowIfNull(() => graph);
            ArgumentHelpers.ThrowIfNull(() => start);

            var unvisited = new Queue<NodeIdentity>();
            var spotted = new HashSet<NodeIdentity>();

            void addNeighbors(IEnumerable<DirectedEdge> edges) {
                foreach (var neighbor in edges) {
                    if (spotted.Add(neighbor.To)) {
                        unvisited.Enqueue(neighbor.To);
                    }
                }
            }

            unvisited.Enqueue(start);

            while (unvisited.Any()) {
                var current = unvisited.Dequeue();
                yield return current;

                addNeighbors(graph.EdgesFrom(current));
            }
        }

        public static Path FindPath(DirectedGraph graph, NodeIdentity start, Func<NodeIdentity, bool> predicate) {
            ArgumentHelpers.ThrowIfNull(() => graph);
            ArgumentHelpers.ThrowIfNull(() => start);
            ArgumentHelpers.ThrowIfNull(() => predicate);

            var unvisited = new Queue<Path>();
            var spotted = new HashSet<NodeIdentity>();

            void addNeighbors(Path origin, IEnumerable<DirectedEdge> edges) {
                foreach (var neighbor in edges) {
                    if (spotted.Add(neighbor.To)) {
                        unvisited.Enqueue(Path.Of(origin, neighbor.To));
                    }
                }
            }

            unvisited.Enqueue(Path.Of(start));

            while (unvisited.Any()) {
                var current = unvisited.Dequeue();
                if (predicate(current.End)) {
                    return current;
                }
                addNeighbors(current, graph.EdgesFrom(current.End));
            }
            return Path.Empty;
        }
    }
}
