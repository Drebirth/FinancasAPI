﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Services.Contas;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContasController : ControllerBase
    {      
        private readonly ContaService _service;

        public ContasController(ContaService service)
        {
            _service = service;
        }



        [HttpGet(Name = "GetContas")]
        public async Task<IActionResult> Get()
        {                   
            return Ok(await _service.GetContasAsync());
        }

        [HttpPost(Name = "PostConta")]
        [SwaggerIgnore]
        public async Task<IActionResult> Post(Conta conta)
        {                   
            await _service.CreateContaAsync(conta);
            
            return new CreatedAtRouteResult("GetContas", new { id = conta.Id }, conta);

        }

        [HttpGet("{id}", Name = "GetConta")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetContaAsync(id));
        }

        [HttpPut("{id}", Name = "PutConta")]
        public async Task<IActionResult> Put(int id, Conta conta)
        {
            await _service.UpdateContaAsync(id, conta);

            return Ok("Dados atualizados");
        }

        [HttpDelete("{id}", Name = "DeleteConta")]
        public async Task<IActionResult> Delete(int id)
        {
            
            await _service.DeleteContaAsync(id);
            return Content("Conta excluída com sucesso!");
        }
    }
}
