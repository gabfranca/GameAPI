using Game.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Game.Infra.Mapping
{
    public  class Match_UserMap : EntityTypeConfiguration<Match_User>
    {
        public Match_UserMap()
        {
            ToTable("Match_UserMap");
            HasKey(x => x.Id);
            Property(x => x.Pontuation).IsRequired();
   

            HasRequired(x => x.Match);
            HasRequired(x => x.User);

        }
    }
}
