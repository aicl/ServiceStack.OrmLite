using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common.Extensions;
using ServiceStack.OrmLite;

namespace ServiceStack.DataAnnotations{

    public enum JoinType{
        Inner,
        Left,
        Right
    }
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class JoinToAttribute:Attribute{

        private string childAlias;
        private string parentAlias;
        private JoinType joinType;

        public JoinToAttribute(Type child,string parentProperty, string childProperty)
            :this(null, child,parentProperty, childProperty){}


        public JoinToAttribute(Type parent, Type child,
                               string parentProperty, string childProperty )
        {

            Child=child;
            Parent=parent;
            ParentProperty=parentProperty;
            ChildProperty= childProperty;

            childAlias= child.GetModelName();
            parentAlias= parent!=null? parent.GetModelName():null;

            joinType = JoinType.Inner;

        }

        public Type Child {get ;set;}

        public Type Parent {get ;set;}

        public string  ChildAlias{
            get{ return childAlias;}
            set{ childAlias =value;}
        }

        public string ParentAlias {
            get {
                return parentAlias;
            }
            set {
                parentAlias = value;
            }
        }       

        public String ParentProperty{ get; set;}

        public String ChildProperty{ get; set;}

        public JoinType JoinType {
            get {
                return joinType;
            }
            set {
                joinType = value;
            }
        }   

        public int Order{get; set;}

    }
}
