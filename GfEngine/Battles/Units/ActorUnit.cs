using GfEngine.Models.Actors;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfToolkit.Shared;

namespace GfEngine.Battles.Units
{
    public class ActorUnit : Unit
    {
        public Actor ActorData { get; set; }
        // ActorUnit의 생성자. 주로 플레이어 캐릭터나 적 정예 캐릭터가 판 위에 올라올 때 사용.
        public ActorUnit(Actor actorData, Teams team)
        {
            ActorData = actorData;
            Name = actorData.Name;
            Team = team;
        }
    }
}