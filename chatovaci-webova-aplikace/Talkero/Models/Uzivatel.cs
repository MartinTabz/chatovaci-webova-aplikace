using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talkero.Models
{
    public class Uzivatel
    {
        [Key]
        public int Id { get; set; }

        public string Mail { get; set; } = String.Empty;

        public string Nick { get; set; } = String.Empty;

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
    }
}
