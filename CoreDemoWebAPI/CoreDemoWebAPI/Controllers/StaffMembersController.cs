using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using CoreDemoWebAPI.Domain;
using CoreDemoWebAPI.Data;

using Microsoft.AspNetCore.Authorization; // Added by AANA
using CoreDemoWebAPI.Data.Interfaces;
using CoreDemoWebAPI.Models;

namespace CoreDemoWebAPI.Controllers
{
	//[Authorize] // Added by AANA
	[ApiController]
	[ApiVersion("1.0")]
	[ApiExplorerSettings(GroupName = "v1")]
	public class StaffMembersController : ControllerBase
    {

		private readonly IUow _uow;

		private readonly IJwtAuthenticationManager jwtAuthenticationManager; // Added by AANA

		private readonly ILoggerManager _logger;

		private readonly ITestClass _testClass;
		
		
		public StaffMembersController(IUow uow, IJwtAuthenticationManager jwtAuthenticationManager, ILoggerManager logger, ITestClass testClass) // Added by AANA
		{
			_uow = uow;
			this.jwtAuthenticationManager = jwtAuthenticationManager;
			_logger = logger;
			_testClass = testClass;
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

			_logger.LogInfo("Here is info message from our StaffMembersController - Read Action method");

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


		// TestClass - Dependency Injection - BEGIN

		// Get Staff Members
		[HttpGet]
		[Route("api/staffmembers/test")] // this end point or url is important !
		public IActionResult GetTest() // can give any name here
		{
			return Ok(_testClass.GetConnectionString());
		}

		// TestClass - Dependency Injection - END



		// From QUERY Attribute - BEGIN

		[HttpGet]
		//[Route("api/staffmembers/test2")] // this end point or url is important !
		[Route("api/staffmembers/test2/{name}")]
		// If we have the url
		//https://localhost:44351/api/staffmembers/test2/John?name=Kathy
		// name variable will hold 'Kathy'
		// ie. John will be ignored but will use Kathy
		public IActionResult GetTest2([FromQuery] string name)
		{
			return Ok($"Name = {name}");
		}


		[HttpGet]
		[Route("api/staffmembers/test3/{name}")]
		// If we have the url
		//https://localhost:44351/api/staffmembers/test3/John
		// name variable will hold 'John'
		public IActionResult GetTest3(string name)
		{
			return Ok($"Name = {name}");
		}



		[HttpPost]
		[Route("api/staffmembers/test4")]
		//https://localhost:44351/api/staffmembers/test5?FirstName=AAAAA&LastName=BBBBB

		// Body in Postman
		//{
		// "FirstName":"Tanya",
		//"LastName":"Kaur",
		//"Title":"Miss"
		//}
		public IActionResult GetTest4([FromQuery]StaffMember staffMember)
		{
			return Ok($"Name = {staffMember.FirstName}");
		}



		[HttpPost]
		[Route("api/staffmembers/test5")]
		//https://localhost:44351/api/staffmembers/test5?FirstName=AAAAA&LastName=BBBBB&Id=12
		
		// Body in Postman
		//{
		// "FirstName":"Tanya",
		//"LastName":"Kaur",
		//"Title":"Miss"
		//}

		public IActionResult GetTest5([FromQuery] int id, [FromQuery] StaffMember staffMember)
		{
			return Ok($"Name = {staffMember.FirstName}");
		}

		// From QUERY Attribute - END


		// Pagination - BEGIN
		//How to implement Pagination in ASP NET Core Web API
		// https://www.youtube.com/watch?v=cz_xniYNqww

		// Displays first page
		//https://localhost:44351/api/staffmembers/readpagination?per_page=1&current_page=1

		// Displays second page
		//https://localhost:44351/api/staffmembers/readpagination?per_page=1&current_page=2


		[Route("api/staffmembers/readpagination")] // this end point or url is important !
		public IActionResult GetEmployeesUsingPagination([FromQuery] Paginator filter) // can give any name here
		{

			var paginator = new Paginator(filter.per_page, filter.current_page);

			var staffList = _uow.StaffRepository.GetAll();

			return Ok(staffList.ToList()
				.Skip((paginator.current_page - 1) * paginator.per_page)
				.Take(paginator.per_page)); // status code of 200 and json data will get returned
		}

		// Pagination - END



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
