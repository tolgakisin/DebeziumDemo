using CatService.API.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.API.Data.Entities
{
    public class Cat : BaseEntity
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public int HatId { get; set; }
    }
}
