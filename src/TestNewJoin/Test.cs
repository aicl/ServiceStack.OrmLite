using System;
using System.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
	public class TestCity{
		public TestCity(){}
		public int Id {get;set;}
		public string Name { get; set; }
	}

	public class TestPerson {
		public TestPerson (){}
		public int Id {get;set;}
		public string Name {get;set;}
		public int JobTestCityId {get;set;}
	}


	public class TestJoin<TFrom ,TJoin>
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

	public class CheesecakeFactory
	{
	    static object CreateCheesecake()
	    {
	        return new { Fruit="Strawberry", Topping="Chocolate" };
	    }

	    public static void DoMain()
	    {
	        object weaklyTyped = CreateCheesecake();
	        var stronglyTyped = GrottyHacks.Cast(weaklyTyped,
	            new { Fruit="", Topping="" });

	        Console.WriteLine("Cheesecake: {0} ({1})",
	            stronglyTyped.Fruit, stronglyTyped.Topping);            

			GrottyHacks.Cast(weaklyTyped, () => new { Fruit="", Topping="" });

			Console.WriteLine("Cheesecake: {0} ({1})",
	            stronglyTyped.Fruit, stronglyTyped.Topping);            

			TestJoin<TestPerson,TestCity> join = new TestJoin<TestPerson, TestCity>();
			join.SelectFields(f=> new {f.Id, f.Name});

			//var res= join.Select2( ()=> new {Id=1, Name="hello"});
			//Console.WriteLine(res.Name);

			//var res1= GrottyHacks.Cast(join.Select1((f,j)=> new { Id= f.Id, Name=f.Name}),
			//                           new{Id=1,Name="nomre"} );
			var res1= join.Select1((f,j)=> new { Id= f.Id, Name=f.Name});
			Console.WriteLine(res1.GetType());
			//join.SelectFields((f,j)=> new {f.Name, j.Name});


			var result = Repeat(new { Name = "Foo Bar", Age = 100 }, 10);

			Console.WriteLine(result);

			var newResult = GetDogsWithBreedNames((Name, BreedName) => new {Name, BreedName });

			Console.WriteLine(newResult);
			Console.WriteLine(newResult.Name);


			var sm = join.Select3((person, city) => new { Name="Ciudad", Id=1 });
			Console.WriteLine(sm);
	    }


		public static TResult GetDogsWithBreedNames<TResult>(Func<object, object, TResult> creator)
		{
		    //var db = new DogDataContext(ConnectString);
		    //var result = from d in db.Dogs
		    //                join b in db.Breeds on d.BreedId equals b.BreedId
		    //                select creator(d.Name, b.BreedName);
		    //return result;
			return creator("Billy","Golden");
		}



		private static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
    		for(int i=0; i<count; i++)
    		{
        		yield return element;
    	}
}


	}

}