using LaNacionChallenge.Domain;
using LaNacionChallenge.Repository;
using LaNacionChallenge.Services;
using Moq;
using System.Numerics;

namespace ChallengueTestProyect
{
    [TestFixture]
    public class AddressTests
    {
        private Mock<IAddressRepository<Address>> _addressRepositoryMock;
        private AddressService _addressService;

        [SetUp]
        public void Setup()
        {
            _addressRepositoryMock = new Mock<IAddressRepository<Address>>();
            _addressService = new AddressService(_addressRepositoryMock.Object);
        }

        [Test]
        public async Task AddOrUpdateAsync_WhenAddressIdIsZero_CallsAddAsyncOnRepository()
        {
            // Arrange
            var address = new Address { Id = 0, Street = "123 Main St", City = "Anytown", State = "Any State" };
            _addressRepositoryMock.Setup(x => x.AddAsync(address)).ReturnsAsync(1);

            // Act
            var result = await _addressService.AddOrUpdateAsync(address);

            // Assert
            _addressRepositoryMock.Verify(x => x.AddAsync(address), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AddOrUpdateAsync_WhenAddressIdIsNotZero_CallsUpdateAsyncOnRepository()
        {
            // Arrange
            var address = new Address { Id = 1, Street = "123 Main St", City = "Anytown", State = "Any State" };
            _addressRepositoryMock.Setup(x => x.UpdateAsync(address)).ReturnsAsync(1);

            // Act
            var result = await _addressService.AddOrUpdateAsync(address);

            // Assert
            _addressRepositoryMock.Verify(x => x.UpdateAsync(address), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteAsync_WhenAddressIdIsValid_CallsDeleteAsyncOnRepository()
        {
            // Arrange
            var id = 1;
            _addressRepositoryMock.Setup(x => x.DeleteAsync(id)).ReturnsAsync(1);

            // Act
            var result = await _addressService.DeleteAsync(id);

            // Assert
            _addressRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllAsync_CallsGetAllAsyncOnRepository()
        {
            // Arrange
            var addresses = new List<Address>
        {
            new Address { Id = 1, Street = "123 Main St", City = "Anytown", State = "Any State" },
            new Address { Id = 2, Street = "456 First St", City = "Anytown", State = "Any State" },
            new Address { Id = 3, Street = "789 Second St", City = "Anytown", State = "Any State" }
        };
            _addressRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(addresses);

            // Act
            var result = await _addressService.GetAllAsync();

            // Assert
            _addressRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.AreEqual(addresses, result);
        }

        [Test]
        public async Task GetByIdAsync_WhenAddressIdIsValid_CallsGetByIdAsyncOnRepository()
        {
            // Arrange
            var id = 1;
            var address = new Address { Id = id, Street = "123 Main St", City = "Anytown", State = "Any State" };
            _addressRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(address);

            // Act
            var result = await _addressService.GetByIdAsync(id);

            // Assert
            _addressRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.AreEqual(address, result);
        }
    }
}