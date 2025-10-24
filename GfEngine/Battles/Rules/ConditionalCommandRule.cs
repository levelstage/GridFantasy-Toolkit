using GfEngine.Battles.Commands;
using GfEngine.Battles.Conditions;

namespace GfEngine.Battles.Rules
{
    public class ConditionalCommandRule
    {
        public ICondition Condition { get; set; }
        public Command CommandToExecute { get; set; }
    }
}