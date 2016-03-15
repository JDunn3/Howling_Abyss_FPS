using UnityEngine;
using System.Collections;

public class MeleeMinionController : MonoBehaviour {

    public float range;

    GameObject[] Inhibs;
    Transform target;
    NavMeshAgent agent;
    Animation anim;
    string team;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        team = this.tag;
        target = GameObject.Find(GameController.getOpposingTeam(team) + "Nexus").transform;

        agent.SetDestination(target.position);
        anim.Play("run");
	}

    void Update()
    {

    }

    void FixedUpdate()
    {
        this.transform.LookAt(target);
    }

}
