#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : IdentityDbContext<UserModel, RoleModel, string> //DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region identity
            //重写asp.net core identity表以及表之间的关系
            builder.Entity<UserModel>(options =>
            {
                options.HasKey(x => x.Id);
                options.ToTable("Tbl_User").HasKey(x => x.Id);

                // Each User can have many UserClaims
                options.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                options.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                options.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                options.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            });
            builder.Entity<IdentityUserClaim<string>>(options => options.ToTable("Tbl_UserClaim").HasKey(k => k.Id));
            builder.Entity<IdentityUserToken<string>>(options => options.ToTable("Tbl_UserToken").HasKey(k => new { k.UserId, k.LoginProvider, k.Name }));
            builder.Entity<IdentityUserLogin<string>>(options => options.ToTable("Tbl_UserLogin").HasKey(k => new { k.LoginProvider, k.ProviderKey }));
            builder.Entity<RoleModel>(options =>
            {
                options.ToTable("Tbl_Role").HasKey(k => k.Id);
                options.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
                options.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired(true);
            });
            builder.Entity<IdentityRoleClaim<string>>(options => options.ToTable("Tbl_RoleClaim").HasKey(k => k.Id));
            builder.Entity<IdentityUserRole<string>>(options => options.ToTable("Tbl_UserRole").HasKey(k => new { k.UserId, k.RoleId }));
            #endregion

            builder.Entity<Movie>().ToTable("Tbl_Movie").HasKey(k => k.Id);

        }
    }
}
