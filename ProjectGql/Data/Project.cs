using System;
using System.ComponentModel.DataAnnotations;

namespace PMIS.ProjectGql.Data
{
    public class Project
    {
        //�������� � �� ����� ������������ Guid, �.�. ������� Hot Chocolate ���� ��������� ������������ (� ������� ����������� [ID(nameof(Project))] � .EnableRelaySupport())
        public Guid Id { get; set; }

        //������� ��� ����������� ��������� �������������� �� �������
        public string Guid => Id.ToString();

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}