using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsController : MonoBehaviour {

	static float minionAttackSpeed = 1.0f,
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
				
				this.attackSpeed =AttackSpeed;
			}
		}

		public class DefensiveStatsObject
		{
			public float health;

			public DefensiveStatsObject(float Health)
			{
				
				this.health = Health;
			}
		}

		public class OtherStatsObject
		{
			public OtherStatsObject()
			{

			}
		}

		public StatsObject(float MaxHealth, float AttackSpeed)
		{
			AttackStats = new OffensiveStatsObject(MaxHealth);
			DefendStats = new DefensiveStatsObject(AttackSpeed);
			OtherStats = new OtherStatsObject();
		}
	}

	static Dictionary<string,StatsObject> baseStatsDict = new Dictionary<string, StatsObject>() {
		{ "MeleeMinion", 		new StatsObject (100.0f, minionAttackSpeed) },
		{ "Nexus", 				new StatsObject (1000.0f, 0.0f) },
		{ "Tower_Outer", 		new StatsObject (1000.0f, towerAttackSpeed) },
		{ "Tower_Inhib", 		new StatsObject (1000.0f, towerAttackSpeed) },
		{ "Tower_NexusNorth", 	new StatsObject (1000.0f, towerAttackSpeed) },
		{ "Tower_NexusSouth", 	new StatsObject (1000.0f, towerAttackSpeed) }
	};

	public static StatsObject GetBaseStats(string identifier)
	{
		identifier = identifier.Replace ("(Clone)", "");
		identifier = identifier.Replace ("Red", "");
		identifier = identifier.Replace ("Blue", "");

		return baseStatsDict[identifier];
	}
		
}
