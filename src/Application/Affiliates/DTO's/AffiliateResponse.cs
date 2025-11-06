

namespace Application.Affiliates.DTO_s
{
    public class AffiliateResponse
    {
        public int UserID { get; set; }
        public int? ReferralCode { get; set; }
        public decimal? CommissionRate { get; set; }
        public DateTime? PartnerSince { get; set; }
    }
}
