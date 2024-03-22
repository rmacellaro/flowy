using System.Linq.Expressions;
using System.Reflection;
using It.Flowy.Camunda.Models.Core.Common;

namespace Flowy.Core.Contexts;

public static class QueryableExtensions {

  public static IQueryable<T> OrderBySort<T>(this IQueryable<T> source, Sort? sort) {
    if (sort == null) { return source; }
    return source.OrderBy(sort.Column, sort.Method);
  }

  public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string? columnPath, string? method) {
    if (columnPath == null || method == null) { throw new Exception("column or method is null"); }
    if (method == "ASC") {
      return source.OrderByColumnUsing(columnPath, "OrderBy");
    } else if (method == "DESC") {
      return source.OrderByColumnUsing(columnPath, "OrderByDescending");
    } else {
      throw new Exception("Method is not ASC or DESC");
    }
  }

  public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath) 
  => source.OrderByColumnUsing(columnPath, "OrderBy");
  
  public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath) 
  => source.OrderByColumnUsing(columnPath, "OrderByDescending");

  public static IOrderedQueryable<T> ThenByColumn<T>(this IOrderedQueryable<T> source, string columnPath) 
  => source.OrderByColumnUsing(columnPath, "ThenBy");

  public static IOrderedQueryable<T> ThenByColumnDescending<T>(this IOrderedQueryable<T> source, string columnPath) 
  => source.OrderByColumnUsing(columnPath, "ThenByDescending");

  private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method) {
    ParameterExpression parameter = Expression.Parameter(typeof(T), "item");
    Expression member = columnPath.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
    Expression keySelector = Expression.Lambda(member, parameter);
    Expression methodCall = Expression.Call(typeof(Queryable), method, new[] { 
      parameter.Type, member.Type 
    }, source.Expression, Expression.Quote(keySelector));
    return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
  }

  public static ICollection<string> SupportedFilterMethod = new List<string>(){ "Contains", "Equals", "StartsWith", "EndsWith" };
  public static IQueryable<T> FilterBy<T>(this IQueryable<T> source, string? columnPath, string? method, object? value){
    if (string.IsNullOrEmpty(columnPath) || string.IsNullOrEmpty(method) || value == null){
      throw new Exception("columnPath or Method or Value is null or empty");
    }
    if (!SupportedFilterMethod.Contains(method)){ throw new Exception("Method " + method + ", to Filter not supported!"); }

    ParameterExpression parameter = Expression.Parameter(typeof(T), "item");
    Expression property = Expression.Property(parameter, columnPath);
    Expression target = Expression.Constant(value);
    //Expression containsMethod = Expression.Call(property, "Contains", null, target);

    Expression callMethod;
    if (method.Equals("Equals"))  {
      MethodInfo? equalMethod = property.Type.GetMethod("Equals", new[] { target.Type });
      if (equalMethod == null) { return source; }
      callMethod = Expression.Call(property, equalMethod, target);
    } else {
      callMethod = Expression.Call(property, method, null, target);
    }

    Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(callMethod, parameter);
    return source.AsQueryable().Where(lambda);
  }

  public static IQueryable<T> FiltersBy<T>(this IQueryable<T> source, ICollection<Query>? queries){
    if (queries != null) {
      foreach(Query q in queries){
        source = source.FilterBy(q.Column, q.Method, q.Value);
      }
    }
    return source;
  }
}