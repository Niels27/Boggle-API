using  Microsoft.EntityFrameworkCore;
using  BoggleWebApi.Models;

namespace BoggleWebApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<BoggleBoard> Boards { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {

        }
    }
}
