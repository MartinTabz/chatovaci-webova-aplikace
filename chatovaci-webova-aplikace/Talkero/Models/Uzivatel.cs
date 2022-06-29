using System.ComponentModel.DataAnnotations;

namespace Talkero.Models
{
    public class Uzivatel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Mail { get; set; } = String.Empty;

        [Required]
        public string Nick { get; set; } = String.Empty;

        [Required]
        public string Heslo { get; set; } = String.Empty;

        public bool Overen { get; set; }

        public string Profilovka { get; set; } = String.Empty;

        public string Banner { get; set; } = String.Empty;

        public bool Zablokovan { get; set; }

        public string DuvodZablokovani { get; set; } = String.Empty;

        public bool ZadaOOdblokovani { get; set; }

        public bool JeAdmin { get; set; }

        public bool SouhlasiSPodminkami { get; set; }

        public bool ZasilatNovinky { get; set; }

        public string DatumNarozeni { get; set; } = String.Empty;
    }
}
