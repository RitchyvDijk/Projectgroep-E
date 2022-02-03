using System;
using Xunit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using webapplication.Controllers;

namespace tests
{
    public class UnitTest1
    {
        ChatDbContext ChatDbcontext;
        AfspraakDbContext AfspraakDbcontext;
        GebruikerDbContext identityDbContext;
        UserManager<Gebruiker> userManager;
        RoleManager<IdentityRole> roleManager;
        GebruikerDbContext GebruikerContext;

        // [Fact]
        // public async Task Test1AsyncAsync()
        // {
        //     string jongerdan12 = "< 12 jaar";
        //     string tussen1216 = "12 tot 16 jaar";
        //     string ouderdan16 = "> 16 jaar";

        //     //string Nicknaam, Hulpverlener hulpverlener, int GebJaar, int BSN, string Probleem 
        //     int JTjonger12 = 2015;
        //     int JTouder16 = 2000;
        //     int JTt1216 = 2008;

        //     string pw = "Test@01";
           
        //     var jongeUser = new Client { GebJaar = JTjonger12 };
        //     await userManager.CreateAsync(jongeUser, pw);

        //     var middelUser = new Client { GebJaar = JTt1216 };
        //     await userManager.CreateAsync(middelUser, pw);

        //     var OudeUser = new Client { GebJaar = JTouder16 };
        //     await userManager.CreateAsync(OudeUser, pw);

        //     Assert.Matches(jongeUser.LeeftijdsGroep(), jongerdan12);
        //     Assert.Matches(middelUser.LeeftijdsGroep(), tussen1216);
        //     Assert.Matches(OudeUser.LeeftijdsGroep(), ouderdan16);
        // }
        [Fact]
        public void TestoverzichtAanmeldingenReq18()
        {
            //await _context.afspraakModel.ToListAsync()
            var ac = new AfspraakController(AfspraakDbcontext, identityDbContext, userManager, roleManager);
            var result = ac.Index();
            Assert.NotNull(result);
        }

        [Fact]
        public void TestGroupChat()
        {
            string topic = "faalangst";
            string zoek = "faalangst";
            string age = "16";
            
            var GroupController = new GroupController(ChatDbcontext);
            var result = GroupController.Index(topic,zoek,age);
            Assert.NotNull(result);
        }
        
        [Fact]
        public void TestPriveChat()
        {
            var PriveChatController = new PriveChatController(ChatDbcontext, userManager, GebruikerContext);
            var result = PriveChatController.Index();
            Assert.NotNull(result);
        }

    }
}
