using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Affiliates.DTO_s
{
    public class CreateAffiliateRequest
    {
         
        public int id { get; set; }
        public int UserID { get; set; }
        public int? ReferralCode { get; set; }
        public decimal? CommissionRate { get; set; }
        public DateTime? PartnerSince { get; set; }
      

    }
}
