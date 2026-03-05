

using Application.Affiliates.DTO_s;
using Application.Affiliates.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Affiliates.Queries
{
    public partial  class QueriesTest
    {
        private readonly Mock<IAffiliateRepository> _repository;
        private readonly Mock<IEntityMapper<Affiliate, CreateAffiliateRequest, UpdateAffiliateRequest, AffiliateResponse>> _mapper;
        private readonly Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>> _userMapper;
        private readonly Mock<AffiliateQueries> _queriesMock;

        public QueriesTest()
        {
            _repository = new Mock<IAffiliateRepository>();
            _mapper = new Mock<IEntityMapper<Affiliate, CreateAffiliateRequest, UpdateAffiliateRequest, AffiliateResponse>>();
            _userMapper = new Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>>();

            // Partial mock to override GetById
            _queriesMock = new Mock<AffiliateQueries>(_repository.Object, _mapper.Object, _userMapper.Object) { CallBase = true };
        }

        [Fact]
        public async Task GetAllAffiliates_ReturnsSuccess_WhenAffiliatesExist()
        {
            // Arrange
            var Affiliates = new List<Affiliate>
    {
        new Affiliate { Id = 1, user = new User { Id = 10,
                        Role=UserRole.Affiliate,

         FullName="john doe",
         PasswordHash ="ae342rfew",
         Phone="+213755443344",
         Username="abd33reww"}
          },
        new Affiliate { Id = 2, user = new User { Id = 20,
                                Role=UserRole.Affiliate,

         FullName="john smith",
         PasswordHash ="ae34sswrfew",
         Phone="+213755433355",
         Username="abd33rwr4ww"} }
    };

            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(Affiliates);
            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Affiliates[0]);
            _repository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(Affiliates[1]);

            _mapper.Setup(m => m.ToResponse(Affiliates[0])).Returns(new AffiliateResponse {  });
            _mapper.Setup(m => m.ToResponse(Affiliates[1])).Returns(new AffiliateResponse {  });

            _userMapper.Setup(m => m.ToResponse(Affiliates[0].user)).Returns(new UserResponse { });
            _userMapper.Setup(m => m.ToResponse(Affiliates[1].user)).Returns(new UserResponse { });

            // Act
            var result = await _queriesMock.Object.GetAllAffiliates();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
           
        }


        [Fact]
        public async Task GetAllAffiliates_ReturnsFailure_WhenNoAffiliatesExist()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Affiliate>());

            // Act
            var result = await _queriesMock.Object.GetAllAffiliates();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Affiliates Found", result.Error);
        }

        [Fact]
        public async Task GetAllAffiliates_ReturnsFailure_WhenRepositoryThrows()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queriesMock.Object.GetAllAffiliates();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failled to fetch Affiliates", result.Error);
        }

    }

}
