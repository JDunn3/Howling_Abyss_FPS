using UnityEngine;
using System.Collections;

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
        for (int x = 0; x < 3; x++)
        {
            Instantiate(meleeMinion, this.transform.GetChild(0).position, this.transform.GetChild(0).rotation);
            yield return new WaitForSeconds(1);
        }
    }
}
