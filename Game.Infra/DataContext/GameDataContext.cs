using Game.Domain;
using Game.Infra.Mapping;
using System.Configuration;
using System.Data.Entity;

namespace Game.Infra.DataContext
{
    public class GameDataContext : DbContext
    {
        public GameDataContext() : base("GameConnectionString")
        {
             Database.SetInitializer<GameDataContext>(new GameDataConextInitializer());
          // Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }


        public IDbSet<Session> Sessions { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<ChallengeQuestion> ChallengeQuestions { get; set; }

        public IDbSet<BonusQuestion> BonusQuestions { get; set; }

        public IDbSet<Match> Matches { get; set; }


        public IDbSet<Match_User> Match_Users { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new SessionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ChallengeQuestionMap());
            modelBuilder.Configurations.Add(new BonusQuestionMap());
            modelBuilder.Configurations.Add(new MatchMap());
            modelBuilder.Configurations.Add(new Match_UserMap());



            modelBuilder.Entity<Session>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<ChallengeQuestion>();
            modelBuilder.Entity<BonusQuestion>();
            modelBuilder.Entity<Match>();
            modelBuilder.Entity<Match_User>();




        }
        public class GameDataConextInitializer : DropCreateDatabaseIfModelChanges<GameDataContext>
        {
            protected override void Seed(GameDataContext context)
            {
                context.Sessions.Add(new Session { Id = 1, IsActive = true, UserId = 1 });
                context.Sessions.Add(new Session { Id = 2, IsActive = true, UserId = 2 });
                context.Sessions.Add(new Session { Id = 3, IsActive = true, UserId = 3 });

                context.Categories.Add(new Category { Id = 1, Name = "Lógica" });
                context.Categories.Add(new Category { Id = 2, Name = "Conhecimentos Gerais" });
                context.Categories.Add(new Category { Id = 3, Name = "Programação" });
                context.Categories.Add(new Category { Id = 4, Name = "Organização e Métodos" });



                context.Users.Add(new User { Id = 1, Name = "Gabriel França", Email = "gabriel@outlook.com.br", Nickname = "gabriel", Password = "111111" });
                context.Users.Add(new User { Id = 2, Name = "User 01", Email = "gabriel@outlook.com.br", Nickname = "user1", Password = "111111" });
                context.Users.Add(new User { Id = 3, Name = "User 02", Email = "gabriel@outlook.com.br", Nickname = "user2", Password = "111111" });


                context.Matches.Add(new Match { Id = 1, CategoryId = 2,  NumbOfPlayers = 3, Token = "ABCDE" });

                context.Match_Users.Add(new Match_User {Id =1 , MatchId = 1, UserId = 1, Pontuation = 500,BoardPosition = 0, Token = "AAAAA" });
                context.Match_Users.Add(new Match_User { Id = 2, MatchId = 1, UserId = 2, Pontuation = 500, BoardPosition = 0, Token = "AAAAA" });
                context.Match_Users.Add(new Match_User { Id = 3, MatchId = 1, UserId = 3, Pontuation = 500, BoardPosition = 0 , Token = "AAAAA" });

                context.ChallengeQuestions.Add(new ChallengeQuestion
                {
                    Id = 1,
                    CategoryId =1,
                    Question = "Qual das seguintes palavras não se enquadra no grupo?",
                    OptionOne = "Bonito",
                    OptionTwo = "Faca",
                    OptionThree = "Livro",
                    OptionFour = "Lápis",
                    Asnwer = 1,
                    CreatedBy = 1
                });

                context.ChallengeQuestions.Add(new ChallengeQuestion
                {
                    Id = 2,
                    CategoryId = 1,
                    Question = "Suponha que todos os guardas são atletas e todos os atletas são esbeltos. Pode-se concluir que:",
                    OptionOne = "José não é esbelto, José não é atleta",
                    OptionTwo = "Paulo é esbelto, Paulo é atleta",
                    OptionThree = "Edson é atleta, Edson é guarda",
                    OptionFour = "Carlos não é guarda, Carlos não é esbelto.",
                    Asnwer = 1,
                    CreatedBy = 1
                });




                context.BonusQuestions.Add(new BonusQuestion
                {
                    Id = 1, 
                    CategoryId = 4,
                    OptionOne = "Uma vez definida a Missão da Empresa ela será mantida até o fechamento da empresa. Já a Visão pode ser alterada",
                    OptionTwo = "A Missão expressa onde e como a empresa espera obter lucros através da prestação de serviços", 
                    OptionThree = "Na análise SWOT o crescimento Vertical caracteriza-se pelo aumento do número de empregados de um departamento", 
                    SumResult = 2, 
                    CreatedBy =1
                });

                context.SaveChanges();

                base.Seed(context);

            }
        }

    }
}
