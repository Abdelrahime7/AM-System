

namespace Application.Affiliates.DTO_s
{
    public class UpdateAffiliateRequest
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        public int? ReferralCode { get; set; }
        public decimal? CommissionRate { get; set; }

    }
}
