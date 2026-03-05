
namespace Domain.Entities
{
    public class Affiliate
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int ?  ReferralCode { get; set; }
        public decimal ? CommissionRate  { get; set; }
        public DateTime? PartnerSince { get; set; }
        public User user { get; set; }



    }
}
