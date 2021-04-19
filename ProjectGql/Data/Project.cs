using System;
using System.ComponentModel.DataAnnotations;

namespace PMIS.ProjectGql.Data
{
    public class Project
    {
        //возиожно и не нужно использовать Guid, т.к. система Hot Chocolate сама добавляет уникальность (с помощью конструкции [ID(nameof(Project))] и .EnableRelaySupport())
        public Guid Id { get; set; }

        //добавил для возможности получения идентификатора на клиенте
        public string Guid => Id.ToString();

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}