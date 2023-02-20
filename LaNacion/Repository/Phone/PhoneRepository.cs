using Dapper;
using LaNacionChallenge.Domain;
using System.Data;
using System.Numerics;

namespace LaNacionChallenge.Repository
{
    public class PhoneRepository : IPhoneRepository<PhoneNumber>
    {
        private readonly IDbConnection _db;
        public PhoneRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(PhoneNumber phone)
        {
            var query = @"INSERT INTO PhoneNumbers (Type,Number,ContactId,Active) 
                                      VALUES (@Type,@Number,@ContactId,@Active);
                                      SELECT CAST(LAST_INSERT_ROWID() AS INT);";

            return await _db.QuerySingleAsync<int>(query, phone);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @"UPDATE PhoneNumbers
                            SET Active = false
                          WHERE Id = @Id;";

            return await _db.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<PhoneNumber>> GetAllAsync()
        {
            var query = @"SELECT * FROM PhoneNumbers";
            return await _db.QueryAsync<PhoneNumber>(query);
        }

        public async Task<IEnumerable<PhoneNumber>> GetByContactIdAsync(int id)
        {
            var query = @"SELECT * FROM PhoneNumbers WHERE ContactId = @Id";
            return await _db.QueryAsync<PhoneNumber>(query, new { Id = id });
        }

        public async Task<PhoneNumber> GetByIdAsync(int id)
        {
            var query = @"SELECT * FROM PhoneNumbers WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<PhoneNumber>(query, new {Id = id });
        }

        public async Task<int> UpdateAsync(PhoneNumber phoneNumber)
        {
            var query = @"UPDATE PhoneNumbers
                            SET Active = @Active,
                                Type = @Type,
                                Number = @Number,
                                ContactId = @ContactId
                          WHERE Id = @Id;";

            return await _db.ExecuteAsync(query, phoneNumber);
        }
    }
}
