namespace GfEngine.Models.Buffs.Modifiers
{
	public class ScalingModifier : Modifier
	{
		public string Fomula { get; set; } // 이 버프의 공식
		public ScalingModifier() { }
		public ScalingModifier(ScalingModifier parent) : base(parent)
		{
			Fomula = parent.Fomula;
		}
        public override Modifier Clone()
        {
			return new ScalingModifier(this);
        }
	}
}