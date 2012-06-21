/* 
 * inspired by http://www.albahari.com/nutshell/predicatebuilder.aspx
 * Licensing
 * LINQKit is free. 
 * The source code is issued under a permissive free license, 
 * which means you can modify it as you please, 
 * and incorporate it into your own commercial or non-commercial software.
 * 
*/ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace ServiceStack.OrmLite
{

    public static class PredicateBuilder
    {
    
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>(){ return f => false; }
        public static Expression<Func<T, bool>> Null<T>(){ return null; }
        
        public static Expression<Func<T, bool>> Start<T>(Expression<Func<T, bool>> self) 
        { 
            return self; 
        }
        
        public static bool IsNull<T>(this Expression<Func<T, bool>> self) 
        { 
            return self==null; 
        }
        
     
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> self,
                                                          Expression<Func<T, bool>> expression)
        {
            var invokedExpr = Expression.Invoke(self, self.Parameters.Cast<Expression> ());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse (self.Body, invokedExpr), self.Parameters);
        }
        
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> self,
                                                           Expression<Func<T, bool>> expression)
        {
            var invokedExpr = Expression.Invoke (expression, self.Parameters.Cast<Expression> ());

            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso (self.Body, invokedExpr), self.Parameters);
        }
    }

    /*
    public static class PredicateBuilder
    {

        public static Expression<Func<T, bool>> True<T> ()
        {
            return Expression.Lambda<Func<T, bool>> (Expression.Constant (true), Expression.Parameter (typeof (T)));
        }

        public static Expression<Func<T, bool>> False<T> ()
        {
            return Expression.Lambda<Func<T, bool>> (Expression.Constant (false), Expression.Parameter (typeof (T)));
        }

        public static Expression<Func<T, bool>> OrElse<T> (this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
        {
            return self.Combine (expression, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> AndAlso<T> (this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
        {
            return self.Combine (expression, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Not<T> (this Expression<Func<T, bool>> self)
        {
            return self.Combine (Expression.Not);
        }
    }
    */


}

