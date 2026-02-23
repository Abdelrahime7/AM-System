

using Application.Admins.Dto_s;
using Application.Admins.DTO_s.session;
using Application.Admins.Features.Queries;
using Application.Common.Models;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Admins.Queries
{
    public partial  class QueriesTest
    {
        private readonly Mock<IAdminRepository> _repository;
        private readonly Mock<IEntityMapper<Admin, CreateAdminRequest, UpdateAdminRequest, AdminResponse>> _mapper;
        private readonly Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>> _userMapper;
        private readonly Mock<AdminQueries> _queriesMock;

        public QueriesTest()
        {
            _repository = new Mock<IAdminRepository>();
            _mapper = new Mock<IEntityMapper<Admin, CreateAdminRequest, UpdateAdminRequest, AdminResponse>>();
            _userMapper = new Mock<IEntityMapper<User, CreateUserRequest, UpdateUserRequest, UserResponse>>();

            // Partial mock to override GetById
            _queriesMock = new Mock<AdminQueries>(_repository.Object, _mapper.Object, _userMapper.Object) { CallBase = true };
        }

    //    [Fact]
    //    public async Task GetAllAdmins_ReturnsSuccess_WhenAdminsExist()
    //    {
    //        // Arrange
    //        var admins = new List<Admin>
    //{
    //    new Admin { Id = 1, user = new User { Id = 10,
    //                    Role=UserRole.Admin,

    //     FullName="john doe",
    //     PasswordHash ="ae342rfew",
    //     Phone="+213755443344",
    //     Username="abd33reww"}
    //      },
    //    new Admin { Id = 2, user = new User { Id = 20,
    //                    Role=UserRole.Admin,

    //     FullName="john smith",
    //     PasswordHash ="ae34sswrfew",
    //     Phone="+213755433355",
    //     Username="abd33rwr4ww"} }
    //};

    //        _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(admins);
    //        _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admins[0]);
    //        _repository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(admins[1]);

    //        _mapper.Setup(m => m.ToResponse(admins[0])).Returns(new AdminResponse {  });
    //        _mapper.Setup(m => m.ToResponse(admins[1])).Returns(new AdminResponse {  });

    //        _userMapper.Setup(m => m.ToResponse(admins[0].user)).Returns(new UserResponse { Id = 10 });
    //        _userMapper.Setup(m => m.ToResponse(admins[1].user)).Returns(new UserResponse { Id = 20 });

    //        // Act
    //        var result = await _queriesMock.Object.GetAllAdmins();

    //        // Assert
    //        Assert.True(result.IsSuccess);
    //        Assert.Equal(2, result.Value.Count());
           
    //    }


        [Fact]
        public async Task GetAllAdmins_ReturnsFailure_WhenNoAdminsExist()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Admin>());

            // Act
            var result = await _queriesMock.Object.GetAllAdmins();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Admins Found", result.Error);
        }

        [Fact]
        public async Task GetAllAdmins_ReturnsFailure_WhenRepositoryThrows()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queriesMock.Object.GetAllAdmins();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failled to fetch Admins", result.Error);
        }

    }

}
