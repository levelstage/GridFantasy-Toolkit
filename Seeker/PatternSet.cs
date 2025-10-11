public class PatternSet
{
	public List<Pattern> Patterns;
	public int Penetration;
	
	public PatternSet(List<Pattern> patterns)
	{
		Patterns = patterns;
		Penetration = 0;
	}

	public PatternSet(List<Pattern> patterns, int penetration)
	{
		Patterns = patterns;
		Penetration = penetration;
	}

}