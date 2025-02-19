using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;

namespace NotesBackend.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly NotesDbContext _context;
        public TagController(NotesDbContext context)
        {
            _context = context;
        }

        // create a new tag
        [HttpPost]
        public async Task<IActionResult> CreateNewTag(TagCreateRequest newTag)
        {
            if (newTag == null)
            {
                return BadRequest("invalid request data");
            }

            var tag = TagMappers.FromNewTagToTagModel(newTag);
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return Ok(tag);
        }
    }
}
