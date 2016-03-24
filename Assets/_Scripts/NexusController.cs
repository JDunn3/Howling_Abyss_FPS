using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NexusController : MonoBehaviour {


    public int startDelay;
    public GameObject meleeMinion;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnMinionWaves());
	}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    IEnumerator SpawnMinionWaves()
    {
        yield return new WaitForSeconds(startDelay);
        //Spawn the melee minions
        for (int x = 0; x < 6; x++)
        {
            GameObject myMeleeMinion = Instantiate(meleeMinion, this.transform.GetChild(0).position, this.transform.GetChild(0).rotation) as GameObject;
            AssignTeam(myMeleeMinion);
            yield return new WaitForSeconds(1);
        }
    }

    void AssignTeam(GameObject teamAssignableGameObject)
    {
		Color color;
		if (this.tag == "Red")
			color = teamAssignableGameObject.GetComponentInChildren<Renderer> ().material.GetColor ("_Color") + new Color (1f, 0f, 0f);
        else
			color = teamAssignableGameObject.GetComponentInChildren<Renderer> ().material.GetColor ("_Color") + new Color (0f, 0f, 1f);

		teamAssignableGameObject.tag = this.tag;
		foreach(Transform child in teamAssignableGameObject.transform)
			child.gameObject.tag = this.tag;
		//teamAssignableGameObject.GetComponent<MinionController>().ForceTarget(GameObject.Find(GameController.getOpposingTeam(this.tag) + "Nexus"));
		teamAssignableGameObject.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
    }
}
