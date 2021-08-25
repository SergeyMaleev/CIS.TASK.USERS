using Api.DataContext.Entity;
using Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataContext
{
    public class DbInitializer
    {
        /// <summary>
        /// Создает администратора и создает роли при первом запуске сервера
        /// </summary>       
        public static async Task InitializeAsync(EfDbContext context)
        {          
            if (context.Roles.FirstOrDefault(x => x.Name == "Администратор") == null)
            {
                var role = new Role() { Name = "Администратор" }; 
                await context.Roles.AddAsync(role);
            }
            if (context.Roles.FirstOrDefault(x => x.Name == "Редактор справочников") == null)
            {
                var role = new Role() { Name = "Редактор справочников" };
                await context.Roles.AddAsync(role);
            }
            if (context.Roles.FirstOrDefault(x => x.Name == "Заказчик") == null)
            {
                var role = new Role() { Name = "Заказчик" };
                await context.Roles.AddAsync(role);
            }

            await context.SaveChangesAsync();

            string pass = "admin";
            var user = new User
            {
                Name = "Name",
                Login = "admin",
                Email = "admin@admin.ru",               
            };

            if (context.Users.FirstOrDefault(x => x.Login == "admin") == null)
            {
                
                UserServices.CreatePasswordHash(pass, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                var adminRole = context.Roles.FirstOrDefault(x => x.Name == "Администратор");
                user.Roles.Add(adminRole);

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
