using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    using System.Collections;
    using System.Linq.Expressions;

    internal class Program
    {
        private static void Main(string[] args)
        {

            var arr = new[]
                          {
                              new MyClass("dima"),
                              new MyClass("dima"),
                              new MyClass("value"),
                              new MyClass("test"),    
                          };

            var t = FilterByPropertyValue(arr.AsQueryable(), "naMe", "dima");

        }


        public static IQueryable<TSource> FilterByPropertyValue<TSource>(
            IQueryable<TSource> query,
            string propertyName,
            object value)
        {
            var param = Expression.Parameter(typeof(TSource));
            var body = Expression.Equal(Expression.Property(param, propertyName), Expression.Constant(value));
            var expr = Expression.Call(
                typeof(Queryable),
                "Where",
                new[] { typeof(TSource) },
                query.Expression,
                Expression.Lambda<Func<TSource, bool>>(body, param));
            return query.Provider.CreateQuery<TSource>(expr);

        }
    }

    class MyClass
    {
        public MyClass(string name)
        {
            this.Name = name;
        }

        private string Name { get; set; }
    }
}
