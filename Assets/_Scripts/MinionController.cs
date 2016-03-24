using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// TO DO- Remove old range sphere collirders from the objects!!
/// Should the die deleteObject() be moved into the combat controller rather than the minion controller????
/// </summary>
public class MinionController : MonoBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    Animation anim;
	//Dunno why but this was giving a warning about hiding something if not using new.
	SphereCollider aggroCollider;
    Rigidbody rb;
	CombatController myCombatController;
	StatsController.StatsObject baseStats;
	LayerMask layerMask;
    public bool targetLocked;
	float nextAttack,
	aggroRange;

	void Awake () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
		myCombatController = GetComponent<CombatController>();
		aggroCollider = GetComponentInChildren<SphereCollider>();
		nextAttack = 0.0f;
		aggroRange = 9f;

		agent.updateRotation = true;
		agent.avoidancePriority = Random.Range(1,99);
	}

	void Start()
	{
		baseStats = myCombatController.GetBaseStats();
		anim.Play("run");
		layerMask = 1 << GameController.attackableLayer;
		//target = GameObject.Find(GameController.getOpposingTeam (this.tag) + "Nexus");
		agent.SetDestination(GameObject.Find(GameController.getOpposingTeam (this.tag) + "Nexus").transform.position);
	}


    void LateUpdate()
	{
		DecideAction();

    }

	void DecideAction()
	{
		if (target == null) 
		{
			if (ScanForTargets ())
				agent.SetDestination (target.transform.position);
			if(agent.pathStatus == NavMeshPathStatus.PathComplete)
				agent.SetDestination(GameObject.Find(GameController.getOpposingTeam (this.tag) + "Nexus").transform.position);

			anim.Play ("run");

		}
		else
		{
			this.transform.LookAt (target.transform);
			agent.SetDestination (target.transform.position);
			if (agent.remainingDistance < 1.5f) 
			{
				Attack ();
			}
		}


	}

	bool ScanForTargets()
	{
		Collider[] colliders = Physics.OverlapSphere (this.transform.position, aggroRange, layerMask, QueryTriggerInteraction.Ignore);
		if (colliders.Length > 0) {
			List<Collider> potentialTargets = colliders.OfType<Collider> ().ToList<Collider> ();
			//order by the closest
			potentialTargets = potentialTargets.OrderBy (x => Vector3.Distance (this.transform.position, x.transform.position)).ToList ();
			foreach (Collider potentialTarget in potentialTargets) 
			{
				//ignore teammates
				if (this.tag != potentialTarget.gameObject.tag) 
				{
					target = potentialTarget.gameObject;
					return true;
				}				
			}
		}
		return false;
	}
		

	void Update()
	{
		if (myCombatController.IsAlive() == false) 
		{
			Destroy (this.gameObject);
		}
	}
		
	public void Attack()
	{
		var currentTime = Time.time;
		if (Time.time > nextAttack) {
			nextAttack = Time.time + baseStats.AttackStats.attackSpeed;
			anim.Play ("attack");
			target.GetComponent<CombatController> ().ReceiveDamage (GameController.DamageType.Physical, 50/*This should be attack damage... duh*/);
		}
	}
		

}
