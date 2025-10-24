using GfToolkit.Shared.Dtos.Models;
using GfToolkit.Shared.Dtos.Behaviors;
namespace GfToolkit.Shared.Dtos
{
    public class GameDataDto
    {
        public static GameDataDto Database { get; set; } = new();
        public List<ActorDto> Actors { get; set; } = new();
        public List<BehaviorDto> Behaviors { get; set; } = new();
        // public List<SkillDto> Skills { get; set; } = new();
        // public List<ItemDto> Items { get; set; } = new();
        // public List<WeaponDto> Weapons { get; set; } = new();
        public List<ModifierDto> Buffs { get; set; } = new();
        // public static List<TraitDto> Traits { get; set; } = new();
    }
}