using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CommunitySupportPlatform.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }  // Primary key

        public decimal DonationAmount { get; set; }

        public DateTime DonationDate { get; set; }

        // Navigation properties
        [ForeignKey("Donor")]
        public int DonorId { get; set; }  // Foreign key to Donor
        public virtual Donor Donor { get; set; }  // Navigation property to Donor

        [ForeignKey("Campaign")]
        public int CampaignId { get; set; }  // Foreign key to Campaign
        public virtual Campaign Campaign { get; set; }  // Navigation property to Campaign

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }

    public class DonationDto
    {
        public int DonationId { get; set; }
        public decimal DonationAmount { get; set; }
        public DateTime DonationDate { get; set; }

        // Additional properties for navigation purposes
        public int DonorId { get; set; }
        public int CompanyId { get; set; }
        public string DonorName { get; set; }  // Including DonorName for easier display in views
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }  // Including CampaignName for easier display in views
    }

}