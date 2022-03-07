// Pagination - BEGIN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemoWebAPI.Models
{
    public class Paginator
    {
        public int per_page { get; set; }
        public int current_page { get; set; }


        // Constructor
        public Paginator()
        {
            this.per_page = 2;
            this.current_page = 1;
        }


        // Constructor
        public Paginator(int per_page, int current_page)
        {
            this.per_page = per_page > 5 ? 5 : per_page;
            this.current_page = current_page < 1 ? 1: current_page;
        }


    }
}

// Pagination - END