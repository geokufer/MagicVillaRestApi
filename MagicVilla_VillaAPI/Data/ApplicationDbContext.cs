using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Villa?> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Villa 1",
                    Description = "Villa 1 Description",
                    Rate = 100,
                    Occupancy = 4,
                    SquareMeters = 200,
                    Amenity = "Villa 1 Amenity",
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedDate = System.DateTime.Now,
                    UpdatedDate = System.DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Villa 2",
                    Description = "Villa 2 Description",
                    Rate = 200,
                    Occupancy = 6,
                    SquareMeters = 300,
                    Amenity = "Villa 2 Amenity",
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedDate = System.DateTime.Now,
                    UpdatedDate = System.DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Name = "Villa 3",
                    Description = "Villa 3 Description",
                    Rate = 300,
                    Occupancy = 8,
                    SquareMeters = 400,
                    Amenity = "Villa 3 Amenity",
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedDate = System.DateTime.Now,
                    UpdatedDate = System.DateTime.Now
                },
                new Villa
                {
                    Id = 4,
                    Name = "Villa 4",
                    Description = "Villa 4 Description",
                    Rate = 400,
                    Occupancy = 10,
                    SquareMeters = 500,
                    Amenity = "Villa 4 Amenity",
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedDate = System.DateTime.Now,
                    UpdatedDate = System.DateTime.Now
                },
                new Villa
                {
                    Id = 5,
                    Name = "Villa 5",
                    Description = "Villa 5 Description",
                    Rate = 500,
                    Occupancy = 12,
                    SquareMeters = 600,
                    Amenity = "Villa 5 Amenity",
                    ImageUrl = "https://via.placeholder.com/150",
                    CreatedDate = System.DateTime.Now,
                    UpdatedDate = System.DateTime.Now
                }
            );
        }
    }
}
