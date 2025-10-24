using GfEngine.Battles;
namespace GfEngine.Logics.Parsing
{
    public interface IFormulaParser
    {
        double Evaluate(string formula, BattleContext context);
    }
}