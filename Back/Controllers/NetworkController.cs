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
    public class NetworkController : ControllerBase
    {
        public TVContext Context { get; set; }

        public NetworkController(TVContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<List<Network>> GetAll()
        {
            return await Context.Networks.Include(net => net.Channels).ThenInclude(ch => ch.Slots).ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Network>> GetOne(int id)
        {
            var network = await Context.Networks.Where(net => net.ID == id).Include(net => net.Channels).ThenInclude(ch => ch.Slots).SingleOrDefaultAsync();
            if(network == null)
                return BadRequest("Mreza nije nadjena.");
            else
                return Ok(network);
        }

        [HttpPost]
        public async Task<Network> Create([FromBody] Network net)
        {
            Context.Networks.Add(net);
            await Context.SaveChangesAsync();
            return net;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var net = await Context.Networks.Where(ch => ch.ID == id).Include(net => net.Channels).ThenInclude(ch => ch.Slots).FirstOrDefaultAsync();
            if(net == null)
                return BadRequest("Mreza nije nadjen.");
            Context.Networks.Remove(net);
            await Context.SaveChangesAsync();
            return Ok();
        }
    }
}
