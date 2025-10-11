public class Unit : Actor
{
	
	// Actor와 공유하지 않는 속성
	public LiveStatus LiveStat { get; set; }
	public Actor ActorData {get; set;}
	public Teams Team { get; set; }
	public List<Behavior> Behaviors { get; set; }
	
	// 가장 major한 Unit의 생성자. 주로 플레이어 캐릭터나 적 정예 캐릭터가 판 위에 올라올 때 사용.
	public Unit(Actor actorData, Teams team) : base(actorData)
	{
		ActorData = actorData;
		this.LiveStat = new LiveStatus(actorData.Stat, actorData.MoveClass, actorData.WeaponClass);
		this.Team = team;
		
		// 기본 Behaviors 추가.
		this.Behaviors = new List<Behavior>();
		this.Behaviors.Add(new BasicMoveBehavior(actorData.MoveClass));
		if(this.Equipment != null)this.Behaviors.Add(new BasicAttackBehavior(this.Equipment));
	}
	
	// 해당 웨이브에만 등장하는 임시 유닛 생성용 생성자.
	public Unit(MoveType moveType, Weapon weapon, Status status, Teams team, string name)
    {
		this.Name = name;
        this.MoveClass = moveType;
        this.WeaponClass = weapon.Type;
		this.Equipment = new Weapon(weapon);
		this.LiveStat = new LiveStatus(status, moveType, weapon.Type);
		this.Behaviors = new List<Behavior>();
		if(moveType != MoveType.Stationary)this.Behaviors.Add(new BasicMoveBehavior(moveType));
		if(weapon != null)this.Behaviors.Add(new BasicAttackBehavior(this.Equipment));
		this.Team = team;
    }
	
	public void Update()
	{
		
	}

	public void TurnStart()
	{
		
	}

	public void TurnOver()
	{
		
	}
	
	// 피해를 받는 함수.
	public int TakeDamage(int damage, DamageType damage_type)
	{	
		LiveStatus livestat = this.LiveStat;
		int finalDamage = damage;
		float residence = 1;
		switch(damage_type)
		{
			case DamageType.Physical:
				finalDamage -= livestat.Buffed().Defense;
				residence = 1 - Buff.netBuffMagnitude(BuffType.PhysicalResidence, livestat.Buffs)/100;
				break;
			case DamageType.Magical:
				finalDamage -= livestat.Buffed().MagicDefense;
				residence = 1 - Buff.netBuffMagnitude(BuffType.MagicalResidence, livestat.Buffs)/100;
				break;
			
		}
		if (finalDamage < 0) finalDamage = 0;
		finalDamage = (int)(finalDamage * residence);
		return livestat.ChangeHp(-finalDamage);
	}

    public override string ToString()
    {
    // 이동 타입의 첫 글자를 반환.
		string initial = this.MoveClass.ToString().Substring(0, 1);
		if (this.MoveClass == MoveType.Knight) initial = "N";
		if (this.Team != Teams.Players) initial = initial.ToLower();
    return initial;
    }
}
