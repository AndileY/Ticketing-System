using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TicketSystemWebApi.Data;

namespace TicketSystemWebApi.Data
{
    public static class UserSeeder
    {
        public static async Task SeedDefaultUsersAsync(UserManager<User> userManager)
        {
            var defaultUsers = new List<(string Email, string FirstName, string LastName, int GroupId, string Password, string Role)>
        {
            ("admin@gmail.com", "System", "Admin", 1, "Admin@123!", "Admin"),
            ("assignee@gmail.com", "System", "Assignee", 2, "Assign@123!", "Assignee"),
            ("support@gmail.com", "System", "Support", 3, "Support@123!", "FirstLineSupport")
        };

            foreach (var (email, firstName, lastName, groupId, password, role) in defaultUsers)
            {
                var existingUser = await userManager.FindByEmailAsync(email);
                if (existingUser == null)
                {
                    var user = new User
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        UserGroupId = groupId,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        // ✅ Assign actual Identity role — this adds a record to AspNetUserRoles
                        await userManager.AddToRoleAsync(user, role);

                        

                        Console.WriteLine($"✅ Created user: {email} with role {role}");
                    }

               
                }
            }
        }
    }
}





