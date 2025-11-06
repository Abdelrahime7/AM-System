using Application.Affiliates.DTO_s;
using Application.Interfaces.Common.Mappers;
using Domain.Entities;

namespace Application.Affiliates.Mapper
{
    internal class AffiliateMapper : IEntityMapper<Affiliate, CreateAffiliateRequest, UpdateAffiliateRequest,
        AffiliateResponse>
    {
        public Affiliate ToEntity(CreateAffiliateRequest dto)
        {
            return new Affiliate
            {

                ReferralCode = dto.ReferralCode,
                CommissionRate = dto.CommissionRate,
                PartnerSince = DateTime.UtcNow

            };
        }

        public AffiliateResponse ToResponse(Affiliate entity)
        {
            return new AffiliateResponse
            {
                UserID = entity.UserID,
                ReferralCode = entity.ReferralCode,
                CommissionRate = entity.CommissionRate,
                PartnerSince = entity.PartnerSince
            };
        }

        public void ToUpdateEntity(Affiliate entity, UpdateAffiliateRequest dto)
        {
          entity.UserID   = dto.UserID ??  entity.UserID;
            entity.ReferralCode = dto.ReferralCode ?? entity.ReferralCode;
            entity.CommissionRate = dto.CommissionRate ?? entity.CommissionRate;
          //  entity.PartnerSince = dto.PartnerSince ?? entity.PartnerSince;



        }
    }
}
