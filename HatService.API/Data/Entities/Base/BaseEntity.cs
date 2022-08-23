using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatService.API.Data.Entities.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
