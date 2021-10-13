using DIOApi.Data_Transfer_Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIOApi.Services.Data_Access_Layer
{
    public class LoginDBContext : DbContext
    {
        public DbSet<LoginModel> loginInfo { get; set; }
        public LoginDBContext(DbContextOptions<LoginDBContext> options) : base(options) { }
    }
}
