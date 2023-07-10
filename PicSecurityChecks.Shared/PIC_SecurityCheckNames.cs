using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PicSecurityChecks.Shared
{
    [Table("SecurityCheckNames", Schema ="PIC")]
    public class PIC_SecurityCheckNames
    {
        [Key]
        public int id { get; set; }
        [MaxLength(100)]
        public string? applicationId { get; set; }
        [MaxLength(50)]
        public string? application_number { get; set; }
        [Required]

        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set;}

        public string? MiddleName { get; set; }
        public string? OtherName { get; set; }
        public string? OtherSurname { get; set; }
        [MaxLength(100)]
        public string? Gender { get; set; }
        public DateTime dob { get; set; }
        [MaxLength(6)]
        public string? VSCheck { get; set; }
        public string? ReportedReason { get; set; }
        [MaxLength(100)]
        public string? UserId { get; set; }
        [MaxLength(50)]
        public string? UserName { get; set;}
        public string? ReportedType { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool CheckName { get; set; }
        [MaxLength(200)]
        public string? email { get; set; }
        [MaxLength(20)]
        public string? profileName { get; set; }
        [MaxLength(10)]
        public string? pin { get; set; }
        [MaxLength(300)]
        public string? agency { get; set; }
        public string? positionAppliedFor { get; set; }
        public bool nameFlagged { get; set; } = false;
    }
}
