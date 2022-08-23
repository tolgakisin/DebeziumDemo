using HatService.API.Data.Common;
using HatService.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HatController : ControllerBase
    {
        private readonly Context _context;
        public HatController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Hat>> GetAll()
        {
            return _context.Hats.ToListAsync();
        }

        [HttpPost]
        public async Task<Hat> Add()
        {
            var rand = new Random();
            var hat = new Hat
            {
                Name = $"Hat-{rand.Next(1, 1000)}",
                HatType = (byte)rand.Next(1, 5),
            };

            var added = (await _context.AddAsync(hat)).Entity;
            await _context.SaveChangesAsync();

            return added;
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            var hat = await _context.Hats.FindAsync(id);
            _context.Remove(hat);

            return await _context.SaveChangesAsync() > 0;
        }

        [HttpPut]
        public async Task<Hat> Update(int id, string name, byte hatType)
        {
            var hat = await _context.Hats.FindAsync(id);
            if (hat == null) return null;

            hat.Name = name;
            hat.HatType = hatType;
            _context.Update(hat);
            await _context.SaveChangesAsync();

            return hat;
        }
    }
}
