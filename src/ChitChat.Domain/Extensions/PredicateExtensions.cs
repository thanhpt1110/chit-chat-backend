using System.Linq.Expressions;

namespace ChitChat.Domain.Extensions
{
    /// <summary>
    /// Contains predicate extension methods.
    /// </summary>
    /// <remarks>
    /// Bases on <a href="https://github.com/lotosbin/BinbinPredicateBuilder">Binbin.Linq.PredicateBuilder</a>.
    /// </remarks>
    public static class PredicateExtensions
    {
        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        /// <param name="first">The first expression.</param>
        /// <param name="second">The second expression.</param>
        /// <typeparam name="T">Element type.</typeparam>
        public static Expression<Func<T, bool>>? And<T>(
            this Expression<Func<T, bool>>? first,
            Expression<Func<T, bool>>? second)
        {
            if (first != null && second != null)
                return first.Compose(second, Expression.AndAlso);
            else if (first == null && second == null)
                return null;
            else if (first == null)
                return second;
            else
                return first;
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "or".
        /// </summary>
        /// <param name="first">The first expression.</param>
        /// <param name="second">The second expression.</param>
        /// <typeparam name="T">Element type.</typeparam>
        public static Expression<Func<T, bool>>? Or<T>(
            this Expression<Func<T, bool>>? first,
            Expression<Func<T, bool>>? second)
        {
            if (first != null && second != null)
                return first.Compose(second, Expression.OrElse);
            if (first == null && second == null)
                return null;
            if (first == null)
                return second;
            return first;
        }

        /// <summary>
        /// Negates the predicate.
        /// </summary>
        /// <param name="expression">The predicate expression.</param>
        /// <typeparam name="T">Element type.</typeparam>
        public static Expression<Func<T, bool>>? Not<T>(
            this Expression<Func<T, bool>>? expression)
        {
            if (expression == null)
                return null;
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        /// <summary>
        /// Combines the first expression with the second using the specified merge function.
        /// </summary>
        /// <param name="first">The first expression.</param>
        /// <param name="second">The second expression.</param>
        /// <param name="merge">The merge function.</param>
        /// <typeparam name="T">Element type.</typeparam>
        private static Expression<T> Compose<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => (Expression)p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }

    /// <summary>
    /// Parameter rebinder.
    /// </summary>
    internal sealed class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, Expression> _map;

        private ParameterRebinder(Dictionary<ParameterExpression, Expression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, Expression>();
        }

        /// <summary>
        /// Replaces parameter in the expression.
        /// </summary>
        /// <param name="map">Parameter map.</param>
        /// <param name="exp">An expression.</param>
        public static Expression ReplaceParameters(
            Dictionary<ParameterExpression, Expression> map,
            Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        /// <inheritdoc />
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out var replacement))
            {
                if (replacement is ParameterExpression pe)
                    p = pe;
                else
                    return replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
