using GfEngine.Core;
using GfEngine.Core.Conditions;
using GfEngine.Battles.Parsing;
using GfToolkit.Shared;
using System;

namespace GfEngine.Battles.Conditions
{
    /// <summary>
    /// 두 개의 수식(Formula) 결과를 비교하는 조건 클래스입니다.
    /// 예: LeftFormula ("O.ATK * 2") > RightFormula ("T.DEF")
    /// </summary>
    public class BattleComparingCondition : ICondition
    {
        // 1. 데이터 필드
        public string LeftFormula { get; set; }
        public string RightFormula { get; set; }
        public ComparisonOperator Operator { get; set; }

        // 2. 의존성 필드: IFormulaParser를 readonly로 주입받아 사용
        private readonly IBattleFormulaParser _parser; 

        // 3. 생성자: 외부에서 IFormulaParser를 주입받습니다.
        public BattleComparingCondition(IBattleFormulaParser parser, string leftFormula, string rightFormula, ComparisonOperator op)
        {
            // 의존성 주입 시 null 방어
            _parser = parser ?? throw new ArgumentNullException(nameof(parser), "IFormulaParser는 반드시 주입되어야 합니다.");
            LeftFormula = leftFormula;
            RightFormula = rightFormula;
            Operator = op;
        }
        // 4. Condition으로서 기능하는 메인 로직.
        public bool IsMet(IContext context)
        {
            if(context is BattleContext battleContext)
            {
                // 1. double 값 계산
                double rawLeftValue = _parser.Evaluate(LeftFormula, battleContext);
                double rawRightValue = _parser.Evaluate(RightFormula, battleContext);

                // 2. [핵심 수정] 비교 전에 양변을 정수(int)로 변환
                // Math.Floor를 사용하여 소수점 이하를 내림 처리합니다.
                int leftValue = (int)Math.Round(rawLeftValue, MidpointRounding.AwayFromZero);
                int rightValue = (int)Math.Round(rawRightValue, MidpointRounding.AwayFromZero);

                // 3. 정수 비교 (정확성 보장!)
                switch (Operator)
                {
                    case ComparisonOperator.GreaterThan:
                        return leftValue > rightValue;
                    case ComparisonOperator.GreaterThanOrEqual:
                        return leftValue >= rightValue;
                    case ComparisonOperator.LessThan:
                        return leftValue < rightValue;
                    case ComparisonOperator.LessThanOrEqual:
                        return leftValue <= rightValue;
                    case ComparisonOperator.Equal:
                        return leftValue == rightValue; // 이제 안전하게 사용 가능
                    case ComparisonOperator.NotEqual:
                        return leftValue != rightValue;
                    default:
                        return false;
                }
            }
            return false; // BattleContext가 아니라면, 그냥 false반환.
        }
    }
}