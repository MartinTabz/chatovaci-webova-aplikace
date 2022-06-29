using Microsoft.EntityFrameworkCore;

namespace Talkero.Data
{
    public class TalkeroData : DbContext
    {
        //public DbSet<Models.**název modelu**>** název tabulky pro databázi** { get; set; }

        public TalkeroData (DbContextOptions<TalkeroData> options) : base(options) { }
    }
}
