using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] //When EF creates this table it's gonna call it photos.
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; } // to see if this is their main photo
        public string PublicId { get; set; }

        public AppUser AppUser { get; set; } //This is to fully define the relationship between the AppUser 
        public int AppUserId { get; set; } //and our photo entity
    }
}