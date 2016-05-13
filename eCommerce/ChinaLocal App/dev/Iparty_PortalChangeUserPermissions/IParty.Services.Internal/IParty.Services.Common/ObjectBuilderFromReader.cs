using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace IParty.Services.Common
{
    public class ObjectBuilderFromReader
    {
        public static Func<IDataReader, T> Build<T>()
        {
            string expCacheKey = string.Format("Build {0} Reader", typeof(T).FullName);

            return ExpressionCache.GetExpression(expCacheKey,
                ExpressionBuilder.Build<T>).Compile();

        }

        public static Func<IDataReader, T> Build<T>(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type == null)
                return null;
            string expCacheKey = string.Format("Build {0} Reader", type.FullName);

            return ExpressionCache.GetExpression(expCacheKey,
                ExpressionBuilder.Build<T>(typeName)).Compile();
        }
    }
    public class ExpressionBuilder
    {
        public static Expression<Func<IDataReader, T>> Build<T>()
        {
            ParameterExpression r = Expression.Parameter(typeof(IDataReader), "r");

            List<MemberBinding> bindings = new List<MemberBinding>();

            foreach (PropertyInfo property in GetWritableProperties(typeof(T)))
            {
                MethodCallExpression propertyValue = Expression.Call(
                    typeof(DataReaderExtensions).GetMethod("Field").MakeGenericMethod(property.PropertyType), r,
                    Expression.Constant(property.Name));

                MemberBinding binding = Expression.Bind(property, propertyValue);

                bindings.Add(binding);
            }

            Expression initializer = Expression.MemberInit(Expression.New(typeof(T)), bindings);

            Expression<Func<IDataReader, T>> lambda = Expression.Lambda<Func<IDataReader, T>>(initializer, r);

            return lambda;
        }

        public static Expression<Func<IDataReader, T>> Build<T>(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type == null)
                return null;

            if (type.IsSubclassOf(typeof(T)))
            {
                ParameterExpression r = Expression.Parameter(typeof(IDataReader), "r");

                List<MemberBinding> bindings = new List<MemberBinding>();

                foreach (PropertyInfo property in GetWritableProperties(type))
                {
                    MethodCallExpression propertyValue = Expression.Call(
                        typeof(DataReaderExtensions).GetMethod("Field").MakeGenericMethod(property.PropertyType), r,
                        Expression.Constant(property.Name));

                    MemberBinding binding = Expression.Bind(property, propertyValue);

                    bindings.Add(binding);
                }

                Expression initializer = Expression.MemberInit(Expression.New(type), bindings);

                Expression<Func<IDataReader, T>> lambda = Expression.Lambda<Func<IDataReader, T>>(initializer, r);

                return lambda;
            }
            else
            {
                return null;
            }

        }

        public static IEnumerable<PropertyInfo> GetWritableProperties(Type t)
        {
            foreach (PropertyInfo property in t.GetProperties())
            {
                if (property.CanWrite)
                    yield return property;
            }
        }
    }
    public static class DataReaderExtensions
    {
        public static T Field<T>(this IDataReader reader, string fieldName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Compare(reader.GetName(i), fieldName, true, CultureInfo.InvariantCulture) == 0)
                {
                    int ordinal = reader.GetOrdinal(fieldName);
                    if (reader.IsDBNull(ordinal))
                        return default(T);

                    return (T)reader[ordinal];
                }
            }

            return default(T);

        }
    }
}