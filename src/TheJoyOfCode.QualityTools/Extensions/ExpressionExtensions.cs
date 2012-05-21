using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TheJoyOfCode.QualityTools.Extensions
{
    public static class ExpressionExtensions
    {

        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static MethodInfo GetMethodInfo(this Expression<Func<object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            return ((MethodCallExpression)(expression.Body)).Method;
        }
        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static MethodInfo GetMethodInfo(this Expression<Action> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            return ((MethodCallExpression)(expression.Body)).Method;
        }

        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static MethodInfo GetMethodInfo<TTarget>(this Expression<Func<TTarget, object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            return ((MethodCallExpression)(expression.Body)).Method;
        }
        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static MethodInfo GetMethodInfo<TTarget>(this Expression<Action<TTarget>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            return ((MethodCallExpression)(expression.Body)).Method;
        }

        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static MethodInfo GetMethodInfo<TTarget, TResult>(this Expression<Func<TTarget, TResult>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            return ((MethodCallExpression)(expression.Body)).Method;
        }
        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static PropertyInfo GetPropertyInfo(this Expression<Func<object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            return ((PropertyInfo)((MemberExpression)expression.Body).Member);
        }

        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static PropertyInfo GetPropertyInfo<TTarget>(this Expression<Func<TTarget, object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            var memberExpression = GetMemberExpression(expression);
            return ((PropertyInfo)(memberExpression.Member));
        }
        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static string GetPropertyName(this Expression<Func<object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            return (((MemberExpression)expression.Body).Member).Name;
        }

        /// <summary>
        /// Get the name of a member from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The name of the member.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static string GetPropertyName<TTarget>(this Expression<Func<TTarget, object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");
            var memberExpression = GetMemberExpression(expression);
            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Get the parent <see cref="Type"/> from an <see cref="Expression{TDelegate}"/>.
        /// </summary>
        /// <param name="expression">The lambda <see cref="Expression{TDelegate}"/> representing the member.</param>
        /// <returns>The <see cref="Type"/> of the parent.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is a null reference.</exception>
        public static Type GetMemberParentType<TTarget>(this Expression<Func<TTarget, object>> expression)
        {
            Guard.ArgumentNotNull(expression, "expression");

            var memberExpression = GetMemberExpression(expression);
            return memberExpression.Member.DeclaringType;
        }


        private static MemberExpression GetMemberExpression<TTarget>(Expression<Func<TTarget, object>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                return body.Operand as MemberExpression;
            }
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                return expression.Body as MemberExpression;
            }
            throw new ArgumentException("Expression is not a MemberExpression", "expression");
        }


      
    }
}