using LibraryManagmentSystem.Data.Model;

namespace LibraryManagmentSystem.Data
{
    public class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();


                // Add Customers
                var mem1 = new Member { MemberName = "Om Ta", Email = "mem1@Livlib.com" };

                var mem2 = new Member { MemberName = "Se AH", Email = "mem2@Livlib.com" };

                var mem3 = new Member { MemberName = "me m3", Email = "mem3@Livlib.com" };

                context.Members.Add(mem1);
                context.Members.Add(mem2);
                context.Members.Add(mem3);

                context.SaveChanges();
            }
        }
    }
}
