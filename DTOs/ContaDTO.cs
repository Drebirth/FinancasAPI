using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Projeto_FinancasAPI.DTOs
{
    public class ContaDTO
    {
        private int _id;
        private string _usuario;
        private decimal _saldo;

        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }
    }
}
