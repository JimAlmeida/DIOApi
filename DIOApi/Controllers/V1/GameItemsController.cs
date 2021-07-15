using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DIOApi.DAL;
using DIOApi.DTOs;
using DIOApi.Exceptions;

namespace DIOApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameItemsController : ControllerBase
    {
        private readonly GameDbContext _context;
        

        public GameItemsController(GameDbContext context)
        {
            _context = context;
            Query.setContext(context);
        }

        // GET: api/GameItems
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameItem))]
        public async Task<ActionResult<IEnumerable<GameItem>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameItem))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GameItem>>> Search([FromQuery] string name, [FromQuery] string publisher)
        {
            try
            {
                var queryResults = await Query.Search(name, publisher);
                return Ok(queryResults);
            }
            catch (NullQueryParameters nqp)
            {
                return BadRequest(nqp.Message);
            }
        }

        // GET: api/GameItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameItem>> GetGameItem(int id)
        {
            var gameItem = await _context.Games.FindAsync(id);

            if (gameItem == null)
            {
                return NotFound();
            }

            return gameItem;
        }

        // PUT: api/GameItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameItem(int id, GameItem gameItem)
        {
            if (id != gameItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameItem>> PostGameItem(GameItem gameItem)
        {
            if (!GameItemExists(gameItem.Name, gameItem.Publisher))
            {
                _context.Games.Add(gameItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGameItem", new { id = gameItem.Id }, gameItem);
            }
            else
            {
                return UnprocessableEntity("There's already a game with these characteristics");
            }
        }

        // DELETE: api/GameItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameItem(int id)
        {
            var gameItem = await _context.Games.FindAsync(id);
            if (gameItem == null)
            {
                return NotFound();
            }

            _context.Games.Remove(gameItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameItemExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        private bool GameItemExists(string name, string publisher)
        {
            return _context.Games.Any(item => (item.Name == name) && (item.Publisher == publisher));
        } 
    }
}
