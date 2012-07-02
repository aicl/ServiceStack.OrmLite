using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using ServiceStack.Common.Extensions;
using ServiceStack.DataAnnotations;
using ServiceStack.Text;

namespace ServiceStack.OrmLite
{
    public static class TypeExtensions
    {
        public static string GetModelName(this Type type)
        {
            var aliasAttr = type.FirstAttribute<AliasAttribute>();

            return aliasAttr!=null? aliasAttr.Name:type.Name;

        }


        public static string GetFieldName(this Type type, string propertyName)
        {
            var pi = type.GetProperty(propertyName);
            if (pi==null) 
                throw new ArgumentException(string.Format("Type '{0}' does not contain property with name: '{1}",
                                                          type.Name, propertyName));

            var aliasAttr = pi.FirstAttribute<AliasAttribute>();

            return aliasAttr!=null? aliasAttr.Name:pi.Name;

        }
    }
}

