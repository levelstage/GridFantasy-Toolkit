using GfEngine.Models.Buffs;
using GfToolkit.Shared;
using GfEngine.Models.Statuses;
using GfEngine.Battles.Parsing;
using GfEngine.Battles.Commands;
using GfEngine.Core.Events;
using System;
using System.Linq;
using System.Collections.Generic;
using GfEngine.Battles;
using GfEngine.Battles.Units;
using GfEngine.Core;

namespace GfEngine.Logics
{
    public class BattleManager
    {
        public static BattleManager Instance { get; set; }
        public IBattleFormulaParser BattleFormulaParser { get; private set; }
        public CommandSchedular CmdSchedular { get; set; }
        private Dictionary<BattleEventType, List<IEventListener>> _globalListeners = new Dictionary<BattleEventType, List<IEventListener>>();
        private Dictionary<Unit, Dictionary<BattleEventType, List<IEventListener>>> _unitListeners = new Dictionary<Unit, Dictionary<BattleEventType, List<IEventListener>>>();
        private List<Command> _pendingInterrupts = new List<Command>();

        public BattleManager()
        {
            BattleFormulaParser = new BattleNCalcParser();
        }
        public void AddPendingInterrupt(Command command)
        {
            if (command is NullCommand)
            {
                return;
            }
            _pendingInterrupts.Add(command);
        }

        private void RegisterGlobalListener(BattleEventType eventType, IEventListener listener)
        {
            if (_globalListeners.TryGetValue(eventType, out List<IEventListener> eventBus)) eventBus.Add(listener);
            else
            {
                _globalListeners[eventType] = new List<IEventListener>() { listener };
            }
        }

        private void RegisterUnitListener(Unit unit, BattleEventType eventType, IEventListener listener)
        {
            if (unit == null) return;
            if (_unitListeners.TryGetValue(unit, out Dictionary<BattleEventType, List<IEventListener>> busDict))
            {
                if (busDict.TryGetValue(eventType, out List<IEventListener> eventBus)) eventBus.Add(listener);
                else
                {
                    busDict[eventType] = new List<IEventListener>() { listener };
                }
            }
            else
            {
                _unitListeners[unit] = new Dictionary<BattleEventType, List<IEventListener>>()
                {
                    [eventType] = new List<IEventListener>() { listener }
                };
            }
        }

        private void UnregisterGlobalListener(BattleEventType eventType, IEventListener listener)
        {
            if (_globalListeners.TryGetValue(eventType, out List<IEventListener> eventBus)) eventBus.Remove(listener);
        }
        
        private void UnregisterUnitListener(Unit unit, BattleEventType eventType, IEventListener listener)
        {
            if (_unitListeners.TryGetValue(unit, out Dictionary<BattleEventType, List<IEventListener>> busDict))
            {
                if (busDict.TryGetValue(eventType, out List<IEventListener> eventBus)) eventBus.Remove(listener);
            }
        }

        public void Subscribe(Unit sourceUnit, BattleEventType eventType, IEventListener listener)
        {
            // GlobalEvents에 포함되어 있는 경우
            if (GameData.GlobalEvents.Contains(eventType))
            {
                // 전역 리스너 딕셔너리(_globalListeners)에 등록
                RegisterGlobalListener(eventType, listener);
            }
            else
            {
                // 대상 지정 리스너 딕셔너리(_unitListeners)에 등록
                RegisterUnitListener(sourceUnit, eventType, listener);
            }
        }
        
        public void Unsubscribe(Unit SourceUnit, BattleEventType eventType, IEventListener listener)
        {
            if (GameData.GlobalEvents.Contains(eventType))
            {
                UnregisterGlobalListener(eventType, listener);
            }
            else
            {
                UnregisterUnitListener(SourceUnit, eventType, listener);
            }
        }

        public void BroadcastGlobalEvent(BattleEventType eventType, BattleContext context)
        {
            if (_globalListeners.TryGetValue(eventType, out List<IEventListener> subscribers))
            {
                foreach (var listener in subscribers.ToList())
                {
                    // 리스너의 HandleEvent에 eventType도 함께 전달합니다.
                    listener.HandleEvent(eventType, context);
                }
                
            }
        }
        
        public void NotifyTarget(Unit eventTargetUnit, BattleEventType eventType, IContext eventContext)
        {
            if (!_unitListeners.ContainsKey(eventTargetUnit)) return;
            if (_unitListeners[eventTargetUnit].TryGetValue(eventType, out List<IEventListener> subscribers))
            {
                foreach (var listener in subscribers.ToList())
                {
                    // 리스너의 HandleEvent에 eventType도 함께 전달합니다.
                    listener.HandleEvent(eventType, eventContext);
                }
            }
        }

        public int GetParticularStat(Status status, StatType type, float coefficient = 1f)
        {
            float res;
            switch (type)
            {
                case StatType.MaxHp:
                    res = status.MaxHp * coefficient;
                    break;
                case StatType.Defense:
                    res = status.Defense * coefficient;
                    break;
                case StatType.MagicDefense:
                    res = status.MagicDefense * coefficient;
                    break;
                case StatType.Attack:
                    res = status.Attack * coefficient;
                    break;
                case StatType.MagicAttack:
                    res = status.MagicAttack * coefficient;
                    break;
                case StatType.Agility:
                    res = status.Agility * coefficient;
                    break;
                default:
                    res = 0;
                    break;
            }
            return (int)res;
        }
    }
}