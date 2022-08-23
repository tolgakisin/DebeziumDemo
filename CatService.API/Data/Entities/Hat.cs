using CatService.API.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.API.Data.Entities
{
    public class Hat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte HatType { get; set; }
    }
}
