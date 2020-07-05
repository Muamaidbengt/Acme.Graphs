using System;

namespace Acme.Graphs {
    public class DuplicateNodeException : ArgumentException {
        public DuplicateNodeException(string paramName)
            : base("Duplicate node found", paramName) {
        }
    }
}
