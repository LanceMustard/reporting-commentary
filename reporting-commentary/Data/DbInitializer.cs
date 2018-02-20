using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportingCommentary.Models;

namespace ReportingCommentary.Data
{
    public class DbInitializer
    {
        static private string[] orders = new string[] { "Valves", "Pumps", "Motors", "Pipe Fabrication", "Bolts", "Gaskets", "Structural Steel", "HDPE", "Junction Boxs", "HVAC", "Gas Turbine", "Heat Exchanger" };

        public static void Initialize(CommentaryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            if (context.Users.Count() == 0) AddUsers(context);
            if (context.Customers.Count() == 0) AddCustomers(context);
            if (context.Packages.Count() == 0) AddPackages(context);
        }


        static private void AddCustomers(CommentaryContext context)
        {
            context.Customers.Add(new Customer { Code = "RIOT", Name = "Rio Tinto" });
            context.Customers.Add(new Customer { Code = "WEL", Name = "Woodside" });
            context.SaveChanges();
        }

        static private void AddUsers(CommentaryContext context)
        {
            context.Users.Add(new User { Name = "Allan Gamble" });
            context.Users.Add(new User { Name = "David Fisher" });
            context.SaveChanges();
        }

        static private void AddPackages(CommentaryContext context)
        {
            context.Packages.Add(new Package { Number = "A", Name = "General", Customer = context.Customers.FirstOrDefault(x => x.Code == "RIOT"), PackageManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble") });
            context.Packages.Add(new Package { Number = "B", Name = "Plant", Customer = context.Customers.FirstOrDefault(x => x.Code == "RIOT"), PackageManager = context.Users.FirstOrDefault(x => x.Name == "David Fisher") });
            context.Packages.Add(new Package { Number = "C", Name = "NPI", Customer = context.Customers.FirstOrDefault(x => x.Code == "RIOT") });
            context.Packages.Add(new Package { Number = "D", Name = "Rail", Customer = context.Customers.FirstOrDefault(x => x.Code == "RIOT") });
            context.SaveChanges();
        }
    }
}
