using MMASimulation.Shared.Engine.Comments.Utils;
using MMASimulation.Shared.Engine.FightUtils;
using MMASimulation.Shared.Models.Fighters;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMASimulation.Shared.Models.Fights
{
	public class Fight
	{

		public int Id { get; set; }
		public int NumberRounds { get; set; }
		public bool TitleBout { get; set; } = false;
		public bool GeneratePBP { get; set; } = false;
		public bool Happened { get; set; } = false;

		public int Fighter1Id { get; set; }
		[ForeignKey(nameof(Fighter1Id))]
		public required Fighter Fighter1 { get; set; }

		public int Fighter2Id { get; set; }
		[ForeignKey(nameof(Fighter2Id))]
		public required Fighter Fighter2 { get; set; }

		public List<FightPBP> Pbp { get; set; } = new List<FightPBP>();

		[NotMapped]
		public FightAttributes? FightAttributes { get; set; }

		public void FightSim()
		{

			//Inicia Atributos da Luta
			FightAttributes atributtes = new FightAttributes();

			//Prepara os lutadres
			PrepareFights.PrepareFight(Fighter1, Fighter2, NumberRounds);
			PrepareFights.MakeTempFighters(atributtes, Fighter1, Fighter2);

			//Comentarios Iniciais
			SpecificComments.MakeLocutorComment(Fighter1, Fighter2, Pbp, atributtes, TitleBout);
			SpecificComments.MakeOddsComment(Fighter1, Fighter2, Pbp, atributtes);

			FightController(atributtes);
		}

		public void FightController(FightAttributes atributtes)
		{

			//Simula os rounds
			for (atributtes.CurrentRound = 1; atributtes.CurrentRound <= NumberRounds && !atributtes.BoutFinished; atributtes.CurrentRound++)
			{
				//Comentario indicando o Round
				SpecificComments.MakeCurrentRoundComment(Pbp, atributtes.CurrentRound);

				//Lutadores em Pé ao iniciar o Round
				PrepareFights.PrepareRound(Fighter1, Fighter2);

				for (atributtes.FightSeconds = 0; atributtes.FightSeconds <= 300; atributtes.FightSeconds += TimeUtils.DeltaTime(Fighter1, Fighter2))
				{
					//Comentario indicando inicio da Luta
					SpecificComments.MakeFirstRoundComment(Fighter1, Fighter2, Pbp, atributtes);

					//Comentario indicando os segundos passados do Round
					SpecificComments.MakeFightTimeComment(Pbp, atributtes);

					//Chama os atributos de ação de luta
					//Action();

					//Finaliza a luta por finalização
					if (atributtes.BoutFinished)
					{
						break;
					}

				}

				//Finaliza o Round
				if (atributtes.FightSeconds >= 300)
				{
					//FinishRound();
				}

				//Finaliza a luta por tempo
				if (atributtes.CurrentRound >= NumberRounds)
				{
					atributtes.BoutFinished = true;
				}
			}

			if (atributtes.FighterWinner != -1)
			{
				//FinishFight(atributtes.FighterWinner);

			}
			else
			{
				//JudgeFightRound(3);
				//FinishFight(atributtes.FighterWinner);
			}

		}


	}
}
