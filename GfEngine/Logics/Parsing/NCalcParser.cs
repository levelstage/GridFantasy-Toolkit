using NCalc;
using GfEngine.Battles;
using System;
using GfToolkit.Shared;

namespace GfEngine.Logics.Parsing
{
    // NCalc를 "래핑(Wrapping)"하는 클래스
    public class NCalcParser : IFormulaParser
    {
        public double Evaluate(string formula, BattleContext context)
        {
            Expression e = new Expression(formula);

            // [핵심] BattleContext의 데이터를 NCalc 파라미터로
            // 변환해주는 헬퍼 메서드를 호출
            MapContextToParameters(e, context);  
            try
            {
                return Convert.ToDouble(e.Evaluate());
            }
            catch (Exception ex)
            {
                // 포뮬러에 문법 오류가 있을 때 크래시 대신 0 또는 기본값 반환
                Console.WriteLine($"Formula Error: {ex.Message} in '{formula}'");
                return 0.0;
            }
        }

        // 이 메서드가 모든 변수 매핑을 담당
        private void MapContextToParameters(Expression e, BattleContext context)
        {
            // 모든 StatType Enum을 순회하며 매핑을 자동화합니다.
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                // StatType에서 해당 스탯의 문자열 코드(예: MaxHp -> MHP, CurrentHp -> HP)를
                // 얻어오는 헬퍼 메서드가 필요합니다. (예: GetStatCode(StatType.MaxHp) -> "MHP")
                string statCode = GetStatCode(statType); 

                // O (Origin/Caster) 매핑
                if (context.OriginUnit != null)
                {
                    // 최종 스탯 (버프 적용): O.MHP, O.ATK, O.HP 등
                    e.Parameters[$"O.{statCode}"] = context.OriginUnit.ParseStat(statType); 
                    
                    // 원본 스탯: O.OMHP, O.OATK 등
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