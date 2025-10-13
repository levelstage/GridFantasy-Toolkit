using GfToolkit.Shared.Models.Actors;
using GfToolkit.Shared.Models.Items;
using GfToolkit.Shared.Models.Statuses;
using System.Collections.Generic;
namespace GfToolkit.Shared.Battles
{
	public class Wave
	{
		public Square[,] Map;
		public List<Unit> Enemies;
		public Wave()
		{
			Map = new Square[8, 8];
			Enemies = new List<Unit>();
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Square s = new Square(j, i);
					if (i == 1)
					{
						// 테스트용 적 폰 배치. 7랭크에 위치할 예정임.
						Status pawnStatus = new Status(20, 10, 5, 8, 0, 5); // 보병 스탯
						Unit myPawn = new InstantUnit("방패병", pawnStatus, MoveType.Pawn_Down, GameData.AllWeapons[WeaponCode.IronShield], Teams.Enemies);
						s.PlaceUnit(myPawn);
						Enemies.Add(myPawn);
					}
					if (i == 6)
					{
						// 테스트용 아군 폰 배치. 2랭크에 위치할 예정임.
						Unit myPawn = new InstantUnit(GameData.AllActors[ActorCode.Phantom], Teams.Players);
						s.PlaceUnit(myPawn);

					}
					if (i == 5 && j == 5)
					{
						Actor hagen = new Actor(GameData.AllActors[ActorCode.Hagen]);
						Unit myPiece = new ActorUnit(hagen, Teams.Players);
						s.PlaceUnit(myPiece);
					}
					Map[i, j] = s;
				}
			}

		}
	}
}