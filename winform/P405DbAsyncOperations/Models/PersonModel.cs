using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P405DbAsyncOperations.Models
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar", Order = 2)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar", Order = 3)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(30)]
        [Column(TypeName = "varchar", Order = 4)]
        [EmailAddress]
        public string Email { get; set; }

    }
}
