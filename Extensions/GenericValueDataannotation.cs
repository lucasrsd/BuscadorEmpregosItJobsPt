using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Crawler_ItJobs_Portugal.Extensions
{
    public static class GenericValueDataannotation
    {
        public static T GetAttributeFrom<T> (this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof (T);
            var property = instance.GetType ().GetProperty (propertyName);
            return (T) property.GetCustomAttributes (attrType, false).First ();
        }
    }
}