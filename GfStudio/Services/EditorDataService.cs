using System.Collections.Generic;
using GfToolkit.Shared.Models.Buffs;
using GfToolkit.Shared.Behaviors;

namespace GfStudio.Services
{
    public class EditorDataService
    {
        public List<BuffSet> BuffSets { get; set; } = new();
        public BuffSet CurrentBuffSet { get; set; } = new BuffSet { Effects = new List<Buff>() };
        public List<Behavior> Behaviors { get; set; } = new List<Behavior>();
        public Behavior CurrentBehavior { get; set; }

        public void AddOrUpdateBehavior()
        {
            if (CurrentBehavior == null) return;
            var existing = Behaviors.FirstOrDefault(b => b.Name == CurrentBehavior.Name);
            if (existing != null)
            {
                int idx = Behaviors.IndexOf(existing);
                Behaviors[idx] = CloneBehavior(CurrentBehavior);
            }
            else
            {
                Behaviors.Add(CloneBehavior(CurrentBehavior));
            }
        }

        private Behavior CloneBehavior(Behavior b)
        {
            if (b is AreaAttackBehavior a)
            {
                return new AreaAttackBehavior()
                {
                    Name = a.Name,
                    Description = a.Description,
                    ApCost = a.ApCost,
                    Power = a.Power,
                    DamageType = a.DamageType,
                    AppliedBuffSet = a.AppliedBuffSet // 필요 시 깊은 복사
                };
            }
            else if (b is SelfEffectBehavior s)
            {
                return new SelfEffectBehavior()
                {
                    Name = s.Name,
                    Description = s.Description,
                    ApCost = s.ApCost,
                    ChangingHp = s.ChangingHp,
                    Effect = s.Effect
                };
            }
            return null;
        }

        public void DeleteBehavior(Behavior b)
        {
            if (b != null) Behaviors.Remove(b);
            if (CurrentBehavior == b) CurrentBehavior = null;
        }
        }
}
