using Microsoft.EntityFrameworkCore;

namespace Events.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EventsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EventsContext>>()))
            {
                // Look for any movies.
                if (context.Event.Any())
                {
                    return;   // DB has been seeded
                }
                var user = new User
                {
                    GlobalId = "d6abf410-322c-4aef-9efc-8a845b6d7985",
                    Name = "jacek",
                };

                context.User.AddRange(
                    user
                );

                context.Event.AddRange(
                    new Event
                    {
                        Name = "First event",
                        Description = "First event desc",
                        Location = "Berlin",
                        Owner = user,
                        EndTime = new DateTime(2022, 1, 1, 12, 0, 0),
                        StartTime = new DateTime(2022, 1, 2, 12, 0, 0)
                    },
                    new Event
                    {
                        Name = "Second event",
                        Description = "Second event desc",
                        Location = "Munich",
                        Owner = user,
                        EndTime = new DateTime(2022, 2, 1, 12, 0, 0),
                        StartTime = new DateTime(2022, 2, 2, 12, 0, 0)
                    }

                );
                context.SaveChanges();
            }
        }
    }
}