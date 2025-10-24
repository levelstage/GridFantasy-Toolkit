using System.Collections.Generic;
using GfEngine.Models.Buffs;
using GfToolkit.Shared;

namespace GfEngine.Battles.Squares
{
    public class GroundEffect : GameObject
    {   
        // 이 효과를 누가 만들었는가?
        public Teams SourceTeam { get; set; } 
        
        // 이 효과가 적용될 대상은 누구인가? (적, 아군, 중립 등)
        public HashSet<TeamType> TargetRelationships { get; set; }
        
        // 대상에게 실제로 적용될 버프/디버프 효과
        public BuffSet Effect { get; set; }
        
        // 이 효과가 몇 턴 동안 지속되는가?
        public int Duration { get; set; }

        public GroundEffect()
        {
            TargetRelationships = new HashSet<TeamType>();
        }
    }
}