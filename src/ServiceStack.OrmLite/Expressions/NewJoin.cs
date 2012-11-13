using System;
using System.Linq.Expressions;

namespace ServiceStack.OrmLite
{
	public class NewJoin<TFrom ,TJoin>
	{
		public object Select1<TKey>(Expression<Func<TFrom,TJoin, TKey>> fields)
		{
			//return  new { fields}; 
			return GrottyHacks.Cast(new {fields}, new {fields});
		}

		public T Select2<T>(Func<T> fields)
		{
			return (T)(object) new{fields}; 
		}

		//public void SelectFields<TKey>(Expression<Func<TFrom,TJoin, TKey>> fields)
		//{
		//	//return (T)(object) new { fields}; 
		//}

		public void SelectFields<TKey>(Expression<Func<TFrom, TKey>> fields)
		{
			//return (T)(object) new { fields}; 
		}



		//var newResult = GetDogsWithBreedNames((Name, BreedName) => new {Name, BreedName });
		//public static TResult GetDogsWithBreedNames<TResult>(Func<object, object, TResult> creator)
		//public object Select1<TKey>(Expression<Func<TFrom,TJoin, TKey>> fields)
		//public static TResult Select3<TResult>(Func<TFrom, TJoin, TResult> creator)
		public  TResult Select3<TResult>(Expression<Func<TFrom, TJoin, TResult>> fields)
		{

			return (TResult) (object) new {Name="Billy", Id=12};
			//return creator("Billy","Golden");
		}

	}

	static class GrottyHacks
	{
	    internal static T Cast<T>(object target, T example)
	    {
	        return (T) target;
	    }

		internal static T Cast<T>(object obj, Func<T> type) { 
			return (T)obj; 
		}
	}
}

