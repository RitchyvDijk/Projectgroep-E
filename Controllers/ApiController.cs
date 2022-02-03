using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapplication.Areas.Identity.Data;

namespace week13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly webapplicationIdentityDbContext _dbContext;

        public ApiController(MyContext context, webapplicationIdentityDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PriveChat>> GetPriveChat(string id)
        {
            var ClientId = _dbContext.Users.Where(u => u.UserName == id).FirstOrDefault().Id;
            return _context.PriveChat.Where(p => p.Afzender == ClientId || p.Ontvanger == ClientId).ToList();
        }

         // GET: api/PriveChat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriveChat>>> GetPriveChat()
        {
            return await _context.PriveChat.ToListAsync();
        }

        // PUT: api/PriveChat/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriveChat(int id, PriveChat priveChat)
        {
            if (id != priveChat.Id)
            {
                return BadRequest();
            }

            _context.Entry(priveChat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriveChatExists(id))
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

        // POST: api/PriveChat
        [HttpPost]
        public async Task<ActionResult<PriveChat>> PostPriveChat(PriveChat priveChat)
        {
            _context.PriveChat.Add(priveChat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriveChat", priveChat);
        }

        // DELETE: api/PriveChat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriveChat(int id)
        {
            var priveChat = await _context.PriveChat.FindAsync(id);

            if (priveChat == null)
            {
                return NotFound();
            }

            _context.PriveChat.Remove(priveChat);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriveChatExists(int id)
        {
            return _context.PriveChat.Any(e => e.Id == id);
        }
    }
}
