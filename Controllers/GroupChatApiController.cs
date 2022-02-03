using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using webapplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace week13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatApiController : ControllerBase
    {
        private readonly ChatDbContext _context;
        private readonly GebruikerDbContext _dbContext;


        public GroupChatApiController(ChatDbContext context, GebruikerDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        // GET: api/PriveChat
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GroupChat>>> GetGroupChats(int id)
        {
            var chat = await _context.GroupChats.Where(p => p.GroupId == id).ToListAsync();
            for (int i = 0; i < _context.GroupChats.Where(p => p.GroupId == id).Count(); i++)
            {
                var naam = _dbContext.Users.Where(u => u.Id == chat[i].Afzender).FirstOrDefault();
                chat[i].DateTime = naam.UserName;
            }
            return chat;
        }

        // PUT: api/PriveChat/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupChats(int id, GroupChat GroupChats)
        {
            if (id != GroupChats.Id)
            {
                return BadRequest();
            }

            _context.Entry(GroupChats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupChatsExists(id))
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
        public async Task<ActionResult<GroupChat>> PostPriveChat(GroupChat GroupChat)
        {
            _context.GroupChats.Add(GroupChat);
            await _context.SaveChangesAsync();
            var naam = _dbContext.Users.Where(p => p.Id == GroupChat.Afzender).FirstOrDefault();
            GroupChat.DateTime = naam.UserName;

            return CreatedAtAction("GetGroupChats", GroupChat);
        }

        // DELETE: api/PriveChat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupChats(int id)
        {
            var GroupChat = await _context.GroupChats.FindAsync(id);

            if (GroupChat == null)
            {
                return NotFound();
            }

            _context.GroupChats.Remove(GroupChat);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupChatsExists(int id)
        {
            return _context.GroupChats.Any(e => e.Id == id);
        }
    }
}
