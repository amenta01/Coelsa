using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Coelsa.Common.Interfaces;
using Coelsa.Common.Models;
using Microsoft.Extensions.Configuration;
using Coelsa.Common.Common;

namespace Coelsa.Service.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private string connectionString;

        public ContactRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DbConnectionString");
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public async Task Add(Contact contact)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"INSERT INTO Contacts (FirstName, LastName, Company, Email, PhoneNumber) values (@FirstName, @LastName, @Company, @Email, @PhoneNumber)";
                await dbConnection.ExecuteAsync(query, contact);
            }
        }

        public async Task<PaginationGeneric<Contact>> GetByCompany(string company, int pageNumber = 1, int pageSize = 10)
        {
            var parameters = new { Company = company,
                                      Skip = (pageNumber - 1) * pageSize, 
                                      Take = pageSize };

            string query = @"SELECT * FROM Contacts WHERE Company = @Company ORDER BY CURRENT_TIMESTAMP ";
            if (string.IsNullOrEmpty(company))
                query = "SELECT * FROM Contacts ORDER BY Company ";
            
            query += "OFFSET @Skip ROWS ";
            query += "FETCH NEXT @Take ROWS ONLY";

            using (IDbConnection dbConnection = Connection)
            {   
                dbConnection.Open();
                PaginationGeneric<Contact> paginator = new PaginationGeneric<Contact>
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    Company = company,
                    Result = await dbConnection.QueryAsync<Contact>(query, parameters)
                };

                return paginator;
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"DELETE Contacts WHERE IdContact = @IdContact";
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, new { IdContact = id });
            }
        }

        public async Task Update(Contact contact)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"Update Contacts SET FirstName = @FirstName, LastName = @LastName, Company = @Company, Email = @Email, PhoneNumber = @PhoneNumber WHERE IdContact = @IdContact";
                dbConnection.Open();
                await dbConnection.QueryAsync(query, contact);
            }
        }
    }
}
