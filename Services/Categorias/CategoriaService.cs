﻿using Projeto_FinancasAPI.Repository.Categorias;
using Projeto_FinancasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Projeto_FinancasAPI.Services.Categorias
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var categorias = await _repository.GetAllAsync();
            return categorias;
        }

        public async Task<Categoria?> GetCategoriaAsync(int id)
        {
            var categoria = await _repository.GetAsync(id);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada, favor tentar novamente!");
            }
            return categoria;
        }

        public async Task<Categoria> CreateCategoriaAsync(Categoria categoria)
        {
            if (categoria is null)
            {
                throw new Exception("Dados enviados estão vazios, favor verificar os dados e efetuar o envio novamente!");

            }
            await _repository.CreateAsync(categoria);
            return categoria;
        }

        public async Task<Categoria> UpdateCategoriaAsync(int id, Categoria categoria)
        {
            

            if (id != categoria.Id)
            {
                throw new Exception("Categoria não encontrada, favor verificar o codigo indetificador e tentar novamente...");
            } else
            {
               await  _repository.UpdateAsync(categoria);

            }

            return categoria;
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            var categoria = await _repository.GetAsync(id);
            if (categoria is null)
            {
                throw new Exception("Categoria não encontrada, favor verificar o codigo indetificador e tentar novamente...");
            }
            await _repository.DeleteAsync(categoria);
        }
    }
}
