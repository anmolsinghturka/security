
namespace PicSecurityChecks.Shared
{
    public class Niche
    {
        public string? Id { get; set; }
        public string? PersonId { get; set; }
        public string? OccurrenceFileNo { get; set; }
        public string? SurName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Involvement { get; set; }
        public string? Flag { get; set; }
        public string? OccurrenceType { get; set; }
        public string? SearchScore { get; set; }
        public string? IDEN { get; set; }
        public string? FPS { get; set; }
        public List<NicheCharge> Charges { get; set; } = new List<NicheCharge>();
        public string? errMessage { get; set; }
        public string? Link { get; set; }
        public string? errormsg { get; set; }
    }
}
