// Custom CSV Formatter - BEGIN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using CoreDemoWebAPI.Domain;
using Microsoft.AspNetCore.Http;

namespace CoreDemoWebAPI
{
    public class CsvOutputFormatter : TextOutputFormatter
    {

        // Constructor
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode); // so application can work with other languages (Greek, Latin, Chinese etc...)
        }


        protected override bool CanWriteType(Type type)
        {
            if(typeof(StaffMember).IsAssignableFrom(type) || typeof(IEnumerable<StaffMember>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
            
        }


        // This will be responsible for giving the data out 
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var sb = new StringBuilder();

            if (context.Object is IEnumerable<StaffMember>)
            {

                //foreach(var staffmember in (IEnumerable<StaffMember>)context.Object)
                foreach(var staffmember in context.Object as IEnumerable<StaffMember>)
                {
                    FormatCsv(sb, staffmember);
                }

            }

            else 
            {
                FormatCsv(sb, context.Object as StaffMember);
            }

            await response.WriteAsync(sb.ToString());

        }


        private static void FormatCsv(StringBuilder sb, StaffMember staffMember)
        {
            // returns quotes around each field
            //sb.AppendLine($"{staffMember.Id},\"{staffMember.LastName}\",\"{staffMember.FirstName}\"");

            // returns without quotes around each field
            sb.AppendLine($"{staffMember.Id}, {staffMember.LastName}, {staffMember.FirstName}");
        }



    }
}

// Custom CSV Formatter - END
