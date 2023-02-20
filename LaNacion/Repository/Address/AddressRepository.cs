using Dapper;
using LaNacionChallenge.Domain;
using System.Data;

namespace LaNacionChallenge.Repository
{
    public class AddressRepository : IAddressRepository<Address>
    {
        private readonly IDbConnection _db;
        public AddressRepository(IDbConnection db)
        {
            _db = db;
        }
        public async Task<int> AddAsync(Address adress)
        {
            var query = @"INSERT INTO Addresses (Street, City, State, ZipCode, ContactId, Active)
                          VALUES (@Street, @City, @State, @ZipCode, @ContactId, @Active);
                          SELECT CAST(LAST_INSERT_ROWID() AS INT);";

            return await _db.QuerySingleAsync<int>(query, adress);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @"UPDATE Addresses 
                                SET Active = false
                                WHERE Id = @Id;";

            return await _db.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var query = @"SELECT * FROM Addresses;";
            return await _db.QueryAsync<Address>(query);
        }

        public async Task<IEnumerable<Address>> GetByContactIdAsync(int id)
        {
            var query = @"SELECT * FROM Addresses WHERE ContactId = @Id;";
            return await _db.QueryAsync<Address>(query, new { Id = id });
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            var query = @"SELECT * FROM Addresses WHERE Id = @Id;";
            return await _db.QueryFirstOrDefaultAsync<Address>(query, new { Id = id });
        }

        public async Task<int> UpdateAsync(Address address)
        {
            var query = @"UPDATE Addresses 
                          SET Active = @Active,
                              Street = @Street ,
                              City = @City ,
                              State = @State ,
                              ZipCode = @ZipCode ,
                              ContactId = @ContactId   
                          WHERE Id = @Id;";

            return await _db.ExecuteAsync(query, address);
        }
    }
}
