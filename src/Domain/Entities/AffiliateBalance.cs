namespace Domain.Entities;

public class AffiliateBalance
{
    public int Id { get; set; }
    public decimal Amount { get; set; }

    public int AffiliateId { get; set; }
    public User Affiliate { get; set; } = null!;
    
    public ICollection<Withdrawal> Withdrawals { get; set; } = new List<Withdrawal>();
}