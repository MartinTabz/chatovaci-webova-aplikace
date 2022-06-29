using System.ComponentModel.DataAnnotations;

namespace Talkero.Models
{
    public class Zprava
    {
        [Key]
        public int ZpravaId { get; set; }

        public string ObsahZpravy { get; set; }

        public string Obrazek { get; set; }   
        
        public DateTime DatumOdeslani { get; set; }
    }
}
