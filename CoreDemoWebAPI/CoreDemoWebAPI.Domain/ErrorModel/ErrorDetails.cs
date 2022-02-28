//Global Error Handling - BEGIN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;


namespace CoreDemoWebAPI.Domain.ErrorModel
{
    public class ErrorDetails
    {

	public int StatusCode { get; set; }
		public string Message { get; set; }

		public override string ToString()
		{
			// 'this' means the current class object. 
			// So whenever someone has the object of the ErrorDetails class,
			// they will be executing the ToString method and will get the data out in JSON format.
			return JsonSerializer.Serialize(this);
		}

	}
}

//Global Error Handling - END
