namespace GfEngine.Battles.Parsing
{
    public interface IBattleFormulaParser
    {
        double Evaluate(string formula, BattleContext context);
    }
}