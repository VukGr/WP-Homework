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
    public class SlotController : ControllerBase
    {
        public TVContext Context { get; set; }

        public SlotController(TVContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("{channelId}")]
        public async Task<ActionResult<List<Slot>>> Get(int channelId)
        {
            var channel = await Context.Channels.FindAsync(channelId);
            if(channel == null)
                return BadRequest("Kanal nije nadjena.");
            return Ok(channel.Slots);
        }

        [HttpPut]
        public async Task<ActionResult<Slot>> Update([FromBody] Slot slot)
        {    
            if(slot.Hour < 0 || slot.Hour >= 24)
                return BadRequest("Ne validan slot: " + slot.ToString()); 
            var oldSlot = await Context.Slots.FindAsync(slot.ID);
            if(oldSlot.Content != "Prazan")
                return BadRequest("Slot " + slot.Hour + " je vec popunjen");
            oldSlot.Content = slot.Content;
            oldSlot.Color = slot.Color;
            await Context.SaveChangesAsync();
            return Ok(slot);
        }
    }
}
