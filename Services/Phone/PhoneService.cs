using FluentValidation;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Repository;
using System.Numerics;

namespace LaNacionChallenge.Services
{
    public class PhoneService : IPhoneService<PhoneNumber>
    {
        private readonly IPhoneRepository<PhoneNumber> _phoneRepository;
        private readonly IValidator<PhoneNumber> _validatorPhone;
        public PhoneService(IPhoneRepository<PhoneNumber> phoneRepository,
            IValidator<PhoneNumber> validator)
        {
            _phoneRepository = phoneRepository;
            _validatorPhone = validator;
        }

        public async Task<bool> AddOrUpdateAsync(PhoneNumber phoneNumber)
        {
            var validationResult = _validatorPhone.Validate(phoneNumber);
            if (!validationResult.IsValid)
            {
                throw new Exception(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));
            }

            if (phoneNumber.Id == 0)
                return await _phoneRepository.AddAsync(phoneNumber) > 0;

            return await _phoneRepository.UpdateAsync(phoneNumber) > 0;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _phoneRepository.DeleteAsync(id) > 0;
        }

        public async Task<IEnumerable<PhoneNumber>> GetAllAsync()
        {
            return await _phoneRepository.GetAllAsync();
        }

        public async Task<IEnumerable<PhoneNumber>> GetByContactIdAsync(int id)
        {
            return await _phoneRepository.GetByContactIdAsync(id);
        }

        public async Task<PhoneNumber> GetByIdAsync(int id)
        {
            return await _phoneRepository.GetByIdAsync(id);
        }
    }
}
