using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsController : MonoBehaviour {

	static float minionAttackSpeed = 3.0f,
			towerAttackSpeed = 1.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public class StatsObject
	{
		public OffensiveStatsObject AttackStats;
		public DefensiveStatsObject DefendStats;
		public OtherStatsObject OtherStats;

		public class OffensiveStatsObject
		{
			public float attackSpeed;

			public OffensiveStatsObject(float AttackSpeed)
			{
				
				this.attackSpeed = AttackSpeed;
			}
		}

		public class DefensiveStatsObject
		{
			public float health,
				armor,
				magicResist;

			public DefensiveStatsObject(float Health, float Armor, float MagicResist)
			{
				
				this.health = Health;
				this.armor = Armor;
				this.magicResist = MagicResist;
			}
		}

		public class OtherStatsObject
		{
			public OtherStatsObject()
			{

			}
		}

		public StatsObject(float MaxHealth, float AttackSpeed, float Armor, float MagicResist)
		{
			AttackStats = new OffensiveStatsObject(AttackSpeed);
			DefendStats = new DefensiveStatsObject(MaxHealth, Armor, MagicResist);
			OtherStats = new OtherStatsObject();
		}
	}

	static Dictionary<string,StatsObject> baseStatsDict = new Dictionary<string, StatsObject>() {
		{ "MeleeMinion", 		new StatsObject (100.0f, minionAttackSpeed, 2.0f, 10.0f) },
		{ "Nexus", 				new StatsObject (1000.0f, 0.0f, 2.0f, 10.0f) },
		{ "Tower_Outer", 		new StatsObject (1000.0f, towerAttackSpeed, 2.0f, 10.0f) },
		{ "Tower_Inhib", 		new StatsObject (1000.0f, towerAttackSpeed, 2.0f, 10.0f) },
		{ "Tower_NexusNorth", 	new StatsObject (1000.0f, towerAttackSpeed, 2.0f, 10.0f) },
		{ "Tower_NexusSouth", 	new StatsObject (1000.0f, towerAttackSpeed, 2.0f, 10.0f) }
	};

	public static StatsObject GetBaseStats(string identifier)
	{
		identifier = identifier.Replace ("(Clone)", "");
		identifier = identifier.Replace ("Red", "");
		identifier = identifier.Replace ("Blue", "");

		return baseStatsDict[identifier];
	}
		
}
