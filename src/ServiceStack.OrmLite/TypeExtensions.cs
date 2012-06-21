using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

    }
}

