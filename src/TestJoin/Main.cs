using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using ServiceStack.Common.Utils;
using ServiceStack.DataAnnotations;
using ServiceStack.Common.Extensions;
using ServiceStack.OrmLite;
using TestCommon;

namespace TestJoin
{
    class MainClass
    {

        private static List<Dialect> dialects;

        public static void Main (string[] args)
        {

            bool exit=false;

            dialects= BuildDialectList();

            PaintMenu();

            while(!exit){

                Console.WriteLine("Select your option [{0}-{1}] or q to quit  and press ENTER", 1, dialects.Count);
                string option = Console.ReadLine();
                if(string.IsNullOrEmpty( option))
                    Console.WriteLine("NO VALID OPTION");
                else if( option.ToUpper()=="Q")
                    exit=true;
                else {
                    int opt;
                    if (int.TryParse(option, out opt)){
                        if(opt>=1 && opt<=dialects.Count)
                            TestDialect(dialects[opt-1]);
                        else{
                            Console.WriteLine("NO VALID OPTION");
                        }

                    }
                    else
                        Console.WriteLine("NO VALID OPTION");
                }   
            }


        }

        private static void PaintMenu(){
            Console.Clear();
            int i=0;
            foreach(Dialect d in dialects){
                Console.WriteLine("{0} {1}", ++i, d.Name); 
            }
            Console.WriteLine("q quit"); 
        }

        private static List<Dialect> BuildDialectList(){
            List<Dialect> l = new List<Dialect>();
            Dialect d = new Dialect(){
                Name="Sqlite", 
                PathToAssembly="../../../ServiceStack.OrmLite.Sqlite/bin/Debug",
                AssemblyName="ServiceStack.OrmLite.Sqlite.dll",
                ClassName="ServiceStack.OrmLite.Sqlite.SqliteOrmLiteDialectProvider",
                InstanceFieldName="Instance",
                ConnectionString= "~/db.sqlite".MapAbsolutePath()
            };
            l.Add(d);

            d = new Dialect(){
                Name="SqlServer", 
                PathToAssembly="../../../ServiceStack.OrmLite.SqlServer/bin/Debug",
                AssemblyName="ServiceStack.OrmLite.SqlServer.dll",
                ClassName="ServiceStack.OrmLite.SqlServer.SqlServerOrmLiteDialectProvider",
                InstanceFieldName="Instance",
                ConnectionString= "~/test.mdf".MapAbsolutePath()
            };
            l.Add(d);

            d = new Dialect()
                {Name="MySql",
                PathToAssembly="../../../ServiceStack.OrmLite.MySql/bin/Debug",
                AssemblyName="ServiceStack.OrmLite.MySql.dll",
                ClassName="ServiceStack.OrmLite.MySql.MySqlDialectProvider",
                InstanceFieldName="Instance",
                ConnectionString= "Server = 127.0.0.1; Database = ormlite; Uid = root; Pwd = password"
            };
            l.Add(d);

            d = new Dialect(){
                Name="PostgreSQL", 
                PathToAssembly="../../../ServiceStack.OrmLite.PostgreSQL/bin/Debug", 
                AssemblyName="ServiceStack.OrmLite.PostgreSQL.dll", 
                ClassName="ServiceStack.OrmLite.PostgreSQL.PostgreSQLDialectProvider", 
                InstanceFieldName="Instance",
                ConnectionString="Server=localhost;Port=5432;User Id=postgres; Password=postgres; Database=ormlite"
            };
            l.Add(d);

            d = new Dialect()
                {Name="FirebirdSql",
                PathToAssembly="../../../ServiceStack.OrmLite.Firebird/bin/Debug", 
                AssemblyName="ServiceStack.OrmLite.Firebird.dll", 
                ClassName="ServiceStack.OrmLite.Firebird.FirebirdOrmLiteDialectProvider", 
                InstanceFieldName="Instance",
                ConnectionString="User=SYSDBA;Password=masterkey;Database=employee.fdb;DataSource=localhost;Dialect=3;charset=ISO8859_1;"
            };
            l.Add(d);

            return l;

        }



        private static void TestDialect(Dialect dialect){
            Console.Clear();
            Console.WriteLine("Testing expressions for Dialect {0}", dialect.Name);

            OrmLiteConfig.ClearCache();
            OrmLiteConfig.DialectProvider=dialect.DialectProvider;


            using (IDbConnection db =
                   dialect.ConnectionString.OpenDbConnection())
            using ( IDbCommand dbCmd = db.CreateCommand())
            {

                dbCmd.DropTable<Person>();
                dbCmd.DropTable<City>();
                dbCmd.DropTable<Country>();
                dbCmd.CreateTable<Country>();
                dbCmd.CreateTable<City>();
                dbCmd.CreateTable<Person>();

                dbCmd.InsertAll<Country>(Factory.CountryList);
                dbCmd.InsertAll<City>(Factory.CityList);
                dbCmd.InsertAll<Person>(Factory.PersonList);

                try{


                    var vis = ReadExtensions.CreateExpression<TestPerson>();
                    vis.Where(r=>r.Continent=="Europe");
                    Console.WriteLine(vis.ToSelectStatement());
                    Console.WriteLine("-----------------------------------------");

                    vis.ExcludeJoin=true;
                    vis.Where();
                    Console.WriteLine(vis.ToSelectStatement());
                    Console.WriteLine("-----------------------------------------");


                    var r0 = dbCmd.Select<TestPerson>();
                    Console.WriteLine("Records en person: '{0}'", r0.Count);

                    vis.ExcludeJoin=false;
                    vis.Select(r=> new {r.Continent, r.Name}).OrderBy(r=>r.BirthCountry);
                    Console.WriteLine(vis.ToSelectStatement());
                    Console.WriteLine("-----------------------------------------");

                    vis.SelectDistinct(r=>r.Name);
                    Console.WriteLine(vis.ToSelectStatement());
                    Console.WriteLine("-----------------------------------------");

                    vis.Select();
                    vis.Where(r=>r.Continent=="Europe").OrderBy(r=>r.BirthCountry);
                    r0= dbCmd.Select(vis);
                    Console.WriteLine("Records en person r.Continent=='Europe': '{0}'", r0.Count);

                    r0= dbCmd.Select<TestPerson>(r=> r.BirthCity=="London");
                    Console.WriteLine("Records en person r.BirthCity=='London': '{0}'", r0.Count);

                    TestPerson tp = r0[0];
                    tp.Id=0;
                    dbCmd.Insert(tp);
                    tp.Id= (int) dbCmd.GetLastInsertId();

                    Console.WriteLine("El id es :'{0}'", tp.Id);

                    dbCmd.Update(tp);
                    Console.WriteLine("Actualizados : '{0}'",dbCmd.Update(tp, r=> r.Name, r=>r.Id==0));

                    Console.WriteLine("Borrados : '{0}'",dbCmd.Delete<TestPerson>( r=>r.Id==0));


                    int expected=6;
                    var r1= dbCmd.Select<City>(qr=> qr.Population>=10);
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r1.Count, expected==r1.Count?"OK":"********* FAILED *********");


                    expected=7;
                    var r2= dbCmd.Select<Join1>(qr=>qr.Population<=5);
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r2.Count, expected==r2.Count?"OK":"********* FAILED *********");


                    expected=3;
                    var r3= dbCmd.Select<Join2>(qr=>qr.BirthCity=="London");
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r3.Count, expected==r3.Count?"OK":"********* FAILED *********");


                    expected=5;
                    var r4 =dbCmd.Select<Join3>(qr=>qr.Continent=="Europe");
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r4.Count, expected==r4.Count?"OK":"********* FAILED *********");


                    expected=5;
                    var city="Bogota";              
                    var r5 = dbCmd.Select<PersonCity>(qr=> qr.JobCity==city);
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r5.Count, expected==r5.Count?"OK":"********* FAILED *********");

                    expected=6;
                    var r6 = dbCmd.Select<DerivatedFromPerson>(qr=>qr.BirthCityId!=qr.JobCityId);
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r6.Count, expected==r6.Count?"OK":"********* FAILED *********");

                    expected=2;
                    var r7= dbCmd.Select<DerivatedFromDerivatedFromPerson>(qr=>qr.Continent=="Asia");
                    Console.WriteLine("Expected:{0}  Selected:{1}  {2}", expected, r7.Count, expected==r7.Count?"OK":"********* FAILED *********");


                    var r8= dbCmd.Select<DerivatedFromDerivatedFromPerson>(
                        exp=>exp.
                            Where(qr=>qr.BirthCityId!=qr.JobCityId).
                            OrderBy(qr=>qr.Continent));

                    Console.WriteLine("Expected:{0} Selected:{1}  {2}", "America", r8.FirstOrDefault().Continent, "America"==r8.FirstOrDefault().Continent?"OK":"********* FAILED *********");


                }
                catch(Exception e){
                    Console.WriteLine(e);
                    Console.WriteLine(e.Message);   
                }

            }




            Console.WriteLine("Press enter to return to main menu");
            Console.ReadLine();
            PaintMenu();
        }



    }
}