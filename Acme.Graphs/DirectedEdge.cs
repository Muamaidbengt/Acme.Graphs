namespace Acme.Graphs {
    public class DirectedEdge {
        internal DirectedEdge(NodeIdentity from, NodeIdentity to) {
            ArgumentHelpers.ThrowIfNull(() => from);
            ArgumentHelpers.ThrowIfNull(() => to);
            From = from;
            To = to;
        }

        public NodeIdentity From { get; }
        public NodeIdentity To { get; }

        public override string ToString() => $"{From} -> {To}";

        public static DirectedEdge Between(NodeIdentity from, NodeIdentity to) => new DirectedEdge(from, to);
    }
}
