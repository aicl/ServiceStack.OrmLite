using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.Common.Utils;
using ServiceStack.Common.Extensions;
using ServiceStack.OrmLite;

namespace TestCommon
{
    // From "Person" "Person" 
    [SelectFrom(typeof(Person))]
    // join "City" "City" on "Person"."BirthCityId"= "City"."Id"
    [JoinTo(typeof(City),"BirthCityId","Id")]  
    public class Join1{

        public Join1(){}

        //SELECT "Person"."PersonName" as "Name"  //all info is taken from property Name of SelectFrom Person
        public  string Name {get; set;}

        //"City.People" as Population
        [BelongsTo(typeof(City))]       //all info is taken from property Population of City 
        public int Population { get; set;}

    }
    
    [SelectFrom(typeof(Person))]
    [JoinTo(typeof(City),"BirthCityId","Id")]  
    public class Join2{

        public Join2(){}

        public  string Name {get; set;}

        // "City"."Name" as "BirthCity"  //all info is taken from property Name of City 
        [BelongsTo(typeof(City), "Name")]
        public  string BirthCity {get; set;}

    }
    
    [SelectFrom(typeof(Person))]
    [JoinTo(typeof(City),"BirthCityId","Id", Order=0)]
    // Join "Country" "Country" on "City"."CountryId"= "Country"."Id"
    [JoinTo(typeof(City), typeof(Country),"CountryId","Id", Order=1)]
    public class Join3{

        public Join3(){}

        public  string Name {get; set;}
                
        [BelongsTo(typeof(City), "Name")]
        public  string BirthCity {get; set;}

        //"Countries"."Name" as "BirthCountry"
        [BelongsTo(typeof(Country),"Name")]
        public  string BirthCountry {get; set;}

        //"Countries.Continent" as Continent
        [BelongsTo(typeof(Country))]
        public string Continent { get; set;}

    }
        
    
    
    [SelectFrom(typeof(Person))]
    [JoinTo(typeof(City),"BirthCityId","Id", Order=0 )] 
    [JoinTo(typeof(City), typeof(Country),"CountryId","Id", Order=1)]
    // Left Join "City" "C2" on "Person"."JobCityId"= "C2"."Id"
    [JoinTo(typeof(City),"JobCityId","Id", ChildAlias="C2", JoinType=JoinType.Left, Order=2)]
    // Left Join "Country" "C3" on "C2"."CountryId" = "C3"."Id"
    [JoinTo(typeof(City), typeof(Country),"CountryId","Id",ParentAlias="C2", ChildAlias="C3",JoinType=JoinType.Left, Order=3)]

    public class PersonCity
    {
        public PersonCity ()
        {
        }

        public  string Name {get; set;}
                
        [BelongsTo(typeof(City), "Name")]
        public  string BirthCity {get; set;}
        
        [BelongsTo(typeof(Country),"Name")]
        public  string BirthCountry {get; set;}

        //"C2"."Name" as "JobCity"
        [BelongsTo(typeof(City),PropertyName="Name",ParentAlias="C2")]
        public  string JobCity {get; set;}

        //"C3"."Name" as "JobCountry"
        [BelongsTo(typeof(Country),"Name", ParentAlias="C3")]
        public  string JobCountry {get; set;}
                
        [BelongsTo(typeof(Country))]
        public string   Continent { get; set;}
        
        [BelongsTo(typeof(City))]
        public int Population { get; set;}

    }
    
    
    [JoinTo(typeof(City),"BirthCityId","Id")]  
    public class DerivatedFromPerson:Person{
        
        [BelongsTo(typeof(City), "Name")]
        public  string BirthCity {get; set;}

    }
    
    [JoinTo(typeof(City), typeof(Country),"CountryId","Id", Order=1)]    
    public class DerivatedFromDerivatedFromPerson:DerivatedFromPerson{
        
        [BelongsTo(typeof(Country))]
        public string   Continent { get; set;}

    }

    [Alias("Person")]
    [JoinTo(typeof(City),"BirthCityId","Id")]
    [JoinTo(typeof(City), typeof(Country),"CountryId","Id", Order=1)]
    [JoinTo(typeof(City),"JobCityId","Id", ChildAlias="C2", JoinType=JoinType.Left, Order=2)]
    [JoinTo(typeof(City), typeof(Country),"CountryId","Id",ParentAlias="C2", ChildAlias="C3",JoinType=JoinType.Left, Order=3)]
    public class TestPerson
    {
       public TestPerson ()
       {
       }
       
       //[Alias("PersonId")]
       [Alias("Id")]
       [AutoIncrement]
       public int Id { get; set;}
       [Alias("PersonName")]
       [StringLength(60)]
       public string Name { get; set;}
       public int BirthCityId { get; set;}
       public int? JobCityId { get; set;}
       
       [BelongsTo(typeof(City), "Name")]
       public  string BirthCity {get; set;}

       [BelongsTo(typeof(Country),"Name")]
       public  string BirthCountry {get; set;}

       [BelongsTo(typeof(Country))]
       public string  Continent { get; set;}

       [BelongsTo(typeof(City),PropertyName="Name",ParentAlias="C2")]
       public string JobCity {get; set;}

       [BelongsTo(typeof(Country),"Name", ParentAlias="C3")]
       public  string JobCountry {get; set;}
    }



}
