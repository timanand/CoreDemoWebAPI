using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Microsoft.AspNetCore.Authorization;


namespace CoreDemoWebAPI.Controllers
{
    [Authorize] // Added this line by AANA
    //[Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        // GET: api/<NameController>
        [HttpGet]
        [Route("api/name/read")]
        public IEnumerable<string> Get()
        {
            return new string[] { "New Jersey", "New York" };
        }

        // GET api/<NameController>/5
        [HttpGet("{id}")]
        public string   Get(int id)
        {
            return "value";
        }





        //// POST api/<NameController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        //// PUT api/<NameController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<NameController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


        // AANA - BEGIN
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public NameController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // AANA - END



        // AANA - BEGIN
        [AllowAnonymous] // without this line, no one can call this method
        [HttpPost("authenticate")]
        [Route("api/name/authenticate")] // this end point or url is important !
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jwtAuthenticationManager.Authenticate
                (userCred.Username, userCred.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
        // AANA - END


    }
}
