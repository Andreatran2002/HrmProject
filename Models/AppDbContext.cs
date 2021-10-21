using App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Models{
    public class AppDbContext:IdentityDbContext<AppUser>
    // Khi kế thừa IdentityDbContext<AppUser> thì ngoài có bảng Posts thì còn có các bạn được định ngĩa trong User
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {
            //
            // this.RoleClaimss
            // IdentityRolw<string>
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Để nạp vô các table loại bỏ chữ aspnet mặc định 
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName= entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6)); 
                }
            }
        }

        // Tao bảng có dữ liệu của class Post
        public DbSet<Post> Posts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Education> Educations { get; set; }

    }
}