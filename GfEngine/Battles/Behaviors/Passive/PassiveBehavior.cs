using GfEngine.Battles.Commands;
using GfEngine.Battles.Units;
using GfEngine.Core;
using GfEngine.Core.Conditions;
using GfEngine.Core.Events;
using GfEngine.Logics;
using GfToolkit.Shared;
using System;
using System.Collections.Generic;

namespace GfEngine.Battles.Behaviors.Passive
{
    public abstract class PassiveBehavior : Behavior, IEventListener
    {
        public BattleEventType EventToListenFor { get; set; }  // 이 Behavior가 귀 기울일 이벤트 종류
        public ICondition Condition { get; set; }  // 발동 조건
        public Unit SourceUnit { get; set; }  // 이 Behavior를 가진 유닛

        /// <summary>
        /// 유닛의 행동 목록에 추가될 때 BattleManager에 자신을 구독자로 등록합니다.
        /// </summary>
        public void Subscribe()
        {
            // 구독 시, 특정 BattleEventType에 이 객체(this)를 리스너로 등록합니다.
            BattleManager.Instance.Subscribe(SourceUnit, EventToListenFor, this);
        }
        /// <summary>
        /// 유닛의 행동 목록에서 사라질 때 구독을 해제합니다.
        /// </summary>
        public void Unsubscribe()
        {
            BattleManager.Instance.Unsubscribe(SourceUnit, EventToListenFor, this);
        }

        /// <summary>
        /// IEventListener 계약 구현: 이벤트가 발생했을 때 호출됩니다.
        /// </summary>
        public void HandleEvent(Enum eventType, IContext context)
        {
            if ((BattleEventType)eventType != EventToListenFor) return; 
            if (Condition.IsMet(context) && context is BattleContext battleContext) 
            {
                Execute(battleContext);
            }
        }
        public PassiveBehavior() { }
    }
}