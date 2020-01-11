using DataAccess.TableAccess;
using Interfaces.ITableUnitOfWork;
using Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.WindowsAzure.Storage.Table;
using Models.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsersService : IUsersService
    {
        private readonly ITableUnitOfWork UnitOfWork;

        private readonly CloudTable Table;
        private readonly IConfiguration Configuration;

        public UsersService(IConfiguration config, ITableUnitOfWork uow)
        {
            UnitOfWork = uow;
            var usersTable = config.GetSection("Azure").GetSection("UsersTable").Value;
            uow.SetTable(usersTable);
            Table = new CryptoTideTableContext(config, usersTable).Table;
            Configuration = config;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            TableContinuationToken token = null;
            List<User> allUsers = new List<User>();
            do
            {
                var queryResult = await Table.ExecuteQuerySegmentedAsync(new TableQuery<User>(), token);
                allUsers.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            }
            while (token != null);

            return allUsers;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await GetSingleRow("PartitionKey", email);
            return user;
        }

        public async Task<User> GetUserByToken(string token)
        {
            var user = await GetSingleRow("RowKey", token);
            return user;
        }

        private async Task<User> GetSingleRow(string column, string value)
        {
            
            var condition = TableQuery.GenerateFilterCondition(column, QueryComparisons.Equal, value);
            var query = new TableQuery<User>().Where(condition);

            TableContinuationToken token = null;
            List<User> users = new List<User>();
            do
            {
                var results = await Table.ExecuteQuerySegmentedAsync(query, token);
                users.AddRange(results.Results);
                token = results.ContinuationToken;
            } while (token == null);

            return users[0];
        }

        public async Task<User> Authenticate(UserLogin user)
        {
            var emailCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user.Email);
            var passwordCondition = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, user.Password);
            var combinedCondition = TableQuery.CombineFilters(emailCondition, TableOperators.And, passwordCondition);
            var query = new TableQuery<User>().Where(combinedCondition);

            TableContinuationToken token = null;
            List<User> users = new List<User>();

            do
            {
                var results = await Table.ExecuteQuerySegmentedAsync(query, token);
                users.AddRange(results.Results);
                token = results.ContinuationToken;
            } while (token != null);

            var authenticatedUser = users[0];
            authenticatedUser.Token = GetToken(authenticatedUser);
            UnitOfWork.Update(authenticatedUser);
            await UnitOfWork.CommitChanges();
            return authenticatedUser;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Configuration.GetSection("Authentication").GetSection("Secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                { 
                    new Claim(ClaimTypes.GivenName, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task CreateUser(User user)
        {
            UnitOfWork.Create(user);
            await UnitOfWork.CommitChanges();
        }

        public async Task UpdateUser(User user)
        {
            UnitOfWork.Update(user);
            await UnitOfWork.CommitChanges();
        }

        public async Task DeleteUser(User user)
        {
            UnitOfWork.Delete(user);
            await UnitOfWork.CommitChanges();
        }

        public async Task BanUser(User user, string motive, TimeSpan duration)
        {
            var blockEnd = DateTime.Now + duration;
            user.BlockedFor = motive;
            user.BlockedUntil = blockEnd;
            user.Active = false;

            UnitOfWork.Update(user);
            await UnitOfWork.CommitChanges();
        }
    }
}
