using LaNacionChallenge.Domain;
using LaNacionChallenge.Infrastructure.Validators;
using LaNacionChallenge.Repository;
using LaNacionChallenge.Services;
using Moq;
using System.Numerics;

namespace ChallengueTestProyect
{
    [TestFixture]
    public class PhoneServiceTests
    {
        private Mock<IPhoneRepository<PhoneNumber>> _phoneRepositoryMock;
        private PhoneService _phoneService;
        private PhoneValidator _phoneValidator;

        [SetUp]
        public void Setup()
        {
            _phoneRepositoryMock = new Mock<IPhoneRepository<PhoneNumber>>();
            _phoneValidator = new PhoneValidator();
            _phoneService = new PhoneService(_phoneRepositoryMock.Object, _phoneValidator);

        }
        [Test]
        public async Task AddOrUpdateAsync_AddsNewPhone_ReturnsTrue()
        {
            // Arrange
            var phoneNumber = new PhoneNumber
            {
                Number = "1234567890",
                ContactId = 1
            };
            _phoneRepositoryMock.Setup(x => x.AddAsync(It.IsAny<PhoneNumber>())).ReturnsAsync(1);

            // Act
            var result = await _phoneService.AddOrUpdateAsync(phoneNumber);

            // Assert
            _phoneRepositoryMock.Verify(x => x.AddAsync(phoneNumber), Times.Once);
            Assert.IsTrue(result);
        }
        [Test]
        public async Task DeleteAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            var phoneId = 1;
            _phoneRepositoryMock.Setup(x => x.DeleteAsync(phoneId)).ReturnsAsync(1);

            // Act
            var result = await _phoneService.DeleteAsync(phoneId);

            // Assert
            Assert.IsTrue(result);
            _phoneRepositoryMock.Verify(x => x.DeleteAsync(phoneId), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_InvalidId_ReturnsFalse()
        {
            // Arrange
            var phoneId = 1;

            // Act
            var result = await _phoneService.DeleteAsync(phoneId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}