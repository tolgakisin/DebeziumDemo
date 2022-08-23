using CatService.API.Data.Common;
using CatService.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatWithHatController : ControllerBase
    {
        private readonly Context _context;
        public CatWithHatController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<IEnumerable<CatWithHatView>> GetView()
        {
            return _context.GetCatWithHatView();
        }
    }
}
