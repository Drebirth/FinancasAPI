using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_FinancasAPI.Models
{
    [Table("Contas")]
    public class Conta
    {
        private int _id;
        private string _usuario;
        private decimal _saldo;

        [Key]
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


        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [Column(TypeName = "decimal(18,2)")]     
        public decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }
    }
}
