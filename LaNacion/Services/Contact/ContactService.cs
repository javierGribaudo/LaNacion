using FluentValidation;
using FluentValidation.Results;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Repository;
using System.Data;
using System.Linq;

namespace LaNacionChallenge.Services
{
    public class ContactService : IContactService<Contact>
    {
        private readonly IContactRepository<Contact> _contactRepository;
        private readonly IAddressRepository<Address> _addressRepository;
        private readonly IPhoneRepository<PhoneNumber> _phoneRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Contact> _validator;
        private readonly IValidator<PhoneNumber> _validatorPhone;
        public ContactService(IContactRepository<Contact> contactRepo,
            IDbConnection dbConnection,
            IUnitOfWork unitOfWork,
            IAddressRepository<Address> addressRepository,
            IPhoneRepository<PhoneNumber> phoneRepository,
            IValidator<Contact> validator,
            IValidator<PhoneNumber> validatorPhone)
        {
            _contactRepository = contactRepo;
            _unitOfWork = unitOfWork;
            _addressRepository = addressRepository;
            _phoneRepository = phoneRepository;
            _validator = validator;
            _validatorPhone = validatorPhone;
        }
        public async Task<bool> AddOrUpdateAsync(Contact contact)
        {
            ValidateContact(contact);

            if (contact.Id == 0)
            {
                contact.Active = true;
                var contactId = await _contactRepository.AddAsync(contact);


                contact.Address.ContactId = contactId;
                await _addressRepository.AddAsync(contact.Address);

                contact.PhoneNumbers.ToList().ForEach(async phone =>
                {
                    phone.ContactId = contactId;
                    await _phoneRepository.AddAsync(phone);
                });
            }
            else
            {
                await _contactRepository.UpdateAsync(contact);
            }

            //part of unit work save
            await _unitOfWork.SaveChangesAsync();
            return _unitOfWork.IsCommitted();
        }

        private void ValidateContact(Contact contact)
        {
            var validationResult = _validator.Validate(contact);

            TrhowValidationExeption(validationResult);
        }

        private static void TrhowValidationExeption(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new Exception(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage).ToArray()));
            }
        }

        private void ValidatePhone(PhoneNumber phone)
        {
            var validationResult = _validatorPhone.Validate(phone);

            TrhowValidationExeption(validationResult);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return _unitOfWork.IsCommitted();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();

            List<Task> tasks = GetPhoneNumbersAndAddress(contacts);

            await Task.WhenAll(tasks);

            return contacts;
        }

        public async Task<IEnumerable<Contact>> GetByCityOrStateAsync(string state, string city)
        {
            var contacts = await _contactRepository.GetByCityOrStateAsync(state, city);

            List<Task> tasks = GetPhoneNumbersAndAddress(contacts);

            await Task.WhenAll(tasks);

            return contacts;
        }

        private List<Task> GetPhoneNumbersAndAddress(IEnumerable<Contact> contacts)
        {
            return contacts.Select(async contact =>
            {
                contact.PhoneNumbers = (ICollection<PhoneNumber>?)await _phoneRepository.GetByContactIdAsync(contact.Id);
                contact.Address = (await _addressRepository.GetByContactIdAsync(contact.Id)).FirstOrDefault();
            }).ToList();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            contact.PhoneNumbers = (ICollection<PhoneNumber>?)await _phoneRepository.GetByContactIdAsync(contact.Id);
            contact.Address = (await _addressRepository.GetByContactIdAsync(contact.Id)).FirstOrDefault();

            return contact;
        }


        public async Task<IEnumerable<Contact>> GetByEmailAsync(string email)
        {
            Contact cont = new Contact() { Email = email };
            ValidateContact(cont);

            var contacts = await _contactRepository.GetByEmailAsync(email);

            List<Task> tasks = GetPhoneNumbersAndAddress(contacts);

            await Task.WhenAll(tasks);

            return contacts;
        }

        public async Task<IEnumerable<Contact>> GetByPhoneAsync(string phone)
        {
            ValidatePhone(new PhoneNumber() { Number = phone });

            var contacts = await _contactRepository.GetByPhoneAsync(phone);

            List<Task> tasks = GetPhoneNumbersAndAddress(contacts);

            await Task.WhenAll(tasks);

            return contacts;
        }
    }
}
