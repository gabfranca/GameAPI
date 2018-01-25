using Game.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Game.Infra.Mapping
{
    public class MatchMap : EntityTypeConfiguration<Match>
    {
        public MatchMap()
        {

            ToTable("Match");
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.NumbOfPlayers).IsRequired();
            Property(x => x.Token).HasMaxLength(5);
            Property(x => x.CategoryId).IsRequired();

            HasRequired(x => x.Category);
        }
    }
}
