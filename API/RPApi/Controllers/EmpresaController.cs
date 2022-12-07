using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RPApi.Models;
using RPApi.MongoDB.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RPApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresasService _empresasService = new EmpresasService();

        [HttpGet]
        public async Task<ActionResult<List<Empresas>>> getEmpresas()
        {
            return _empresasService.getEmpresas().Result;
        }

        [HttpGet]
        public async Task<ActionResult<Empresas>> getOneEmpresa([FromQuery] string codigoEmpresa)
        {
            return _empresasService.getOneEmpresa(codigoEmpresa).Result;
        }

        [HttpPost]
        public async Task<ActionResult<Empresas>> createEmpresa([FromBody] Empresas empresas)
        {
            return _empresasService.CreateEmpresaAsync(empresas).Result;
        }

        //[HttpPut]
        //public async Task<ActionResult<Empresas>> updateEmpresa([FromBody] Empresas empresas)
        //{
        //    return _empresasService.updateEmpresaAsync(empresas).Result;
        //}
    }
}
