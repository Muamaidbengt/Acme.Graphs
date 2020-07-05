using System;
using System.Collections.Generic;

namespace Acme.Graphs {
    public class NodeIdentity : IEquatable<NodeIdentity> {
        private NodeIdentity(string identity) {
            ArgumentHelpers.ThrowIfNull(() => identity);
            Identity = identity;
        }

        public static NodeIdentity Of(string identity) => new NodeIdentity(identity);

        public string Identity { get; }

        public override string ToString() => Identity;

        public bool Equals(NodeIdentity other) => !(other is null) && Identity.Equals(other.Identity, StringComparison.InvariantCulture);

        public override bool Equals(object obj) => Equals(obj as NodeIdentity);

        public override int GetHashCode() => 91194611 + EqualityComparer<string>.Default.GetHashCode(Identity);

        public static bool operator ==(NodeIdentity a, NodeIdentity b) {
            if (ReferenceEquals(a, b)) {
                return true;
            }

            if (a is null) {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(NodeIdentity a, NodeIdentity b) {
            if (ReferenceEquals(a, b)) {
                return false;
            }

            if (a is null) {
                return true;
            }

            return !a.Equals(b);
        }
    }
}
