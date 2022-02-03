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
        private readonly MyContext _context;
        private readonly webapplicationIdentityDbContext _dbContext;


        public GroupChatApiController(MyContext context, webapplicationIdentityDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }
        
        // GET: api/PriveChat
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GroupChat>>> GetGroupChats(int id)
        {
            var chat = await _context.GroupChats.Where(p => p.GroupId == id).ToListAsync();
            for (int i = 0; i < _context.GroupChats.Where(p => p.GroupId == id).Count(); i++){
                var naam = _dbContext.Users.Where(u => u.Id == chat[i].Afzender).FirstOrDefault();
                chat[i].DateTime = naam.UserName;
            }
            return chat;
        }

        // PUT: api/PriveChat/5
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<GroupChat>> PutGroupChats(int id)
        {
            var chat1 = _context.GroupChats.Where(p => p.Id == id).SingleOrDefault();
            chat1.Meld = true;
            var groupId = chat1.GroupId;
            _context.SaveChanges();
            var chat = _context.GroupChats.Where(p => p.GroupId == groupId).ToList();
            for (int i = 0; i < _context.GroupChats.Where(p => p.GroupId == groupId).Count(); i++){
                var naam = _dbContext.Users.Where(u => u.Id == chat[i].Afzender).FirstOrDefault();
                chat[i].DateTime = naam.UserName;
            }
            return chat;
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
        public ActionResult<IEnumerable<GroupChat>> DeleteGroupChats(int id)
        {
            GroupChat GroupChat = _context.GroupChats.Where(p => p.Id == id).FirstOrDefault();

            if (GroupChat == null)
            {
                return NotFound();
            }

            _context.GroupChats.Remove(GroupChat);

            _context.SaveChangesAsync();

            var groupId = GroupChat.GroupId;
             var chat = _context.GroupChats.Where(p => p.GroupId == groupId).ToList();
            for (int i = 0; i < _context.GroupChats.Where(p => p.GroupId == groupId).Count(); i++){
                var naam = _dbContext.Users.Where(u => u.Id == chat[i].Afzender).FirstOrDefault();
                chat[i].DateTime = naam.UserName;
            }
            return chat;
        }

        private bool GroupChatsExists(int id)
        {
            return _context.GroupChats.Any(e => e.Id == id);
        }
    }
}
