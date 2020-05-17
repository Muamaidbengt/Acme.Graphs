namespace Acme.Graphs
{
    public class Node
    {
        internal Node(NodeIdentity identity)
        {
            Identity = identity;
        }
        public NodeIdentity Identity { get; }
    }
}
