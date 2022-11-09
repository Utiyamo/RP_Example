using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using RPApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RPApi.MongoDB.DAO
{
    public class UsersService
    {
        private IMongoCollection<Users> _usersCollection;

        public UsersService()
        {
            RPExampleSettings mongodb = new RPExampleSettings();
            _usersCollection = mongodb.database().GetCollection<Users>("Users");
        }

        public async Task<List<Users>> getUsers()
        {
            var listUsers = _usersCollection.Find(u => true).ToList();

            return listUsers;
        }

        public async Task<Users> getOneUser(string email)
        {
            var user = _usersCollection.Find(u => u.Email == email).First();

            return user;
        }

        public async Task<Users> getUserByLogin(string username, string password)
        {
            var user = _usersCollection.Find(u => u.Username == username).FirstOrDefault();

            if (!String.IsNullOrEmpty(user?.Password))
            {
                if (password.Equals(user?.Password))
                    return user;
                else
                    return new Users();
            }
            else
                return new Users();
        }

        public async Task<Users> CreateUserAsync(Users user)
        {
            var usersWithUsername = _usersCollection.Find(u => u.Username == user.Username || u.Email == user.Email).ToList();
            if (usersWithUsername.Count > 0)
                return new Users()
                {
                    StatusCode = 500, 
                    Message = "Username ou Email já cadastrado na base"
                };
            else
            {
                _usersCollection.InsertOne(user);

                return _usersCollection.Find(u => u.Id == user.Id).FirstOrDefault();
            }
        }

        public async Task DeleteUserAsync(string email)
        {
            _usersCollection.DeleteOne(u => u.Email == email);
        }
    }
}
