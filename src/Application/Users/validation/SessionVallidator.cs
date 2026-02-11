using Application.Admins.Dto_s;
using Application.Affiliates.DTO_s;
using Application.Assisstants.Dto_s;
using Application.Drivers.DTO_s;
using Application.RoleRequeste;
using Application.Users.DTOs;
using FluentValidation;
using System;


namespace Application.Users.validation
{
    public class CreateRoleSessionValidator : AbstractValidator<CreateRoleSession>
    {
        public CreateRoleSessionValidator(
            IValidator<CreateUserRequest> userRequestValidator,
            IValidator<CreateAffiliateRequest > affiliateValidator,
            IValidator<CreateDriverRequest> driverValidator,
          IValidator<CreateAdminRequest> adminValidator,
          IValidator<CreatAssisstantRequest> assisstantValidator


        )
        {
            // validate common user info
            RuleFor(x => x.UserRequest )
                .SetValidator(userRequestValidator);


            RuleFor(x => (CreateAffiliateRequest)x.RoleRequest).
                SetValidator(affiliateValidator).When(x => x.RoleRequest is CreateAffiliateRequest);

            RuleFor(x => (CreateDriverRequest)x.RoleRequest).
                SetValidator(driverValidator).When(x => x.RoleRequest is CreateDriverRequest); 

            RuleFor(x => (CreateAdminRequest)x.RoleRequest).
                SetValidator(adminValidator).When(x => x.RoleRequest is CreateAdminRequest);

            RuleFor(x => (CreatAssisstantRequest)x.RoleRequest).
                SetValidator(assisstantValidator).When(x => x.RoleRequest is CreatAssisstantRequest);


        }
    }


}
