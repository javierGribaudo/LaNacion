using AutoMapper;
using LaNacionChallenge.Domain;
using LaNacionChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaNacionChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController
    {
        private readonly IAddressService<Address> _Addresservice;
        public AddressController(IAddressService<Address> Addresservice)
        {
            _Addresservice = Addresservice;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _Addresservice.GetAllAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Address> Get(int id)
        {
            return await _Addresservice.GetByIdAsync(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<JsonResult> CreateAddress(Address address)
        {
            address.Id = 0;
            if (await _Addresservice.AddOrUpdateAsync(address))
                return new JsonResult("Address created Successfully");

            return new JsonResult("Could not create Address");
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<JsonResult> Put(int id, Address address)
        {
            address.Id = id;
            if (await _Addresservice.AddOrUpdateAsync(address))
                return new JsonResult("Address updated Successfully");

            return new JsonResult("Could not update Address");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            if(await _Addresservice.DeleteAsync(id))
                return new JsonResult("Address deleted Successfully");

            return new JsonResult("Could not delete Address");
        }

    }
}
