using MongoDB.Driver;
using MongoDB.Bson;
using RPApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace RPApi.MongoDB.DAO
{
    public class ConfigurationService
    {
        private IMongoCollection<Configurations> _configCollection;

        public ConfigurationService()
        {
            RPExampleSettings mongodb = new RPExampleSettings();
            _configCollection = mongodb.database().GetCollection<Configurations>("Configurations");
        }

        public async Task<Configurations> getConfiguration(string codEmpresa)
        {
            return _configCollection.Find(c => c.CodEmpresa.Equals(codEmpresa)).FirstOrDefault();
        }

        public async Task<Configurations> CreateNewConfiguration(Configurations model)
        {
            var confs = getConfiguration(model.CodEmpresa);

            if (!String.IsNullOrEmpty(confs?.Result.CodEmpresa))
            {
                _configCollection.InsertOne(model);
                return _configCollection.Find(c => c.CodEmpresa.Equals(model.CodEmpresa)).FirstOrDefault();
            }
            else
            {
                return new Configurations()
                {
                    StatusCode = 500,
                    Message = "Configuration ja cadastrado na base"
                };
            }
        }

        public async Task<Configurations> UpdateConfiguration(Configurations model)
        {
            var conf = _configCollection.Find(c => c.CodEmpresa.Equals(model.CodEmpresa)).FirstOrDefault();
            if (conf != null)
            {
                Expression<Func<Configurations, bool>> filter = m => (m.CodEmpresa.Equals(model.CodEmpresa));
                var update = Builders<Configurations>.Update
                    .Set(m => m.jwtModel, model.jwtModel);

                var options = new FindOneAndUpdateOptions<Configurations, Configurations>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                };

                return _configCollection.FindOneAndUpdate(filter, update, options);
            }
            else
            {
                return new Configurations()
                {
                    StatusCode = 500,
                    Message = "Configuration inválida para atualização"
                };
            }
        }
    }
}
