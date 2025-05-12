using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projeto_FinancasAPI.Models
{
    [Table("Transacoes")]
    public class Transacao
    {
        private int _id;
        private decimal _valor;

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [ForeignKey("ContaId")]
        public int ContaId { get; set; }

        [JsonIgnore]
        public Conta? conta
        { get; set; }

        
        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? categoria
        { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataTransacao { get; set; }
        public Tipo TipoTransacao { get; set; }

    
        
      

    }
}
