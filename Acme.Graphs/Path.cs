using System.Collections;
using System.Collections.Generic;

namespace Acme.Graphs {
    public class Path : IEnumerable<NodeIdentity> {
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

        public static Path Empty { get => new Path(new List<NodeIdentity>()); }

        public IEnumerator<NodeIdentity> GetEnumerator() => _steps.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
