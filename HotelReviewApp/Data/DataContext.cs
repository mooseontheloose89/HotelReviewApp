using HotelReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReviewApp.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
                    
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
