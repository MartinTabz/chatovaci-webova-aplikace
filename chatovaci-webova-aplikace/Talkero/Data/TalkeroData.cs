using Microsoft.EntityFrameworkCore;
using Talkero.Models;

namespace Talkero.Data
{
    public class TalkeroData : DbContext
    {
        public DbSet<Models.Uzivatel> Uzivatele { get; set; }
        public DbSet<Models.Zprava> Zpravy { get; set; }

        public TalkeroData (DbContextOptions<TalkeroData> options) : base(options) { }
    }
}
