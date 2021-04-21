using System;
using System.ComponentModel.DataAnnotations;

namespace PMIS.DogovorGql.Data
{
    public class Contract
    {
        public Guid Id { get; set; }
        
        public string Guid => Id.ToString();

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}