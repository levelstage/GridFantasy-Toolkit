using GfToolkit.Shared.Models.Actors;
using GfToolkit.Shared.Models.Items;
using GfToolkit.Shared.Models.Statuses;

namespace GfToolkit.Shared.Battles
{
    public class ActorUnit : Unit
    {
        public Actor ActorData { get; set; }
        // ActorUnit의 생성자. 주로 플레이어 캐릭터나 적 정예 캐릭터가 판 위에 올라올 때 사용.
        public ActorUnit(Actor actorData, Teams team)
        {
            ActorData = actorData;
            Name = actorData.Name;
            LiveStat = new LiveStatus(actorData.Stat);
            Team = team;
        }
        public override MoveType GetMoveClass()
        {
            return ActorData.MoveClass;
        }
        public override Weapon GetEquipment()
        {
            return ActorData.Equipment;
        }
    }
}