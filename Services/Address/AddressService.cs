using LaNacionChallenge.Domain;
using LaNacionChallenge.Repository;

namespace LaNacionChallenge.Services
{
    public class AddressService : IAddressService<Address>
    {
        private readonly IAddressRepository<Address> _addressRepository;
        public AddressService(IAddressRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> AddOrUpdateAsync(Address address)
        {
            if (address.Id == 0)
                return await _addressRepository.AddAsync(address) > 0;

            return await _addressRepository.UpdateAsync(address) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _addressRepository.DeleteAsync(id) > 0;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Address>> GetByContactIdAsync(int id)
        {
            return await _addressRepository.GetByContactIdAsync(id);
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }
    }
}
