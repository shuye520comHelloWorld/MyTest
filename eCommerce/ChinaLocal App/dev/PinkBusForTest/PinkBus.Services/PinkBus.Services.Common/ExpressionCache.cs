using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PinkBus.Services.Common
{
    class ExpressionCache
    {
        public static object Locker = new object();

        public static Expression<Func<TSource, TResult>> GetExpression<TSource, TResult>
            (string expressionKey, Func<Expression<Func<TSource, TResult>>> expressionBuilder)
        {
            if (HttpRuntime.Cache.Get(expressionKey) == null)
            {
                lock (Locker)
                {
                    if (HttpRuntime.Cache.Get(expressionKey) == null)
                        HttpRuntime.Cache.Insert(expressionKey, expressionBuilder.Invoke());
                }
            }

            return (Expression<Func<TSource, TResult>>)HttpRuntime.Cache.Get(expressionKey);
        }

        public static Expression<Func<TSource, TResult>> GetExpression<TSource, TResult>
            (string expressionKey, Expression<Func<TSource, TResult>> expressionBuilder)
        {
            if (HttpRuntime.Cache.Get(expressionKey) == null)
            {
                lock (Locker)
                {
                    if (HttpRuntime.Cache.Get(expressionKey) == null)
                        HttpRuntime.Cache.Insert(expressionKey, expressionBuilder);
                }
            }

            return (Expression<Func<TSource, TResult>>)HttpRuntime.Cache.Get(expressionKey);
        }
    }
}