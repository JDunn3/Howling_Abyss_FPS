using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    GameObject[] Inhibs;
    GameObject target;
    NavMeshAgent agent;
    Animation anim;
	//Dunno why but this was giving a warning about hiding something if not using new.
	SphereCollider aggroCollider;
    Rigidbody rb;
	CombatController myCombatController;
	StatsController.StatsObject baseStats;
    bool targetAlive;
	float nextAttack;

	void Awake () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
		myCombatController = GetComponent<CombatController>();
        aggroCollider = GetComponent<SphereCollider>();
		nextAttack = 0.0f;


		targetAlive = false;
		agent.updateRotation = true;
		agent.avoidancePriority = Random.Range(1,99);
	}

	void Start()
	{
		baseStats = myCombatController.GetBaseStats();
		anim.Play("run");

	}


    void Update()
    {
//		if (target.GetComponentInParent<CombatController> ().IsAlive () == false)
//			FindNewTarget ();
    }


    void OnTriggerEnter(Collider other)
    {
		if (target != null && !other.isTrigger && other.tag == GameController.getOpposingTeam(this.tag))
        {
			if (Vector3.Distance(this.transform.position, target.transform.position) > aggroCollider.radius)
            {
                target = other.gameObject;
                agent.SetDestination(target.transform.forward + target.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
		if (target != null) 
		{
			if (Vector3.Distance (transform.position, agent.destination) <= 1.1f) {
				//anim.Play ("idle");
				Attack ();
			}
			//Might need optimization here to check if the target is static so we don't waste resources every  frame calculating a new path
			else if (!target.isStatic) {
				agent.SetDestination (target.transform.position + (target.transform.forward) * .5f);
			}
			transform.LookAt(target.transform);
		}
    }

	void LateUpdate()
	{
		if (myCombatController.IsAlive() == false) 
		{
			anim.Play ("jump");
			Destroy (this.gameObject, 1);
		}
	}

	public void ForceTarget(GameObject myTarget)
	{
		target = myTarget;
		targetAlive = myTarget.GetComponent<CombatController>().IsAlive();
		agent.SetDestination(myTarget.transform.position);
	}

	public void Attack()
	{
		var currentTime = Time.time;
		if (Time.time > nextAttack) {
			nextAttack = Time.time + baseStats.AttackStats.attackSpeed;
			anim.Play ("attack");
			target.GetComponentInParent<CombatController> ().ReceiveDamage (GameController.DamageType.Physical, 50/*This should be attack damage... duh*/);
			anim.PlayQueued ("idle");
		}
	}

//	void FindNewTarget()
//	{
//		foreach( Collider collider in Physics.OverlapSphere (this.transform.position, aggroCollider.radius)
//			{
//				if(collider
//			}
//
//	}

}
