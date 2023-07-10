using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PicSecurityChecks.Shared
{
    [Table("PIC_StaticRecords")]
    public class PIC_FlaggedNames
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? firstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? lastName { get; set; }
        [MaxLength(100)]
        public string? otherNames { get; set; }
        [MaxLength(500)]
        public string? reason { get; set; }
        [MaxLength(500)]
        public string? comment { get; set; }
        [MaxLength(50)]
        public string? createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        [MaxLength(50)]
        public string? modifiedBy { get; set; }
        public DateTime? modifiedAt { get; set; }
        public DateTime? dob { get; set; }
    }
}
