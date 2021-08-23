using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelController : ControllerBase
    {
        public TVContext Context { get; set; }

        public ChannelController(TVContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("{networkId}")]
        public async Task<ActionResult<List<Channel>>> Get(int networkId)
        {
            var network = await Context.Networks.Where(net => net.ID == networkId).Include(net => net.Channels).ThenInclude(ch => ch.Slots).FirstOrDefaultAsync();
            if(network == null)
                return BadRequest("Mreza nije nadjena.");
            return Ok(network.Channels);
        }

        [HttpPost]
        [Route("{networkId}")]
        public async Task<ActionResult<Channel>> Create(int networkId, [FromBody] Channel ch)
        {    
            var network = await Context.Networks.Where(net => net.ID == networkId).Include(net => net.Channels).FirstOrDefaultAsync();
            if(network == null)
                return BadRequest("Mreza nije nadjena.");
            
            if(network.Channels == null)
                network.Channels = new List<Channel>();
            network.Channels.Add(ch);
            
            if(ch.Slots == null)
                ch.Slots = Enumerable.Range(0, 24).Select(i => new Slot{
                    Hour = i,
                    Content = "Prazan",
                    Color = "white",
                }).ToList();
            await Context.SaveChangesAsync();
            return Ok(ch);
        }

        [HttpPut]
        public async Task<Channel> Update([FromBody] Channel ch)
        {    
            Context.Channels.Update(ch);
            await Context.SaveChangesAsync();
            return ch;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {    
            var ch = await Context.Channels.Where(ch => ch.ID == id).Include(ch => ch.Slots).FirstOrDefaultAsync();
            if(ch == null)
                return BadRequest("Kanal nije nadjen.");
            Context.Channels.Remove(ch);
            await Context.SaveChangesAsync();
            return Ok(new Object {});
        }
    }
}
