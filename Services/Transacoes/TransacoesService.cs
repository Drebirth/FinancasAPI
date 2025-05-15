using Projeto_FinancasAPI.Repository.Transacoes;
using Projeto_FinancasAPI.Models;
using System.Collections.Generic;
using Projeto_FinancasAPI.Repository.Contas;

namespace Projeto_FinancasAPI.Services.Transacoes
{
    public class TransacoesService
    {
        private readonly ITransacoesRepository _repository;
        private readonly IContaRepository _contaRepository;

        public TransacoesService(ITransacoesRepository repository, IContaRepository conta)
        {
            _repository = repository;
            _contaRepository = conta;
        }

        public async Task<IEnumerable<Transacao>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Transacao> GetByIdAsync(int id)
        {
            if(id == null)
            {
                throw new ArgumentNullException(nameof(id), "Transação não encontrada, favor verificar o id da transação e tentar novamente!");
            }
            return await _repository.GetAsync(id);
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
        {
            var conta = await _contaRepository.GetAsync(transacao.ContaId);
            if (conta == null)
            {
                throw new ArgumentNullException(nameof(conta), "Conta não encontrada, favor verificar o id da conta e tentar novamente!");
            }
            if (transacao.TipoTransacao == Tipo.Entrada)
            {
                conta.Saldo += transacao.Valor;
                await _contaRepository.UpdateAsync(conta);
            } else
            {
                if (conta.Saldo < transacao.Valor)
                {
                    throw new ArgumentException("Saldo insuficiente para realizar a transação.", nameof(transacao.Valor));
                } else
                {
                    conta.Saldo -= transacao.Valor;
                    await _contaRepository.UpdateAsync(conta);

                }
                
            }


            if (transacao == null)
            {
                throw new ArgumentNullException(nameof(transacao), "Transação não encontrada, favor verificar o id da transação e tentar novamente!");
            }
            if (transacao.Valor <= 0)
            {
                throw new ArgumentException("O valor da transação deve ser maior que zero.", nameof(transacao.Valor));
            }

           
            

            transacao.DataTransacao = DateTime.Now;
            await _repository.CreateAsync(transacao);

            return transacao;
        }

    }
}
