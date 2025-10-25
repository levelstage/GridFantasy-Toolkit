using NCalc;
using System;
using GfToolkit.Shared;
using System.Linq;
using GfEngine.Battles.Squares;

namespace GfEngine.Battles.Parsing
{
    // NCalc를 "래핑(Wrapping)"하는 클래스
    public class BattleNCalcParser : IBattleFormulaParser
    {
        public double Evaluate(string formula, BattleContext context)
        {
            Expression e = new Expression(formula);
            e.EvaluateFunction += (name, args) =>
            {
                switch (name)
                {
                    case "O.HasBuff":
                        // HasBuff(O or T) O 또는 T가 해당하는 버프를 가지고 있는지.
                        if (args.Parameters.Count() == 1)
                        {
                            int buffCode = (int)args.Parameters[0].Evaluate();
                            bool hasBuff = context.OriginUnit?.HasBuff(buffCode) == true;
                            args.Result = hasBuff ? 1.0 : 0.0; 
                        }
                        else
                        {
                            args.Result = 0.0; 
                        }
                        break;
                    case "T.HasBuff":
                        // HasBuff(O or T) O 또는 T가 해당하는 버프를 가지고 있는지.
                        if (args.Parameters.Count() == 1)
                        {
                            int buffCode = (int)args.Parameters[0].Evaluate();
                            bool hasBuff = context.TargetUnit?.HasBuff(buffCode) == true;
                            args.Result = hasBuff ? 1.0 : 0.0; 
                        }
                        else
                        {
                            args.Result = 0.0; 
                        }
                        break;
                    
                    case "TS.Occupied":
                        if (args.Parameters.Count() == 0)
                        {
                            args.Result = (context.TargetSquare != null && context.TargetUnit != null) ? 1.0 : 0.0;
                        }
                        else
                        {
                            args.Result = 0.0;
                        }
                        break;
                    
                    default:
                        break;
                }
            };

            // 4. 파라미터 매핑
            MapContextToParameters(e, context); 
            
            try
            {
                // 5. 계산 실행
                return Convert.ToDouble(e.Evaluate());
            }
            catch
            {
                return 0.0;
            }
        }

        // 이 메서드가 모든 변수 매핑을 담당
        private void MapContextToParameters(Expression e, BattleContext context)
        {
            // 모든 StatType Enum을 순회하며 매핑을 자동화합니다.
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                string statCode = GetStatCode(statType);
                // O (Origin) 매핑
                if (context.OriginUnit != null)
                {
                    e.Parameters[$"O.{statCode}"] = context.OriginUnit.ParseStat(statType);
                    e.Parameters[$"O.O{statCode}"] = context.OriginUnit.ParseStat(statType, isOrigin: true);
                }

                // T (Target) 매핑
                if (context.TargetUnit != null)
                {
                    e.Parameters[$"T.{statCode}"] = context.TargetUnit.ParseStat(statType);
                    e.Parameters[$"T.O{statCode}"] = context.TargetUnit.ParseStat(statType, isOrigin: true);
                }
            }
            
            // (맵 변수 등 기타 항목이 있다면 여기에 추가)
            // e.g., e.Parameters["MAP.TURN"] = context.BattleState.TurnCount; 
        }
            
        private string GetStatCode(StatType statType)
        {
            return StatCodeMapper.GetCode(statType);
        }
    }
}