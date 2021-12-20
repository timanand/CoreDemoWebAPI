using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;
using CoreDemoWebAPI.Domain;


using CoreDemoWebAPI.Data;

namespace CoreDemoWebAPI.Controllers
{
    [ApiController]
    public class StaffMembersController : ControllerBase
    {

		private readonly IUow _uow;

		public StaffMembersController(IUow uow)
        {
			_uow = uow;
        }


		// Get Staff Members
		[HttpGet]
		[Route("api/staffmembers")] // this end point or url is important !
		public IActionResult GetEmployees() // can give any name here
		{
			var staffList = _uow.StaffRepository.GetAll();
			return Ok(staffList.ToList()); // status code of 200 and json data will get returned
		}


		// Adds new record
		[HttpPost]
		[Route("api/staffmembers/create")]
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
		[Route("api/staffmembers/update/{id}")]
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
		[Route("api/staffmembers/delete/{id}")]
		public IActionResult DeleteEmployee(int id, StaffMember staffMember)
		{

			if (id !=null)
            {
				_uow.StaffRepository.Delete(id);
				return Ok(staffMember);
			}

			return BadRequest(ModelState); // 400 + error message in json format
		}

	}
}
