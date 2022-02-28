using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CoreDemoWebAPI.Domain;
using CoreDemoWebAPI.Data;

using Microsoft.AspNetCore.Authorization; // Added by AANA


namespace CoreDemoWebAPI.Controllers
{
	[Authorize] // Added by AANA
	[ApiController]
	[ApiVersion("1.0")]
	[ApiExplorerSettings(GroupName = "v1")]
	public class StaffMembersController : ControllerBase
    {

		private readonly IUow _uow;

		private readonly IJwtAuthenticationManager jwtAuthenticationManager; // Added by AANA
		public StaffMembersController(IUow uow, IJwtAuthenticationManager jwtAuthenticationManager) // Added by AANA
		{
			_uow = uow;
			this.jwtAuthenticationManager = jwtAuthenticationManager;
		}


		// Get Staff Members
		[HttpGet]
		[Route("api/staffmembers/read")] // this end point or url is important !
		public IActionResult GetEmployees() // can give any name here
		{
			var staffList = _uow.StaffRepository.GetAll();

			//Global Error Handling - BEGIN
			// Uncomment below line to force exception and it goes into global error handling
			//throw new Exception("sdsd");
			//Global Error Handling - END

			return Ok(staffList.ToList()); // status code of 200 and json data will get returned

		}


		// Get Staff Member
		[HttpGet]
		[Route("api/staffmembers/read/{id}")] // this end point or url is important !
		public IActionResult GetEmployee(int id) // can give any name here
		{
			StaffMember staffMember = _uow.StaffRepository.GetById(id);
			return Ok(staffMember); // status code of 200 and json data will get returned
		}



		// Adds new record
		[HttpPost]
		[Route("api/staffmembers/create")] // this end point or url is important ! 
		public IActionResult PostEmployee(StaffMember staffMember)
        {
			if (ModelState.IsValid)
            {
				_uow.StaffRepository.Add(staffMember);

				// 201
				return Created("", staffMember);
            }

			return BadRequest(ModelState); // 400 + error message in json format
        }



		// Updates record
		[HttpPut]
		[Route("api/staffmembers/update/{id}")] // this end point or url is important !
		public IActionResult PutEmployee(int id, StaffMember staffMember)
		{
			if(ModelState.IsValid)
            {
				if (id != staffMember.Id)
					return BadRequest("Id is not matching");

				_uow.StaffRepository.Edit(staffMember);

				// 200
				return Ok(staffMember);

			}

			return BadRequest(ModelState); // 400 + error message in json format
		}


		// Delete record
		[HttpDelete]
		[Route("api/staffmembers/delete/{id}")] // this end point or url is important !
		public IActionResult DeleteEmployee(int? id, StaffMember staffMember)
		{

			if (id !=null)
            {
				_uow.StaffRepository.Delete(id);
				return Ok(staffMember);
			}

			return BadRequest(ModelState); // 400 + error message in json format
		}


		// AANA - BEGIN
		//[HttpPost("authenticate")]

		[AllowAnonymous] // without this line, no one can call this method
		[HttpPost]
		[Route("api/staffmembers/authenticate")] // this end point or url is important !
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
