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
        private readonly ChatDbContext _ChatContext;
        private readonly GebruikerDbContext _GebruikerContext;

        public ApiController(ChatDbContext ChatContext, GebruikerDbContext GebruikerContext)
        {
            _ChatContext = ChatContext;
            _GebruikerContext = GebruikerContext;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<PriveChat>> GetPriveChat(string id)
        {
            var ClientId = _GebruikerContext.Users.Where(u => u.Email == id).FirstOrDefault().Id;
            return _ChatContext.PriveChat.Where(p => p.Afzender == ClientId || p.Ontvanger == ClientId).ToList();
        }

        // GET: api/PriveChat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriveChat>>> GetPriveChat()
        {
            return await _ChatContext.PriveChat.ToListAsync();
        }

        // PUT: api/PriveChat/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriveChat(int id, PriveChat priveChat)
        {
            if (id != priveChat.Id)
            {
                return BadRequest();
            }

            _ChatContext.Entry(priveChat).State = EntityState.Modified;

            try
            {
                await _ChatContext.SaveChangesAsync();
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
            _ChatContext.PriveChat.Add(priveChat);
            await _ChatContext.SaveChangesAsync();

            return CreatedAtAction("GetPriveChat", priveChat);
        }

        // DELETE: api/PriveChat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriveChat(int id)
        {
            var priveChat = await _ChatContext.PriveChat.FindAsync(id);

            if (priveChat == null)
            {
                return NotFound();
            }

            _ChatContext.PriveChat.Remove(priveChat);

            await _ChatContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PriveChatExists(int id)
        {
            return _ChatContext.PriveChat.Any(e => e.Id == id);
        }
    }
}
