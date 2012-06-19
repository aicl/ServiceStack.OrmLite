using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.OrmLite;


namespace ServiceStack.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class SelectFromAttribute:Attribute
    {
        public SelectFromAttribute(Type fromTable)
            :this(fromTable, fromTable.GetModelDefinition().ModelName){}
        
        public SelectFromAttribute( Type fromTable, string alias)
        {
            From=fromTable;
            Alias = alias;
        }
                
        public Type From{ get; set;}
        
        public string  Alias{ get; set;}
        
    }
}