using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Graphs {
    public class Path : IEnumerable<NodeIdentity>, IEquatable<Path> {
        private readonly List<NodeIdentity> _steps;
        private Path(IEnumerable<NodeIdentity> steps) {
            ArgumentHelpers.ThrowIfNull(() => steps);
            _steps = new List<NodeIdentity>(steps);
        }

        public static Path Of(IEnumerable<NodeIdentity> steps) {
            return new Path(steps);
        }

        public static Path Of(params NodeIdentity[] steps) {
            return new Path(steps);
        }

        public static Path Of(IEnumerable<DirectedEdge> steps) {
            var s = steps.Select(step => step.To).ToList();
            s.Insert(0, steps.First().From);
            return Of(s);
        }

        public static Path Of(Path origin, params NodeIdentity[] extension) {
            var steps = new List<NodeIdentity>(origin._steps);
            steps.AddRange(extension);
            return Of(steps);
        }

        public NodeIdentity[] GetRange(int index, int count) => _steps.GetRange(index, count).ToArray();

        public NodeIdentity this[int index] => _steps[index];

        public NodeIdentity Start => _steps[0];
        public NodeIdentity End => _steps[Length - 1];
        public int Length => _steps.Count;

        public static Path Empty { get => new Path(new List<NodeIdentity>()); }

        public IEnumerator<NodeIdentity> GetEnumerator() => _steps.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static bool IsEmpty(Path path) => path._steps.Count == 0;
        public override bool Equals(object obj) => Equals(obj as Path);
        public bool Equals(Path other) => !(other is null) && _steps.SequenceEqual(other._steps);
        public override int GetHashCode() {
            unchecked {
                var res = 199406437;
                foreach (var step in _steps) {
                    res = res * 31 + step.GetHashCode();
                }
                return res;
            }
        }

        public static bool operator ==(Path left, Path right) => !(left is null) && left.Equals(right);
        public static bool operator !=(Path left, Path right) => !(left == right);

        public static Path Join(Path origin, params Path[] additionalPaths) {
            var nodes = origin.ToList();
            foreach (var path in additionalPaths) {
                nodes.AddRange(path);
            }
            return Of(nodes);
        }
    }
}
