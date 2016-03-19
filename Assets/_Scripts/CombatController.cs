using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {

	bool alive;

	public float maxHealth;

	StatsController.StatsObject baseStats;

	// Use this for initialization
	void Awake () {
		alive = true;
		baseStats = StatsController.GetBaseStats(this.name);
		maxHealth = baseStats.DefendStats.health;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsAlive()
	{
		return alive;
	}

	public StatsController.StatsObject GetBaseStats()
	{
		return baseStats;
	}
		
}
