using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {

	bool alive;

	public float maxHealth,
	currentHealth;

	StatsController.StatsObject baseStats;

	// Use this for initialization
	void Awake () {
		alive = true;
		baseStats = StatsController.GetBaseStats(this.name);
		maxHealth = baseStats.DefendStats.health;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0)
			alive = false;
	}

	public bool IsAlive()
	{
		return alive;
	}

	public StatsController.StatsObject GetBaseStats()
	{
		return baseStats;
	}

	public bool ReceiveDamage(GameController.DamageType damageType,  float amount)
	{
		float actDmgDealt = 0;
		switch(damageType)
		{
		case GameController.DamageType.Physical:
			actDmgDealt = amount - baseStats.DefendStats.armor;
			currentHealth -= actDmgDealt;
			break;
		case GameController.DamageType.Magic:
			break;
		case GameController.DamageType.True:
			break;
		}
		return actDmgDealt;
	}
		
}
