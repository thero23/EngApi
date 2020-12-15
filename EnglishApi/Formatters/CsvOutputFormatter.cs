using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace EnglishApi.Formatters
{
    public class CsvOutputFormatter:TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(WordGetDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<WordGetDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext
            context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<WordGetDto>)
            {
                foreach (var word in (IEnumerable<WordGetDto>)context.Object)
                {
                    FormatCsv(buffer, word);
                }
            }
            else
            {
                FormatCsv(buffer, (WordGetDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, WordGetDto word)
        {
            buffer.AppendLine($"{word.Id},\"{word.Original},\"{word.Translate}\"");
        }
    }
}
