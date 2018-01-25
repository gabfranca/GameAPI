using Game.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Game.Infra.Mapping
{
    public class ChallengeQuestionMap : EntityTypeConfiguration<ChallengeQuestion>
    {
        public ChallengeQuestionMap()
        {
            ToTable("ChallengeQuestion");
            HasKey(x => x.Id);

            Property(x => x.OptionOne).HasMaxLength(150).IsRequired();
            Property(x => x.OptionTwo).HasMaxLength(150).IsRequired();
            Property(x => x.OptionThree).HasMaxLength(150).IsRequired();
            Property(x => x.OptionFour).HasMaxLength(150).IsRequired();

            HasRequired(x =>x.Category);




        }
    }
}
