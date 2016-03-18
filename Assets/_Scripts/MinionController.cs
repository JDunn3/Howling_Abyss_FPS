using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    GameObject[] Inhibs;
    GameObject target;
    NavMeshAgent agent;
    Animation anim;
    Collider collider;
    Rigidbody rb;
    string team;
    bool targetLocked;
	
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        team = this.tag;
        target = GameObject.Find(GameController.getOpposingTeam(team) + "Nexus");
        targetLocked = false;
        agent.updateRotation = true;
        agent.SetDestination(target.transform.position);
        anim.Play("run");
	}


    void Update()
    {
        if (target == null && targetLocked == true)
        {
            target = GameObject.Find(GameController.getOpposingTeam(team) + "Nexus");
            targetLocked = false;
        }

        if (Vector3.Distance(transform.position, agent.destination) <= 1.2f)
        {
            anim.Play("idle");
        }
        //Might need optimization here to check if the target is static so we don't waste resources every  frame calculating a new path
        else if(!target.isStatic)
            agent.destination = target.transform.position + (target.transform.forward * 0.5f);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger == false && other.tag == GameController.getOpposingTeam(team))
        {
            if (!targetLocked)
            {
                target = other.gameObject;
                agent.SetDestination(target.transform.forward + target.transform.position);
                targetLocked = true;
            }
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }

}
