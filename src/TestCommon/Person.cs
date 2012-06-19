using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace TestCommon
{
   public class Person
   {
       public Person ()
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
       
   }
}
