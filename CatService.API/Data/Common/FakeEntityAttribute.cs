using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.API.Data.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FakeEntityAttribute : Attribute
    {
        public FakeEntityAttribute()
        {

        }
    }
}
