using HatService.API.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatService.API.Data.Entities
{
    public class Hat : BaseEntity
    {
        public string Name { get; set; }
        public byte HatType { get; set; }
    }
}
