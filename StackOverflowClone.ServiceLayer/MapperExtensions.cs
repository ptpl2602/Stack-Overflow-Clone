using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace StackOverflowClone.ServiceLayer
{
    public static class MapperExtensions
    {
       public static void IgnoreUnmappedProperties(TypeMap map, IMappingExpression expr)
        {
            foreach(string propName in map.GetUnmappedPropertyNames())
            {
                if(map.SourceType.GetProperty(propName) != null)
                {
                    expr.ForMember(propName, i => i.Ignore());
                }
                if(map.DestinationType.GetProperty(propName) != null)
                {
                    expr.ForMember(propName, i => i.Ignore());
                }
            }
        }

        public static void IgnoreUnmapped(this IProfileExpression profile)
        {
            profile.ForAllMaps(IgnoreUnmappedProperties);
        }
    }
}
