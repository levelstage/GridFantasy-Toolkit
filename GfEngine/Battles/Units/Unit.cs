using GfEngine.Battles.Behaviors;
using GfEngine.Battles.Squares;
using GfEngine.Models.Items;
using GfEngine.Models.Statuses;
using GfEngine.Models.Buffs;
using GfEngine.Logics;
using GfEngine.Models.Buffs.Modifiers;
using GfEngine.Battles.Patterns;
using GfEngine.Models.Actors;
using GfToolkit.Shared;
using System.Collections.Generic;
using System.Linq;
using GfEngine.Core.Conditions;
using GfEngine.Models.Classes;
namespace GfEngine.Battles.Units
{
	public abstract class Unit
	{
		public string Name { get; set; }
		private LiveStatus _liveStat { get; set; }
		public Teams Team { get; set; }
		public List<Behavior> Behaviors { get; set; }
		public Square CurrentSquare { get; set; }

		// 아래의 두 함수에 의해 유닛이 외부와 소통할때는 modify된 스탯들을 사용하게 됩니다.
		// _liveStat은 최종 스탯을 계산하기 위한 모든 재료를 보관하는 창고로 활용됩니다.
		/// <summary>
		/// 현재 Context에서 유효한 Modifier들로 EffectiveModifiers를 채웁니다.
		/// </summary>
		/// <param name="context">Condition들을 체크하고 Fomula를 계산하기 위한 Context.</param>
		public void UpdateEffectiveModifiers(BattleContext context)
		{
			_liveStat.EffectiveModifiers.Clear();
			foreach (var buff in _liveStat.Buffs)
			{
				BattleContext subContext = new BattleContext(baseContext: context, originUnit: buff.Source, targetUnit: this);
				foreach (var cmr in buff.Modifiers)
				{
					if (cmr.Condition.IsMet(subContext))
					{
						if (cmr.ModiferToApply is ScalingModifier sm)
							sm.Magnitude = (float)BattleManager.Instance.BattleFormulaParser.Evaluate(sm.Fomula, subContext);
						_liveStat.EffectiveModifiers.Add(cmr.ModiferToApply);
					}
				}
			}
		}
		/// <summary>
		/// 어떤 effect의 Magnitude 총 합계를 구합니다.
		/// 주로 정량적 버프(공격력 증가 등)에 사용됩니다.
		/// </summary>
		/// <param name="effect"></param>
		/// <returns></returns>
		public float NetEffectMagnitude(BuffEffect effect)
		{
			float res = 0;
			foreach (Modifier iter in _liveStat.EffectiveModifiers)
			{
				if (iter.Effect == effect) res += iter.Magnitude;
			}
			return res;
		}
		/// <summary>
        /// 특정 유형의 모든 유효한 modifier를 return합니다.
		/// 주로 정성적 버프(공격 범위 변경 등)에 사용됩니다.
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
		public List<Modifier> SearchEffectiveModifiers(BuffEffect effect)
        {
			List<Modifier> res = new List<Modifier>();
			foreach (Modifier iter in _liveStat.EffectiveModifiers)
			{
				if (iter.Effect == effect) res.Add(iter);
			}
			return res;
        }

		/// <summary>
		/// _liveStat.Buffs에 특정 code의 버프가 존재하는지 확인합니다.
		/// </summary>
		/// <param name="code">존재여부를 확인할 버프의 DB 인덱스</param>
		/// <returns></returns>
		public bool HasBuff(int code)
		{
			foreach (Buff buff in _liveStat.Buffs)
			{
				if (buff.Code == code) return true;
			}
			return false;
		}
		/// <summary>
		/// 기본 6종 스테이터스들의 최종 상태를 확인합니다.
		/// context가 null인 경우 가장 마지막으로 계산된 EffectiveModifiers를 참조합니다.
		/// </summary>
		/// <returns>"최종" 스테이터스</returns>
		public Status GetStatus()
		{
			Status buffed_status = new Status(_liveStat.Stat);
			buffed_status.MaxHp += (int)NetEffectMagnitude(BuffEffect.MaxHpBoost);
			buffed_status.Defense += (int)NetEffectMagnitude(BuffEffect.DefenseBoost);
			buffed_status.MagicDefense += (int)NetEffectMagnitude(BuffEffect.MagicDefenseBoost);
			buffed_status.Attack += (int)NetEffectMagnitude(BuffEffect.AttackBoost);
			buffed_status.MagicAttack += (int)NetEffectMagnitude(BuffEffect.MagicAttackBoost);
			buffed_status.Agility += (int)NetEffectMagnitude(BuffEffect.AgilityBoost);
			return buffed_status;
		}
		/// <summary>
		/// _liveStat의 현재 Hp를 변화시키기 위한 함수.
		/// </summary>
		/// <param name="amount">양수면 피해, 음수면 회복</param>
		/// <returns>Hp의 실제 변화량</returns>
		public int ChangeHp(int amount)
		{
			// Buffed()를 호출해서 버프가 적용된 현재의 MaxHp를 가져옵니다.
			int currentMaxHp = GetStatus().MaxHp;
			int newHp = _liveStat.CurrentHp + amount;
			int dealt = amount;

			if (newHp < 0)
			{
				dealt = -_liveStat.CurrentHp;
				_liveStat.CurrentHp = 0;
			}
			else if (newHp > currentMaxHp)
			{
				dealt = currentMaxHp - _liveStat.CurrentHp;
				_liveStat.CurrentHp = currentMaxHp; // 버프 적용된 MaxHp 기준으로 제한
			}
			else _liveStat.CurrentHp = newHp;
			return dealt;
		}
		/// <summary>
        /// 해당 유닛의 원본 스탯을 return합니다.
        /// </summary>
        /// <returns></returns>
		public Status GetOriginStatus()
		{
			return _liveStat.Stat;
		}

		// 이 유닛의 특정 스탯을 파싱하는 함수
		public int ParseStat(BattleContext context, StatType statType, bool isOrigin=false)
		{
			if (statType == StatType.CurrentHp) return CurrentHp();
			Status sourceStat;
			if (isOrigin) sourceStat = GetOriginStatus();
			else sourceStat = GetStatus();
			return BattleManager.Instance.GetParticularStat(sourceStat, statType);
        }
		/// <summary>
        /// 이 유닛에게 어떤 피해가 들어왔을 때 얼마나 데미지를 넣을 수 있는지 계산하는 함수
        /// </summary>
        /// <param name="damage">저항력 계산을 하기 전 순수한 데미지</param>
        /// <param name="damageType">피해의 유형</param>
        /// <returns>유닛에게 들어갈 최종 데미지. 양수: 피해(최소 1데미지가 들어감.), 음수: 회복</returns>
		public int CalculateDamage(int damage, DamageType damageType)
		{
			int finalDamage = damage;
			float residence = 1;
			switch (damageType)
			{
				case DamageType.Physical:
					finalDamage -= GetStatus().Defense;
					residence = 1 - NetEffectMagnitude(BuffEffect.PhysicalResidence) / 100;
					break;
				case DamageType.Magical:
					finalDamage -= GetStatus().MagicDefense;
					residence = 1 - NetEffectMagnitude(BuffEffect.MagicalResidence) / 100;
					break;
			}
			finalDamage = (int)(finalDamage * residence);
			if (finalDamage == 0 && damage > 0) finalDamage = 1; // 시스템적 편의 + 변수를 위한 최소 1뎀
			return finalDamage;
		}
		/// <summary>
        /// 유닛의 생존 여부를 확인합니다.
        /// </summary>
        /// <returns>true-생존, false-사망</returns>
		public bool Alive()
		{
			return _liveStat.CurrentHp > 0;
		}
		/// <summary>
		/// 유닛의 현재 HP를 확인합니다.
		/// </summary>
		/// <returns>유닛의 현재 HP</returns>
		public int CurrentHp()
		{
			return _liveStat.CurrentHp;
		}
		/// <summary>
		/// 지정한 Condition을 특정 유형의 유효한 overrider들로 overriding하는 함수
		/// </summary>
		/// <param name="condition">원본 Condition</param>
		/// <param name="effect">적용할 modifier의 effect</param>
		/// <returns></returns>
		private ICondition OverrideConditionByModifiers(ICondition condition, BuffEffect effect)
		{
			ICondition res = condition;
			List<ConditionOverrider> overriders = new List<ConditionOverrider>();
			foreach (var iter in SearchEffectiveModifiers(effect))
			{
				if (iter is ConditionOverrider conditionOverrider) overriders.Add(conditionOverrider);
			}
			overriders = overriders.OrderBy(i => i.OverridingBy) // 기본적으로 Rewrite -> Or -> And 순
									.ThenBy(i => i.Magnitude) // 세부 우선순위는 Magnitude에 의해 결정.
									.ToList();
			foreach (var iter in overriders)
			{
				switch (iter.OverridingBy)
				{
					case OverridingOperator.Rewrite:
						res = iter.ConditonToOverride;
						break;
					case OverridingOperator.Or:
						res = new OrCondition(new List<ICondition>() { res, iter.ConditonToOverride });
						break;
					case OverridingOperator.And:
						res = new AndCondition(new List<ICondition>() { res, iter.ConditonToOverride });
						break;
				}
			}
			return res;
		}
		/// <summary>
        /// 어떤 PatternSet을 지정한 
        /// </summary>
        /// <param name="patternSet"></param>
        /// <param name="effect"></param>
        /// <param name="context"></param>
        /// <returns></returns>
		private PatternSet OverridePatternSetByModifiers(PatternSet patternSet, BuffEffect effect)
		{
			PatternSet res = new PatternSet(patternSet);
			List<PatternSetOverrider> overriders = new List<PatternSetOverrider>();
			foreach (var iter in SearchEffectiveModifiers(effect))
			{
				if (iter is PatternSetOverrider patternSetOverrider) overriders.Add(patternSetOverrider);
			}
			overriders = overriders.OrderBy(i => i.OverridingBy) // 기본적으로 Rewrite -> Or -> And 순
									.ThenBy(i => i.Magnitude) // 세부 우선순위는 Magnitude에 의해 결정.
									.ToList();
			foreach (var iter in overriders)
			{
				switch (iter.OverridingBy)
				{
					case OverridingOperator.Rewrite:
						res = iter.PatternSetToOverride;
						break;
					case OverridingOperator.Or:
						res.Patterns.UnionWith(iter.PatternSetToOverride.Patterns);
						break;
					case OverridingOperator.And:
						res.Patterns.IntersectWith(iter.PatternSetToOverride.Patterns);
						break;
				}
			}
			return res;
		}
		/// <summary>
		/// 유닛의 버프가 적용된 MoveScope를 Return하는 함수.
		/// </summary>
		/// <returns>유닛의 버프가 적용된 MoveRange</returns>
		public RuledPatternSet GetMoveScope()
		{
			Crest crest = GameData.AllCrests[_liveStat.PresentCrest];
			RuledPatternSet res = new RuledPatternSet(GameData.AllPatternSets[crest.MoveSet], crest.Accessible, crest.ObstructedBy, crest.Penetration);
			res.PatternSetToUse = OverridePatternSetByModifiers(res.PatternSetToUse, BuffEffect.OverrideMovePatternSet);
			res.Accessible = OverrideConditionByModifiers(res.Accessible, BuffEffect.OverrideMoveAcceibles);
			res.ObstructedBy = OverrideConditionByModifiers(res.ObstructedBy, BuffEffect.OverrideMoveObstruction);
			res.Penetration += (int)NetEffectMagnitude(BuffEffect.AddMovePenetration);
			return res;
		}
		/// <summary>
        /// 유닛의 버프가 적용된 BasicAttackScope를 Return하는 함수.
        /// </summary>
        /// <returns>유닛의 버프가 적용된 BasicAttackScope</returns>
		public RuledPatternSet GetBasicAttackScope()
		{
			Weapon weapon = _liveStat.Equipment; // weapon은 crest와 달리 타입이 아닌 객체이기에, DB를 참조하지 않음.
			WeaponClass wc = GameData.AllWeaponClasses[weapon.Class];
			RuledPatternSet res = new RuledPatternSet(GameData.AllPatternSets[wc.BasePatternSet], wc.Accessible, wc.ObstructedBy, wc.Penetration);
			res.PatternSetToUse = OverridePatternSetByModifiers(res.PatternSetToUse, BuffEffect.OverrideWeaponPatternSet);
			res.Accessible = OverrideConditionByModifiers(res.Accessible, BuffEffect.OverrideWeaponAccessibles);
			res.ObstructedBy = OverrideConditionByModifiers(res.ObstructedBy, BuffEffect.OverrideWeaponObstruction);
			res.Penetration += (int)NetEffectMagnitude(BuffEffect.AddWeaponPenetration);
			return res;
		}
	}
}