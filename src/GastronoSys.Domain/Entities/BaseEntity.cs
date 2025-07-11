using System.ComponentModel.DataAnnotations;

namespace GastronoSys.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedByUserId { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedByUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public int? DeletedByUserId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }



        //public string? CreatedByUserAgent { get; set; }
        //public string? UpdatedByUserAgent { get; set; }

    }
}
