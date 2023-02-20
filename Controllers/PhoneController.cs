using AutoMapper;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaNacionChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController
    {
        private readonly IPhoneService<PhoneNumber> _phoneService;
        public PhoneController(IPhoneService<PhoneNumber> phoneService)
        {
            _phoneService = phoneService;
        }// GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<PhoneNumber>> GetAll()
        {
            return await _phoneService.GetAllAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<PhoneNumber> Get(int id)
        {
            return await _phoneService.GetByIdAsync(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<JsonResult> CreatePhoneNumber(PhoneNumber PhoneNumber)
        {

            PhoneNumber.Id = 0;
            if (await _phoneService.AddOrUpdateAsync(PhoneNumber))
                return new JsonResult("PhoneNumber created Successfully");

            return new JsonResult("Could not create PhoneNumber");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, PhoneNumber phoneNumber)
        {
            phoneNumber.Id = id;
            if (await _phoneService.AddOrUpdateAsync(phoneNumber))
                return new JsonResult("PhoneNumber updated Successfully");

            return new JsonResult("Could not update PhoneNumber");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            if (await _phoneService.DeleteAsync(id))
                return new JsonResult("PhoneNumber deleted Successfully");

            return new JsonResult("Could not delete PhoneNumber");
        }
    }
}
