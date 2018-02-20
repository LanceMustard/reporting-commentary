using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingCommentary.Models
{

    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "A customer code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "A customer name is required")]
        public string Name { get; set; }
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "A user name is required")]
        public string Name { get; set; }
    }

    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "A contract name is required")]
        public string Name { get; set; }
        public string Number { get; set; }
        public string ReportingCycle { get; set; }
        public int PackageManagerId { get; set; }
        [ForeignKey("PackageManagerId")]
        public User PackageManager { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }

    public class ReportingPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ReportingItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
    }

    public class CBS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
    }

    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "A project name is required")]
        public string Name { get; set; }
        public string Number { get; set; }
        public int ProjectManagerId { get; set; }
        [ForeignKey("ProjectManagerId")]
        public User ProjectManager { get; set; }
        public int PortfolioManagerId { get; set; }
        [ForeignKey("PortfolioManagerId")]
        public User PortfolioManager { get; set; }
        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
    }

    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string Comments { get; set; }
        public DateTime SubmitDate { get; set; }

        public int ProjectManagerId { get; set; }
        [ForeignKey("ProjectManagerId")]
        public User ProjectManager { get; set; }
        public DateTime ProjectManagerApprovalDate { get; set; }

        public int PortfolioManagerId { get; set; }
        [ForeignKey("PortfolioManagerId")]
        public User PortfolioManager { get; set; }
        public DateTime PortfolioManagerApprovalDate { get; set; }

        public int PackageManagerId { get; set; }
        [ForeignKey("PackageManagerId")]
        public User PackageManager { get; set; }
        public DateTime PackageManagerApprovalDate { get; set; }

        public int CBSId { get; set; }
        [ForeignKey("CBSId")]
        public CBS CBS { get; set; }

        public int ReportingPeriodId { get; set; }
        [ForeignKey("ReportingPeriodId")]
        public ReportingPeriod ReportingPeriod { get; set; }

        public int ReportingItemId { get; set; }
        [ForeignKey("ReportingItemId")]
        public ReportingPeriod ReportingItem { get; set; }

        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }
    }
}
