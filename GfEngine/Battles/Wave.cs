using GfEngine.Models.Actors;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfEngine.Battles.Squares;
using GfEngine.Battles.Units;
using System.Collections.Generic;
using GfToolkit.Shared;
namespace GfEngine.Battles
{
	public class Wave
	{
		public Square[,] Map;
		public List<Unit> Entities;
		public Wave()
		{
			Map = new Square[8, 8];
			Entities = new List<Unit>();
		}
	}
}