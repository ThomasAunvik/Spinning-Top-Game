using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

    float xSeed;
    float zSeed;

    public float intensitevity;

	void Start () {
        xSeed = Random.Range(0, 10000);
        zSeed = Random.Range(0, 10000);
    }
	
	void Update () {
        transform.position = new Vector3(Mathf.Sin(Time.timeSinceLevelLoad + xSeed) * intensitevity, -10, (Mathf.Sin(Time.timeSinceLevelLoad + zSeed)));
        transform.parent.Rotate(new Vector3(0,1,0) * Time.timeScale);
	}
}
