using ClassDemoMusicLib.model;
using ClassDemoMusicLib.repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassDemoMusicRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {
        private readonly MusicRepository _repo;

        public MusicsController(MusicRepository repo)
        {
            _repo = repo;
        }



        // GET: api/<MusicsController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Music> list = _repo.GetAll();
            if (list.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }
        }

        // GET api/<MusicsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Music m = _repo.GetById(id);
                return Ok(m);
            }catch( KeyNotFoundException knfe)
            {
                return NotFound();
            }
        }

        // POST api/<MusicsController>
        [HttpPost]
        public IActionResult Post([FromBody] Music value)
        {
            Music m = _repo.Add(value);

            return Created("api/Musics"+m.Id,m);
        }

        // PUT api/<MusicsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Music value)
        {
            try
            {
                Music music = _repo.Update(id, value);
                return Ok(music);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound();
            }
        }

        // DELETE api/<MusicsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Music music = _repo.Delete(id);
                return Ok(music);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound();
            }
        }
    }
}
