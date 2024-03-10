using Fotocopy.Models;
using Fotocopy.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fotocopy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotocopyController : ControllerBase
    {
        private readonly IPaperRepository _paperRepository;
        public FotocopyController(IPaperRepository paperRepository) 
        { 
            _paperRepository = paperRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllPaper() 
        { 
            var paper = await _paperRepository.GetAllPaperAsync();
            return Ok(paper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaperById([FromRoute] int id)
        {
            var onepaper = await _paperRepository.GetPaperByIdAsync(id);
            return Ok(onepaper);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewPaper([FromBody]PaperModel paperModel)
        {
            var id = await _paperRepository.AddPaperAsync(paperModel);
            return CreatedAtAction(nameof(GetPaperById), new {id = id, controller = "paper" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaper([FromBody] PaperModel paperModel, [FromRoute] int id)
        {
            await _paperRepository.UpdatePaperAsync(id, paperModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePaperPatch([FromBody] JsonPatchDocument paperModel, [FromRoute] int id)
        {
            await _paperRepository.UpdatePaperPatchAsync(id, paperModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdatePaper([FromRoute] int id)
        {
            await _paperRepository.DeletePaperAsync(id);
            return Ok();
        }
    }
}
