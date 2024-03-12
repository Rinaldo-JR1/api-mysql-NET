using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace minimal.Dominios.Entidades
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public String Nome { get; set; } = default!;
        [Required]
        [StringLength(50)]
        public String Marca { get; set; } = default!;
        [Required]
        public int Ano { get; set; } = default!;
    }
}