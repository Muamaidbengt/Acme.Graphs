using System;
using System.Linq.Expressions;

namespace Acme.Graphs {
    internal static class ArgumentHelpers {
        public static void ThrowIfNull<T>(Expression<Func<T>> expr) {
            if (expr.Compile().Invoke() == null) {
                throw new ArgumentNullException(GetDescriptor(expr));
            }
        }

        private static string GetDescriptor<T>(Expression<Func<T>> expr) {
            if (expr.Body is MemberExpression memberExpr) {
                return memberExpr.Member.Name;
            }
            return expr.ToString();
        }
    }
}
