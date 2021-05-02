using System;
using System.ComponentModel.DataAnnotations;

namespace PMIS.Contracts
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModifyDate { get; set; }
    }
}