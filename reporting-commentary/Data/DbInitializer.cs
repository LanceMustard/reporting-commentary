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
            if (context.Contracts.Count() == 0) AddContracts(context);
            if (context.Projects.Count() == 0) AddProjects(context);
            if (context.ReportingPeriods.Count() == 0) AddReportingPeriods(context);
            if (context.ReportingItems.Count() == 0) AddReportingItems(context);
            if (context.CBSs.Count() == 0) AddCBSs(context);
        }

        static private void AddUsers(CommentaryContext context)
        {
            context.Users.Add(new User { Name = "Allan Gamble" });
            context.Users.Add(new User { Name = "David Fisher" });
            context.SaveChanges();
        }

        static private void AddCustomers(CommentaryContext context)
        {
            context.Customers.Add(new Customer { Code = "RIOT", Name = "Rio Tinto" });
            context.Customers.Add(new Customer { Code = "WEL", Name = "Woodside" });
            context.SaveChanges();
        }

        static private void AddContracts(CommentaryContext context)
        {
            context.Contracts.Add(new Contract { Number = "201012-00629", Name = "Koodaideri DES", Customer = context.Customers.FirstOrDefault(x => x.Code == "RIOT"), ContractManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble") });
            context.Contracts.Add(new Contract { Number = "XXX", Name = "WEL Brownfields", Customer = context.Customers.FirstOrDefault(x => x.Code == "WEL"), ContractManager = context.Users.FirstOrDefault(x => x.Name == "David Fisher") });
            context.SaveChanges();
        }

        static private void AddProjects(CommentaryContext context)
        {
            context.Projects.Add(new Project { Number = "A", Name = "General", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629"), PortfolioManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble"), ProjectManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble") });
            context.Projects.Add(new Project { Number = "B", Name = "Plant", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629"), PortfolioManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble"), ProjectManager = context.Users.FirstOrDefault(x => x.Name == "David Fisher") });
            context.Projects.Add(new Project { Number = "C", Name = "NPI", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629"), PortfolioManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble") });
            context.Projects.Add(new Project { Number = "D", Name = "Rail", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629"), PortfolioManager = context.Users.FirstOrDefault(x => x.Name == "Allan Gamble") });
            context.SaveChanges();
        }

        static private void AddReportingPeriods(CommentaryContext context)
        {
            DateTime startDate = new DateTime(2017, 8, 25);
            for (int i = 0; i <= 52; i++)
            {
                context.ReportingPeriods.Add(new ReportingPeriod { StartDate = startDate.AddDays(i * 7), EndDate = startDate.AddDays((i * 7) + 6), Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            }
            context.SaveChanges();
        }

        static private void AddCBSs(CommentaryContext context)
        {
            context.CBSs.Add(new CBS { Number = "CBS10", Value = "10", Description = "Project Management", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS11", Value = "11", Description = "Travel Costs", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS30", Value = "30", Description = "Detailed Design", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS40", Value = "40", Description = "Procurement", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS50", Value = "50", Description = "Fabrication", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS60", Value = "60", Description = "Implementation", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS60-01", Value = "60-01", Description = "Implementation - EPCM Implementation", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS60-02", Value = "60-02", Description = "Implementation - ThirdParty Implementation Labour", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS60-03", Value = "60-03", Description = "Implementation - ThirdParty Implementation (Plant & Equipment)", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS70", Value = "70", Description = "Commissioning", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS80", Value = "80", Description = "Closeout", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS81", Value = "81", Description = "Contractor One Off Costs", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS82", Value = "82", Description = "Contractor Allowance", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS83", Value = "83", Description = "Contractor Contingency", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS84", Value = "84", Description = "Contractor Escalation", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBSALL", Value = "ALL", Description = "All CBS for Commentary Purposes Only", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS83a", Value = "83a", Description = "Contingency", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS83b", Value = "83b", Description = "Contingency", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.CBSs.Add(new CBS { Number = "CBS83c", Value = "83c", Description = "Contingency", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });

            context.SaveChanges();
        }

        static private void AddReportingItems(CommentaryContext context)
        {
            context.ReportingItems.Add(new ReportingItem { Number = "0.01", Description = "Overall Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.02", Description = "Overall Summary - Safety", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.03", Description = "Overall Summary - Schedule", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.04", Description = "Overall Summary - Cost", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.05", Description = "Overall Variance - Finance", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.06", Description = "Committed", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.07", Description = "Incurred", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.08", Description = "Cash", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.09", Description = "Key Upcoming Milestones", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.10", Description = "Key Issues & Concerns", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.11", Description = "Achievements This Week", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.12", Description = "Planned Activities Next Week", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "0.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.01", Description = "Hours Worked vs TRIFR", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.02", Description = "Hours Worked vs Total HS Incidents", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.03", Description = "Current Period TRIFR", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.04", Description = "Current Period Exposure Hours", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.05", Description = "Last Period Incident Count", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.06", Description = "Last Period Incident Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.07", Description = "Incident Summary for Last 12 months", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "1.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.01", Description = "Hours Worked vs Env TRIFR", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.02", Description = "Hours Worked vs Total Env Incidents", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.03", Description = "Current Period Env TRIFR", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.04", Description = "Current Period Exposure Hours", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.05", Description = "Last Period Env Incident Count", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.06", Description = "Last Period Env Incident Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.07", Description = "Env Incident Summary for Last 12 months", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "2.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.01", Description = "Cumulative Cost Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.02", Description = "Cost Summary by WBS", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.03", Description = "Period Cost Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.11", Description = "Cumulative Hour Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.12", Description = "Hour Summary by WBS", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.13", Description = "Period Hour Summary", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.14", Description = "Cumulative & Period MPI", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.15", Description = "Cumulative & Period SPI", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "3.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "4.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "5.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.01", Description = "TIC Value by Area & Name", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.02", Description = "Contract Value Sumamry Table", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.03", Description = "TIC Value by Contract Type", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.04", Description = "TIC Value by WP Scope", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.05", Description = "Estimated Lead Times", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.06", Description = "Estimated Package Value", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.07", Description = "Cumulative Value", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "6.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.01", Description = "TIC Comparison to 'Baseline' estimate", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.02", Description = "Key Quantitiy Comparison to 'Baseline' estimate", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "7.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "8.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.11", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.12", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.13", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.14", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.15", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.16", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.17", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.18", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.19", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "9.20", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "10.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "11.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "12.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "13.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "14.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "15.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "16.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "17.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "18.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "19.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.01", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.02", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.03", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.04", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.05", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.06", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.07", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.08", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.09", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });
            context.ReportingItems.Add(new ReportingItem { Number = "20.10", Description = "", Contract = context.Contracts.FirstOrDefault(x => x.Number == "201012-00629") });

            context.SaveChanges();
        }
    }
}
