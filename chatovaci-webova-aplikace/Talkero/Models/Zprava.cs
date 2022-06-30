using System.ComponentModel.DataAnnotations;

namespace Talkero.Models
{
    public class Zprava
    {
        [Key]
        public int ZpravaId { get; set; }

        public string ObsahZpravy { get; set; } = String.Empty;

        public string Obrazek { get; set; } = String.Empty;
        
        public DateTime DatumOdeslani { get; set; }
    }
}
