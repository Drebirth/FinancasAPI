﻿using Projeto_FinancasAPI.DTOs;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Repository.Contas;

namespace Projeto_FinancasAPI.Services.Contas
{
    public class ContaService
    {
        private readonly IContaRepository _repository;

        public ContaService(IContaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContaDTO>> GetContasAsync()
        {
            //var contas = await _repository.GetAllAsync();
            //return contas;

            var contasDTO = new List<ContaDTO>();
            foreach(var conta in await _repository.GetAllAsync())
            {
                var contaDTO = new ContaDTO
                {
                    Id = conta.Id,
                    Usuario = conta.Usuario,                   
                    Saldo = conta.Saldo
                };
                contasDTO.Add(contaDTO);
            }
            return contasDTO;
        }

        public async Task<ContaDTO?> GetContaAsync(int id)
        {
            //var conta = await _repository.GetAsync(id);
            //if (conta == null)
            //{
            //    throw new Exception("Conta não encontrada, favor tentar novamente!");
            //}
            //return conta;
            var contaDTO = new ContaDTO();
            var conta = await _repository.GetAsync(id);
            if (conta == null)
            {
                throw new Exception("Conta não encontrada, favor tentar novamente!");
            }
            else
            {
                contaDTO.Id = conta.Id;
                contaDTO.Usuario = conta.Usuario;
                contaDTO.Saldo = conta.Saldo;
            }
            return contaDTO;
        }

        public async Task<Conta> CreateContaAsync(Conta conta)
        {
            var usuario = await _repository.GetUsuarioExiste(conta.Usuario);

            if (conta is null)
            {
                throw new Exception("Dados enviados estão vazios, favor verificar os dados e efetuar o envio novamente!");
            }

            if (usuario != null)
            {
                throw new Exception("Usuário já existe, favor verificar os dados e efetuar o envio novamente!");
            }
            else
            {
                await _repository.CreateAsync(conta);

            }
            return conta;
        }

        public async Task<Conta> UpdateContaAsync(int id, Conta conta)
        {
            if (conta == null || id != conta.Id)
            {
                throw new Exception("Dados inválidos.");
            }
            else
            {
                await _repository.UpdateAsync(conta);

            }

            return conta;
        }

        public async Task DeleteContaAsync(int id)
        {
            var conta = await _repository.GetAsync(id);
            if (conta == null)
            {
                throw new Exception("Conta não encontrada, favor tentar novamente!");
            }
            else
            {
                await _repository.DeleteAsync(conta);
            }
        }
    }
}
