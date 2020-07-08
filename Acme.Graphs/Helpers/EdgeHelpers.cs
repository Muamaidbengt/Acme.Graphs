using System.Linq;

namespace Acme.Graphs.Helpers {
    public static class EdgeHelpers {
        public static bool LeadsInto(this DirectedEdge edge, Path path) {
            return path.Any(n => edge.To == n) && path.All(n => edge.From != n);
        }

        public static bool LeadsOutOf(this DirectedEdge edge, Path path) {
            return path.All(n => edge.To != n) && path.Any(n => edge.From == n);
        }
    }
}
