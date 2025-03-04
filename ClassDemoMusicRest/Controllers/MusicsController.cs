using ClassDemoMusicLib.model;
using ClassDemoMusicLib.repositories;
using ClassDemoMusicRest.model;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        [HttpGet]
        [Route("SortByTitle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Sort()
        {
            // kunne lave en metode i repositoriet
            // eller sortere på klient siden i fx. JavaScript
            List<Music> liste = _repo.GetAll();
            liste.Sort( (a,b) => a.Title.CompareTo(b.Title));
            return Ok(liste);
        }

        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Filtrer([FromQuery] MusicFilter filter)
        {
            // hack - burde laves i repository - er ændret til repo
            List<Music> liste = _repo.Search(filter.FromYear, filter.ToYear);
            
            if (liste.Count > 0)
            {
                return Ok(liste);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<MusicsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] MusicDTO dto)
        {
            try
            {
                Music m = _repo.Add(MusicConverter.DTO2Music(dto));

                return Created("api/Musics" + m.Id, m);
            }catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        // PUT api/<MusicsController>/5
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] MusicDTO dto)
        {
            try
            {
                Music music = _repo.Update(id, MusicConverter.DTO2Music(dto));
                return Ok(music);
            }
            catch(ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound();
            }
        }

        // DELETE api/<MusicsController>/5
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
