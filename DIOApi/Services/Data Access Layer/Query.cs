using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIOApi.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using DIOApi.Exceptions;

namespace DIOApi.DAL
{
    public class Query
    {
        private static GameDbContext _context { get; set; }

        public async static Task<IEnumerable<GameItem>> Search(string name, string publisher)
        {
            if (name is not null)
            {
                return await ByName(name);
            }
            else if (publisher is not null)
            {
                return await ByPublisher(publisher);
            }
            else
            {
                throw new NullQueryParameters("No query parameters were provided!");
            }
        }

        private async static Task<IEnumerable<GameItem>> ByName(string name)
        {
            return await _context.Games.Where(item => item.Name == name).ToListAsync();
        }

        private async static Task<IEnumerable<GameItem>> ByPublisher(string publisher)
        {
            return await _context.Games.Where(item => (item.Publisher == publisher)).ToListAsync();
        }

        public static void setContext(GameDbContext dbContext) => _context = dbContext;
    }
}
