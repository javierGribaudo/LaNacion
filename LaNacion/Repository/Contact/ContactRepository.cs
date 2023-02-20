using Dapper;
using LaNacionChallenge.Context;
using LaNacionChallenge.Domain;
using System.Data;
using System.Numerics;

namespace LaNacionChallenge.Repository
{
    public class ContactRepository : IContactRepository<Contact>
    {
        private readonly IDbConnection _db;
        public ContactRepository(IDbConnection db)
        {
            _db = db;
        }
        public async Task<int> AddAsync(Contact contact)
        {
            var query = @"INSERT INTO Contacts (Name, Company, ProfileImage, Email, BirthDate,Active)
                          VALUES (@Name, @Company, @ProfileImage, @Email, @BirthDate, @Active);
                          SELECT CAST(LAST_INSERT_ROWID() AS INT);";

            return await _db.QueryFirstAsync<int>(query, contact);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @"UPDATE Contacts SET Active = 0 WHERE Id = @Id";

            return await _db.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var query = @"Select * from Contacts";
            return await _db.QueryAsync<Contact>(query);
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var contact = await _db.QueryFirstOrDefaultAsync<Contact>(
                @"SELECT * FROM Contacts WHERE Id = @Id;",
                  new { Id = id }
            );
            return contact;

        }

        public async Task<IEnumerable<Contact>> GetByCityOrStateAsync(string state, string city)
        {
            var query = @$"SELECT c.* from Contacts c 
                           INNER JOIN Addresses a 
                                    on c.Id = a.ContactId
                           INNER JOIN PhoneNumbers p
                                    on c.Id = p.ContactId
                           WHERE upper(State) = '{state.ToUpper()}' or upper(City) like '{city.ToUpper()}';";

            return await _db.QueryAsync<Contact>(query);
        }

        public async Task<IEnumerable<Contact>> SearchAsync(string email, string phone)
        {
            var contacts = await _db.QueryAsync<Contact>(
                @"SELECT * FROM Contacts 
                            cont inner join PhoneNumber phone 
                            cont.Email = @email
                            WHERE Id = @Id;",
                  new { email = phone }
            );
            return contacts;
        }


        public async Task<int> UpdateAsync(Contact contact)
        {
            var affected = await _db.ExecuteAsync(
                @"UPDATE Contacts 
                  SET Name = @Name, 
                      Company = @Company, 
                      ProfileImage = @ProfileImage, 
                      Email = @Email, 
                      BirthDate=@BirthDate
                  WHERE Id = @Id",
                contact
            );
            return affected;
        }

        public async Task<IEnumerable<Contact>> GetByEmailAsync(string email)
        {
            var query = @$"SELECT * from Contacts 
                           WHERE upper(Email) = '{email.ToUpper()}';";

            return await _db.QueryAsync<Contact>(query);
        }

        public async Task<IEnumerable<Contact>> GetByPhoneAsync(string phone)
        {
            var query = @$"SELECT c.* from Contacts c 
                           INNER JOIN Addresses a 
                                    on c.Id = a.ContactId
                           INNER JOIN PhoneNumbers p
                                    on c.Id = p.ContactId
                           WHERE upper(p.Number) = '{phone.ToUpper()}';";

            return await _db.QueryAsync<Contact>(query);
        }
    }
}

