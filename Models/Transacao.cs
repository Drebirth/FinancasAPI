using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_FinancasAPI.Models
{
    [Table("Transacoes")]
    public class Transacao
    {
        private int _id;
        private decimal _valor;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataTransacao { get; set; }
        public Tipo TipoTransacao { get; set; }

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        
      

    }
}
