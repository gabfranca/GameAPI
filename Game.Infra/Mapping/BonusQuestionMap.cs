using Game.Domain;
using System.Data.Entity.ModelConfiguration;


namespace Game.Infra.Mapping
{
    public class BonusQuestionMap : EntityTypeConfiguration<BonusQuestion>
    {
        public BonusQuestionMap()
        {
            ToTable("BonusQuestion");
            HasKey(x=>x.Id);
            Property(x=>x.OptionOne).IsRequired();
            Property(x => x.OptionTwo).IsRequired();
            Property(x => x.OptionThree).IsRequired();
            Property(x => x.SumResult).IsRequired();
            HasRequired(x => x.Category);
        }
    }
}
