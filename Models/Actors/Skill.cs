namespace GfEngine.Models.Actor
{
	public class Skill
	{
		public string Name { get; set; }
		public bool IsPassive { get; set; }
		// 스킬을 가지고 있는 것 만으로 받는 버프. (액티브이면서 패시브 스킬이 동시에 딸린 경우까지 고려.)
		public List<BuffSet> SkillBuffs { get; set; }

		public Skill()
		{

		}
		// 스킬은 수정 가능한 객체가 아니므로, 인스턴스화 생성자는 만들지 않았음.
		// 업그레이드 스킬 같은 경우, 스킬이 강화되는게 아니라 아예 다른 스킬을 얻는 매커니즘.
	}
}