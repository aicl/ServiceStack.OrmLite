using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace TestCommon
{
    [Alias("Countries")]
    public class Country
    {
        public Country ()
        {
        }
        
        //[Alias("CountryId")]
        [Alias("Id")]
        [AutoIncrement]
        public int Id { get; set;}
        [StringLength(40)]
        public string Name { get; set;}
        [StringLength(30)]
        public string Continent{get; set;}

    }
}
