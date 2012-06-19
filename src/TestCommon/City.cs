using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace TestCommon
{
    public class City
    {
        public City ()
        {
        }

        [Alias("CityId")]
        [AutoIncrement]
        public int Id { get; set;}
        public int CountryId { get; set;}
        [StringLength(40)]
        public string Name { get; set;}
        [Alias("People")]
        public int Population{ get;set;}
    }
}

