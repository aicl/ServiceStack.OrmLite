using System;
using System.Collections.Generic;

namespace TestCommon
{
    public static class Factory
    {
        public static List<Country> CountryList{
            get{
                List<Country> l = new List<Country> ();
                Country c  = new Country(){ Id=1,Name="England",Continent="Europe"};
                l.Add(c);
                
                c  = new Country(){ Id=2,Name="Colombia",Continent="America"};
                l.Add(c);
                
                c  = new Country(){ Id=3,Name="Spain",Continent="Europe"};
                l.Add(c);
                
                c  = new Country(){ Id=4,Name="Mexico",Continent="America"};
                l.Add(c);
                
                c  = new Country(){ Id=5,Name="Japan",Continent="Asia"};
                l.Add(c);
                
                return l;
                
            }
        
        }
        
        public static List<City> CityList{
            get{
                List<City> l = new List<City>();
                City c = new City(){Id=1, Name="London", CountryId=1, Population=15};
                l.Add(c);
                
                c = new City(){Id=2, Name="Liverpool", CountryId=1, Population=10};
                l.Add(c);
                
                c = new City(){Id=3, Name="Bogota", CountryId=2, Population=5};
                l.Add(c);
                
                c = new City(){Id=4, Name="Cucuta", CountryId=2, Population=5};
                l.Add(c);
                                
                c = new City(){Id=5, Name="Cartagena", CountryId=2, Population=3};
                l.Add(c);
                
                c = new City(){Id=6, Name="Madrid", CountryId=3, Population=15};
                l.Add(c);
                                
                c = new City(){Id=7, Name="Barcelona", CountryId=3, Population=10};
                l.Add(c);
                                
                c = new City(){Id=8, Name="Mexico DF", CountryId=4, Population=10};
                l.Add(c);
                
                c = new City(){Id=9, Name="Tokio", CountryId=5, Population=10};
                l.Add(c);
                                
                
                return l;
            }
        }
        
        
        public static List<Person> PersonList{
            get{
                List<Person> a = new List<Person>();
                a.Add(new Person(){Name="Demis Bellot",BirthCityId=1, JobCityId=1 });
                a.Add(new Person(){Name="Angel Colmenares",BirthCityId=4, JobCityId=3});
                a.Add(new Person(){Name="Adam Witco",BirthCityId=2});
                a.Add(new Person(){Name="Claudia Espinel", BirthCityId=4, JobCityId=3});
                a.Add(new Person(){Name="Libardo Pajaro", BirthCityId=3});
                a.Add(new Person(){Name="Jorge Garzon",BirthCityId=3, JobCityId=8});
                a.Add(new Person(){Name="Alejandro Isaza",BirthCityId=5, JobCityId=3});
                a.Add(new Person(){Name="Wilmer Agamez",BirthCityId=5, JobCityId=3});
                a.Add(new Person(){Name="Rodger Contreras",BirthCityId=5, JobCityId=3});
                a.Add(new Person(){Name="Chuck Benedict", BirthCityId=9});
                a.Add(new Person(){Name="James Benedict II", BirthCityId=9});
                a.Add(new Person(){Name="Ethan Brown",BirthCityId=1});
                a.Add(new Person(){Name="Xavi Garzon",BirthCityId=7, JobCityId=7});
                a.Add(new Person(){Name="Luis garzon",BirthCityId=8, JobCityId=8});
                return a;   
            }
        }
        
        /*
        public static List<Author> AuthorList{
            get{
                List<Author> a = new List<Author>();
                a.Add(new Author(){Name="Demis Bellot",Birthday= DateTime.Today.AddYears(-20),Active=true,Earnings= 99.9m,Comments="CSharp books", Rate=10, City="London"});
                a.Add(new Author(){Name="Angel Colmenares",Birthday= DateTime.Today.AddYears(-25),Active=true,Earnings= 50.0m,Comments="CSharp books", Rate=5, City="Bogota"});
                a.Add(new Author(){Name="Adam Witco",Birthday= DateTime.Today.AddYears(-20),Active=true,Earnings= 80.0m,Comments="Math Books", Rate=9, City="London"});
                a.Add(new Author(){Name="Claudia Espinel",Birthday= DateTime.Today.AddYears(-23),Active=true,Earnings= 60.0m,Comments="Cooking books", Rate=10, City="Bogota"});
                a.Add(new Author(){Name="Libardo Pajaro",Birthday= DateTime.Today.AddYears(-25),Active=true,Earnings= 80.0m,Comments="CSharp books", Rate=9, City="Bogota"});
                a.Add(new Author(){Name="Jorge Garzon",Birthday= DateTime.Today.AddYears(-28),Active=true,Earnings= 70.0m,Comments="CSharp books", Rate=9, City="Bogota"});
                a.Add(new Author(){Name="Alejandro Isaza",Birthday= DateTime.Today.AddYears(-20),Active=true,Earnings= 70.0m,Comments="Java books", Rate=0, City="Bogota"});
                a.Add(new Author(){Name="Wilmer Agamez",Birthday= DateTime.Today.AddYears(-20),Active=true,Earnings= 30.0m,Comments="Java books", Rate=0, City="Cartagena"});
                a.Add(new Author(){Name="Rodger Contreras",Birthday= DateTime.Today.AddYears(-25),Active=true,Earnings= 90.0m,Comments="CSharp books", Rate=8, City="Cartagena"});
                a.Add(new Author(){Name="Chuck Benedict",Birthday= DateTime.Today.AddYears(-22),Active=true,Earnings= 85.5m,Comments="CSharp books", Rate=8, City="London"});
                a.Add(new Author(){Name="James Benedict II",Birthday= DateTime.Today.AddYears(-22),Active=true,Earnings= 85.5m,Comments="Java books", Rate=5, City="Berlin"});
                a.Add(new Author(){Name="Ethan Brown",Birthday= DateTime.Today.AddYears(-20),Active=true,Earnings= 45.0m,Comments="CSharp books", Rate=5, City="Madrid"});
                a.Add(new Author(){Name="Xavi Garzon",Birthday= DateTime.Today.AddYears(-22),Active=true,Earnings= 75.0m,Comments="CSharp books", Rate=9, City="Madrid"});
                a.Add(new Author(){Name="Luis garzon",Birthday= DateTime.Today.AddYears(-22),Active=true,Earnings= 85.0m,Comments="CSharp books", Rate=10, City="Mexico"});
                return a;   
            }
        }
        */
        
    }
}
