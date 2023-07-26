using Microsoft.EntityFrameworkCore;
using TheLions.Models.User;

namespace TheLions.DAL
{
    public class AppDbContext :DbContext
    {
        
        public AppDbContext()
        {
           
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
          
        }
       
       
        public  DbSet<User> Users { get; set; }
        public  DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
                      modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Tbl_User","User");
                
                user.Property(u => u.UserName).HasMaxLength(50).IsUnicode(true);
                user.Property(u => u.Email).HasMaxLength(50).IsUnicode(true);
                user.Property(u => u.Password).HasMaxLength(500).IsUnicode(false);
                user.HasMany(user => user.Roles).WithMany();
            });
            #endregion
            #region Role
 modelBuilder.Entity<Role>(entity =>
            {
              
            });
            #endregion
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Initial Catalog=TheLion;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;");
        }


    }
}
