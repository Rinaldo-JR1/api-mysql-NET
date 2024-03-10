using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace minimal.Dominios.Entidades
{
    public class Adiminstrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public String Email { get; set; } = default!;
        [Required]
        [StringLength(50)]
        public String Senha { get; set; } = default!;
        [Required]
        [StringLength(255)]
        public String Perfil { get; set; } = default!;
    }
}