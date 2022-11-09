using MongoDB.Driver;
using RPApi.Models;
using System;

namespace RPApi.MongoDB
{
    public class RPExampleSettings
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }

        private IMongoDatabase _database;

        public RPExampleSettings()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                var mongoClient = new MongoClient(settings);

                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoDatabase database()
        {
            return _database;
        }
    }
}
