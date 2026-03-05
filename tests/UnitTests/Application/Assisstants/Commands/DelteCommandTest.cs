
using Domain.Entities;
using Moq;


namespace UnitTests.Application.Assisstants.Commands
{
    public partial class CommandTests
    {

       
           
            [Fact]
            public async Task DeleteAssisstantAsync_ReturnsSuccess_WhenAssisstantExists()
            {
                // Arrange
                var Assisstant = new Assisstant { Id = 1 };
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
                _repository.Setup(r => r.Delete(Assisstant));

                // Act
                var result = await _Commands.DeleteAssisstantAsnc(1);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
            }

            [Fact]
            public async Task DeleteAssisstantAsync_ReturnsFailure_WhenAssisstantNotFound()
            {
                // Arrange
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Assisstant)null);

                // Act
                var result = await _Commands.DeleteAssisstantAsnc(1);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Assisstant Not Found", result.Error);
            }

            [Fact]
            public async Task DeleteAssisstantAsync_ReturnsFailure_WhenRepositoryThrows()
            {
                // Arrange
                var Assisstant = new Assisstant { Id = 1 };
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(Assisstant);
                _repository.Setup(r => r.Delete(Assisstant)).Throws(new Exception("DB error"));

                // Act
                var result = await _Commands.DeleteAssisstantAsnc(1);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("Error Deleting Assisstant", result.Error);
            }
        

    }
}
