using CatService.API.Data.Common;
using CatService.API.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly Context _context;
        public CatController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Cat>> GetAll()
        {
            return _context.Cats.ToListAsync();
        }

        [HttpPost]
        public async Task<Cat> Add(Cat cat)
        {
            var added = (await _context.AddAsync(cat)).Entity;
            await _context.SaveChangesAsync();

            return added;
        }
    }
}
