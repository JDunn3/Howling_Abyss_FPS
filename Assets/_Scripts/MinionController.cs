using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    GameObject[] Inhibs;
    public GameObject target;
    NavMeshAgent agent;
    Animation anim;
	//Dunno why but this was giving a warning about hiding something if not using new.
    SphereCollider collider;
    Rigidbody rb;
    bool targetAlive;

	void Awake () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
		targetAlive = false;
		agent.updateRotation = true;
        
	}

	void Start()
	{
		anim.Play("run");
	}


    void Update()
    {
		
//		if (!targetAlive && target == null)
//        {
//            target = GameObject.Find(GameController.getOpposingTeam(this.tag) + "Nexus");
//			targetAlive = target.GetComponent<AttackableController> ().alive;
//            agent.updateRotation = true;
//            agent.SetDestination(target.transform.position);
//        }
		if (target != null) 
		{
			if (Vector3.Distance (transform.position, agent.destination) <= 1.2f) {
				anim.Play ("idle");
			}
        //Might need optimization here to check if the target is static so we don't waste resources every  frame calculating a new path
        else if (!target.isStatic)
				agent.destination = target.transform.position + (target.transform.forward * 0.5f);
		}

    }


    void OnTriggerEnter(Collider other)
    {
		if (target != null && !other.isTrigger && other.tag == GameController.getOpposingTeam(this.tag))
        {
			if (Vector3.Distance(this.transform.position, target.transform.position) > collider.radius)
            {
                target = other.gameObject;
                agent.SetDestination(target.transform.forward + target.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }

	public void ForceTarget(GameObject myTarget)
	{
		target = myTarget;
		targetAlive = myTarget.GetComponent<AttackableController>().alive;
		agent.SetDestination(myTarget.transform.position);
	}

}
