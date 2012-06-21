using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.OrmLite;

namespace ServiceStack.DataAnnotations{
    
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class BelongsToAttribute:Attribute
    {
        private string parentAlias;

        public BelongsToAttribute (Type parent):this(parent, null)
        {

        }

        public BelongsToAttribute (Type parent, string propertyName)
        {
            Parent= parent;
            parentAlias= parent.GetModelName();
            PropertyName=propertyName;
        }

        public Type Parent { get; set;}

        public string PropertyName{ get; set;}

        public string ParentAlias {
            get {
                return parentAlias;
            }
            set {
                parentAlias = value;
            }
        }       

    }

}
