using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(User user)
        {
            try
            {
                var um = new UserManager();
                um.Create(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetriveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var mm = new MovieManager();
                var listResults = mm.RetrieveAll();
                return Ok(listResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetriveById")]
        public ActionResult RetrieveById(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                var listResults = mm.RetrieveById(movie);
                return Ok(listResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                var updatedMovie = mm.Update(movie);
                return Ok(updatedMovie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                var deletedMovie = mm.Delete(movie);
                return Ok(deletedMovie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
