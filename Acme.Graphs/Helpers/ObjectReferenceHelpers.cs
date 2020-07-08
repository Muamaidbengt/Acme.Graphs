using System;
using System.Runtime.CompilerServices;

namespace Acme.Graphs.Helpers {
    public static class ObjectReferenceHelpers {
        private static readonly ConditionalWeakTable<object, RefId> _ids = new ConditionalWeakTable<object, RefId>();

        public static Guid GetRefId<T>(this T obj) where T : class {
            if (ReferenceEquals(obj, null)) {
                return default;
            }

            return _ids.GetOrCreateValue(obj).Id;
        }

        private class RefId {
            public Guid Id { get; } = Guid.NewGuid();
        }
    }
}
