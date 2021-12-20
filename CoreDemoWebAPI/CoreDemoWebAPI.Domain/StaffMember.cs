using System;
using System.ComponentModel.DataAnnotations;

namespace CoreDemoWebAPI.Domain
{
    public class StaffMember
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Title { get; set; }

    }

}
