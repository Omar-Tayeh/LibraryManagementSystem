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

                var bok1 = new Book { Title = "bok1", Description = "desc1", Author = "mem1", Inventory= 3 };

                var bok2 = new Book { Title = "bok2", Description = "desc2", Author = "mem2" };

                var bok3 = new Book { Title = "bok3", Description = "desc3", Author = "mem3" };

                /*context.Members.Add(mem1);
                context.Members.Add(mem2);
                context.Members.Add(mem3);

                context.Books.Add(bok1);
                context.Books.Add(bok2);
                context.Books.Add(bok3);

                context.SaveChanges();*/
            }
        }
    }
}
