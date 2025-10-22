using GfEngine.Battles.Behaviors;
using GfEngine.Models.Actors;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfEngine.Models.Buffs;
using System.Collections.Generic;
using GfToolkit.Shared;
namespace GfEngine.Battles.Units
{
    public class InstantUnit : Unit
    {
        public Status Stat;
		public MoveType MoveClass { get; set; }
		public WeaponType WeaponClass { get; set; }
		public Skill UniqueSkill { get; set; }
		public Weapon Equipment { get; set; }
		public List<Item> Inventory { get; set; }
		public List<Trait> Traits { get; set; }
        // InstantUnit의 생성자. 해당 웨이브에만 등장하는 임시 유닛 생성용임.

        public InstantUnit(Actor actor, Teams team)
        {
            MoveClass = actor.MoveClass;
            WeaponClass = actor.Equipment.Type;
            Stat = actor.Stat;
            LiveStat = new LiveStatus(actor.Stat);
            Team = team;
            Name = actor.Name;
            UniqueSkill = actor.UniqueSkill;
            Equipment = actor.Equipment;
            Inventory = new List<Item>(actor.Inventory); // 장비는 이미 들어가 있음.
            Traits = actor.Traits;
        } 
        public InstantUnit(string name, Status status, MoveType moveType, Weapon weapon, Skill uniqueSkill, Teams team, List<Item> inventory, List<Trait> traits)
        {
            MoveClass = moveType;
            WeaponClass = weapon.Type;
            Stat = status;
            LiveStat = new LiveStatus(status);
            Team = team;
            Name = name;
            UniqueSkill = uniqueSkill;
            Equipment = weapon;
            Inventory = inventory;
            Traits = traits;
        }

        public InstantUnit(string name, Status status, MoveType moveType, Weapon weapon, Teams team)
            : this(name, status, moveType, weapon, null, team, new List<Item>(), new List<Trait>())
        {
            
        }

        public override MoveType GetMoveClass()
        {
            return MoveClass;
        }

        public override Weapon GetEquipment()
        {
            return Equipment;
        }
    }
}