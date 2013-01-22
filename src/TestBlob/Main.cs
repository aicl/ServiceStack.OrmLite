using System;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Firebird;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;

namespace TestBlob
{
	public class BlobTable{
		public BlobTable(){}
		public long Id { get; set; }
        public byte[] SomeBytes { get; set; }
	}

        
        


	class MainClass
	{
		static ILog log;

		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			LogManager.LogFactory = new ConsoleLogFactory();
			log = LogManager.GetLogger(typeof (MainClass));
			log.Info("Configurado log");

			OrmLiteConfig.DialectProvider = new FirebirdOrmLiteDialectProvider();
									
			using (IDbConnection db =
			       "User=SYSDBA;Password=masterkey;Database=employee.fdb;DataSource=localhost;Dialect=3;charset=ISO8859_1;".OpenDbConnection())
			using ( IDbCommand cmd = db.CreateCommand())
			{
				cmd.CreateTable<BlobTable>();


				cmd.CommandText = "INSERT INTO BLOBTABLE (Id, SomeBytes) VALUES (8, @bytes)";
				var parameter = cmd.CreateParameter();
				parameter.ParameterName="bytes";
				parameter.Value=new byte[] { 0, 1, 2, 3 };

				cmd.Parameters.Add(parameter);
				Console.WriteLine(cmd.CommandText);
				cmd.ExecuteNonQuery();

				//----

				cmd.Insert(new BlobTable{Id=9,SomeBytes= new byte[] { 0, 1, 2, 3 } });
				List<BlobTable> bt = cmd.Select<BlobTable>("SELECT * FROM BLOBTABLE");
				foreach( var r in  bt ){
					Console.WriteLine(r);
				}


			}

			Console.WriteLine ("This is The End my friend!");

		}

	}
}
