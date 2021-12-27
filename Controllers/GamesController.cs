using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        GamesContext db;
        public GamesController(GamesContext context)
        {
            db = context;
            if (!db.Games.Any())
            {
                db.Games.Add(new Games { Name = "GTA", Price = 5 });
                db.Games.Add(new Games { Name = "GTA III", Price = 10 });
                db.Games.Add(new Games { Name = "GTA San Andreas", Price = 25 });
                db.Games.Add(new Games { Name = "GTA IV", Price = 40 });
                db.Games.Add(new Games { Name = "GTA V", Price = 50 });
                db.SaveChanges();
            }
        }

        // GET: Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Games>>> Get()
        {
            return await db.Games.ToListAsync();
        }

        // GET: Games/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> Get(int id)
        {
            Games games = await db.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (games == null)
                return NotFound();
            return new ObjectResult(games);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Games>> Post(Games games)
        {
            if (games == null)
            {
                return BadRequest();
            }

            db.Games.Add(games);
            await db.SaveChangesAsync();
            return Ok(games);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Games>> Put(Games games)
        {
            if (games == null)
            {
                return BadRequest();
            }
            if (!db.Games.Any(x => x.Id == games.Id))
            {
                return NotFound();
            }

            db.Update(games);
            await db.SaveChangesAsync();
            return Ok(games);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Games>> Delete(int id)
        {
            Games games = db.Games.FirstOrDefault(x => x.Id == id);
            if (games == null)
            {
                return NotFound();
            }
            db.Games.Remove(games);
            await db.SaveChangesAsync();
            return Ok(games);
        }
    }
}
