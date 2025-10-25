using GfEngine.Battles.Behaviors;
using GfEngine.Battles.Commands;
using GfEngine.Logics;
using GfEngine.Core.Events;
using GfEngine.Core.Conditions;
using GfEngine.Core;
using GfToolkit.Shared;
using System;
using GfEngine.Battles.Units;

namespace GfEngine.Battles.Rules
{
    // 유닛의 패시브 스킬 리스트에 들어갈 규칙들.
    // 사실상 해당 유닛의 EventListner 역할을 수행함.
    public class EventialBehaviorRule : IEventListener
    {
        // [설정값] 이 규칙이 귀 기울일 이벤트 종류
        public BattleEventType EventToListenFor { get; set; }
        // [설정값] 발동 시 실행할 행동 (예: AttackBehavior)
        public Behavior BehaviorToExecute { get; set; }
        // [설정값] 발동 조건 (예: HP가 50% 이하일 때만)
        public ICondition Condition { get; set; }
        public Unit SourceUnit { get; set; } // 예시

        // ... (생성자 및 기타 필드) ...

        /// <summary>
        /// 규칙이 활성화될 때 BattleManager에 자신을 구독자로 등록합니다.
        /// </summary>
        public void Subscribe()
        {
            // 구독 시, 특정 BattleEventType에 이 객체(this)를 리스너로 등록합니다.
            BattleManager.Instance.Subscribe(SourceUnit, EventToListenFor, this);
        }

        /// <summary>
        /// IEventListener 계약 구현: 이벤트가 발생했을 때 호출됩니다.
        /// </summary>
        public void HandleEvent(Enum eventType, IContext context)
        {
            // 1. BattleContext로 타입 변환 (BattleManager에서 넘어온 Context는 BattleContext가 확실함)
            if (!(context is BattleContext battleContext))
            {
                return;
            }

            // 2. 이 Rule이 듣고자 하는 이벤트가 맞는지 확인합니다.
            // (BroadcastEvent 호출 시점에서 이미 필터링되지만, 안전을 위해 다시 확인)
            if (!eventType.Equals(EventToListenFor))
            {
                return;
            }

            // 3. 추가 조건 검사 (예: OnDamaged 이벤트여도, HP 50% 이하일 때만 발동)
            if (Condition != null && !Condition.IsMet(battleContext))
            {
                return;
            }

            // 4. 조건 충족 시, Behavior 실행 및 Command 생성
            // BehaviorToExecute는 BehaviorResult를 반환한다고 가정
            Command result = BehaviorToExecute.Execute(battleContext);

            // 5. 생성된 Command를 BattleManager의 인터럽트 큐에 '새치기'로 삽입
            BattleManager.Instance.AddPendingInterrupt(result);
        }
    }
}
