using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using RPApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace RPApi.MongoDB.DAO
{
    public class EmpresasService
    {
        private IMongoCollection<Empresas> _empresasCollection;

        public EmpresasService()
        {
            RPExampleSettings mongodb = new RPExampleSettings();
            _empresasCollection = mongodb.database().GetCollection<Empresas>("Empresas");
        }

        public async Task<List<Empresas>> getEmpresas()
        {
            return _empresasCollection.Find(e => true).ToList(); ;
        }

        public async Task<Empresas> getOneEmpresa(string codigoEmpresa)
        {
            return _empresasCollection.Find(e => e.CodEmpresa.Equals(codigoEmpresa)).FirstOrDefault();
        }

        public async Task<Empresas> CreateEmpresaAsync(Empresas model)
        {
            var empresasExistentes = _empresasCollection.Find(e => e.CodEmpresa.Equals(model.CodEmpresa)).ToList();
            if(empresasExistentes.Count > 0)
            {
                return new Empresas()
                {
                    StatusCode = 500,
                    Message = "Empresa já cadastrado na base"
                };
            }
            else
            {
                _empresasCollection.InsertOne(model);

                return _empresasCollection.Find(e => e.CodEmpresa.Equals(model.CodEmpresa)).FirstOrDefault();
            }
        }

        //public async Task<Empresas> updateEmpresaAsync(Empresas model)
        //{
        //    var empresaExistente = _empresasCollection.Find(e => e.CodEmpresa.Equals(model.CodEmpresa)).ToList();
        //    if(empresaExistente.Count > 0)
        //    {
        //        Expression<Func<Empresas, bool>> filter = m => (m.Id == model.Id);
        //        var update = Builders<Empresas>.Update
        //            .Set(m => m, model);
        //        var options = new FindOneAndUpdateOptions<Empresas, Empresas>
        //        {
        //            IsUpsert = false,
        //            ReturnDocument = ReturnDocument.After
        //        };

        //        return _empresasCollection.FindOneAndUpdateAsync(filter, update, options).Result;
        //    }
        //    else
        //    {
        //        return new Empresas()
        //        {
        //            StatusCode = 500,
        //            Message = "Empresa não encontrada na base"
        //        };
        //    }
        //}

        public async Task DeleteEmpresaAsync(string codEmpresa)
        {
            _empresasCollection.DeleteOne(u => u.CodEmpresa.Equals(codEmpresa));
        }
    }
}
