using System;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using TestCommon;
using Test;

namespace TestNewJoin
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			//CheesecakeFactory.DoMain();

			var tj =new TestJoin<Person,City>();
			var tjsm = tj.Select3((person, city) => new { Name="Ciudad", Id=1 });
			Console.WriteLine(tjsm);

			var mj = new MoreTestJoin22<Person,City>();
			var tmj = mj.Select4((person, city) => new { Name="Ciudad", Id=1 });
			Console.WriteLine(tmj);


			//var nj = new NewJoin<Person,City>();
			//var tnj = nj.Select3((person, city) => new { Name="Ciudad", Id=1 });
			//Console.WriteLine(tnj);

			//var tj2 =new TestJoin22<Person,City>();
			//var tjsm2 = tj2.Select4((person, city) => new { Name="Ciudad", Id=1 });
			//Console.WriteLine(tjsm2);


			OrmLiteConfig.DialectProvider= MySqlDialectProvider.Instance;

			//var connectionString= "Server = 127.0.0.1; Database = ormlite; Uid = root; Pwd = password";

			//var personVisitor = new MySqlExpressionVisitor<Person>();
			//var cityVisitor = new MySqlExpressionVisitor<City>();

			//var join = new Join<Person,City>(personVisitor,
			//                                 cityVisitor,
			//                                 (f,j)=>f.JobCityId==j.Id );

			var join =new Join<Person,City>(new Person(), new City(),
			                                (f,j)=>f.JobCityId==j.Id );

			Console.WriteLine(join);

			//join.SelectFieds((f,j)=>f.Name);
			//var s = join.SelectFieds((f,j)=>new {f.Name, f.BirthCityId});
			//Console.WriteLine(s);
			//var sm = join.Select3((person, city) => new { Name="Ciudad", Id=1 });
			//Console.WriteLine(sm);


			//using (IDbConnection db =  connectionString.OpenDbConnection())
            //using ( IDbCommand dbCmd = db.CreateCommand())
            //{
			//}
		}
	}
}
