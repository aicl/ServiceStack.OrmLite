using System;

using System.Linq.Expressions;
using System.Collections.Generic;

namespace ServiceStack.OrmLite
{
	public class MoreJoin<TFrom,TJoin>
	{
		public MoreJoin ()
		{

		}

		/*public void SelectFieds<TKey>(Expression<Func<TFrom, TJoin, TKey>> fields)
		{

		}*/

		public TResult SelectFieds<TResult>(Expression<Func<TFrom, TJoin, TResult>> fields) 
		{
			//return new {fields.Parameters[0].Name};
			//return default(TResult);
			return (TResult) (object) new { Name=""};
			//var t =new { Name=""};
			//return (TResult) (object) new AnonymousType( t) ;

		}

		//public  TResult Select33<TResult>(Expression<Func<TFrom, TJoin, TResult>> fields)
		//{

		//	return (TResult) (object) new {Name="Billy", Id=12};
			//return creator("Billy","Golden");
		//}

	}

	public class MoreTestJoin22<TFrom ,TJoin>
	{

		//var newResult = GetDogsWithBreedNames((Name, BreedName) => new {Name, BreedName });
		//public static TResult GetDogsWithBreedNames<TResult>(Func<object, object, TResult> creator)
		//public object Select1<TKey>(Expression<Func<TFrom,TJoin, TKey>> fields)
		//public static TResult Select3<TResult>(Func<TFrom, TJoin, TResult> creator)
		public  TResult Select4<TResult>(Expression<Func<TFrom, TJoin, TResult>> fields)
		{
			if(fields==null ) return default(TResult);
			//return default(TResult);
			var r = new {Name="Billy", Id=12};
			return (TResult) (object)r ;
		}

	}


}
