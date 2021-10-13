using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DIOApi.DTOs;

namespace DIOApi.Formatters
{
    public class GameItemCSVFormatter : TextOutputFormatter
    {
        public GameItemCSVFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        //If it's not of GameItem, it doesn't write anything and returns other formatting like JSONs
        protected override bool CanWriteType(Type type)
        {
            return type == typeof(GameItem);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var csvGame = "";

            if (context.Object is GameItem)
            {
                var game = (GameItem)context.Object;
                csvGame = $"{game.Name},{game.Publisher},{game.Platforms},{game.Year},{game.Description}";
            }

            using (var writer = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
            {
                return writer.WriteAsync(csvGame);
            }
        }
    }
}
