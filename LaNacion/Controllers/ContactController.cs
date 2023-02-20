using AutoMapper;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaNacionChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService<Contact> _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService<Contact> contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactService.GetAllAsync();
        }

        [HttpGet("GetByState/{state}")]
        public async Task<IEnumerable<Contact>> GetByState(string state)
        {
            return await _contactService.GetByCityOrStateAsync(state, "");
        }
        [HttpGet("GetByCity/{city}")]
        public async Task<IEnumerable<Contact>> GetByCity(string city)
        {
            return await _contactService.GetByCityOrStateAsync("", city);
        }
        [HttpGet("GetByEmail/{email}")]
        public async Task<IEnumerable<Contact>> GetByEmail(string email)
        {
            return await _contactService.GetByEmailAsync(email);
        }
        [HttpGet("GetByPhone/{phone}")]
        public async Task<IEnumerable<Contact>> GetByPhone(string phone)
        {
            return await _contactService.GetByPhoneAsync(phone);
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            return await _contactService.GetByIdAsync(id);
        }
        // POST api/<ValuesController>
        [HttpPost]
        public async Task<JsonResult> CreateContact(Contact contact)
        {
            contact.Id = 0;
            if (await _contactService.AddOrUpdateAsync(contact))
                return new JsonResult("Contact created Successfully");

            return new JsonResult("Could not create contact");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, ContactRequestModel contact)
        {
            var mapedContact = _mapper.Map<Contact>(contact);

            mapedContact.Id = id;

            if (await _contactService.AddOrUpdateAsync(mapedContact))
                return new JsonResult("Contact updated Successfully");

            return new JsonResult("Could not update contact");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            if (await _contactService.DeleteAsync(id))
                return new JsonResult("Contact deleted Successfully");

            return new JsonResult("Could not delete contact");
        }
    }
}
