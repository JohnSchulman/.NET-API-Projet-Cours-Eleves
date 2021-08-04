using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace WebApplication1.Core
{
    public class DemoCSVFormatteur : TextOutputFormatter
    {
        public DemoCSVFormatteur()
        {
            SupportedMediaTypes.Add("application/csv");
            SupportedEncodings.Add(Encoding.UTF8);
        }
      /*  protected override bool CanWriteType(Type type)
        {
            if (type == typeof(CoursDTO))
                return true;
            return false;
        }
        */

        // permet de prendre toutes les types possibles
        protected override bool CanWriteType(Type type) => true;


        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            // je parcours mon objet si c'est un list je l'écrie en csv
            if (context.Object is IEnumerable list)
            {
                foreach (var item in list)
                {
                    await WriteCSV(context.HttpContext.Response.Body, selectedEncoding, item);
                }
            }
            // sinon je l'écrit tout simplement
            else
            {
                await WriteCSV(context.HttpContext.Response.Body, selectedEncoding, context.Object);
            }
        }

        private async Task WriteCSV(Stream stream, Encoding encoding, object data)
        {
            var str = data.GetType().GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(CsvAttribute), true).Count() > 0)
                .OrderBy(x => ((CsvAttribute)x.GetCustomAttributes(typeof(CsvAttribute), true).First()).Order)
                .Select(x => x.GetValue(data)?.ToString());

            var bytes = encoding.GetBytes(string.Join(';', str) + Environment.NewLine);
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

    }
}
