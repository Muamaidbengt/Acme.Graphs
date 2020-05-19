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

        public NodeIdentity this[int index] => _steps[index];

        public static Path Empty { get => new Path(new List<NodeIdentity>()); }

        public IEnumerator<NodeIdentity> GetEnumerator() => _steps.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public override bool Equals(object obj) => Equals(obj as Path);
        public bool Equals(Path other) => !(other is null) && _steps.SequenceEqual(other._steps);
        public override int GetHashCode() {
            unchecked { 
                var res = 199406437;
                foreach(var step in _steps) {
                    res = res * 31 + step.GetHashCode();
                }
                return res;
            }
        }

        public static bool operator ==(Path left, Path right) => !(left is null) && left.Equals(right);
        public static bool operator !=(Path left, Path right) => !(left == right);
    }
}
