using CatService.API.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.API.Data.Entities
{
    [FakeEntity]
    public class CatWithHatView
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string CatBreed { get; set; }
        public int? HatId { get; set; }
        public string HatName { get; set; }
    }
}
