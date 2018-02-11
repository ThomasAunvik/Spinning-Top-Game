using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

    float xSeed;
    float zSeed;

    public float intensitevity;

    public Vector3 offset;

	void Start () {
        xSeed = Random.Range(0, 10000);
        zSeed = Random.Range(0, 10000);


    }
	
	void Update () {
        transform.position = new Vector3((Mathf.Sin(Time.timeSinceLevelLoad + xSeed) * intensitevity) + offset.x, offset.y, (Mathf.Sin(Time.timeSinceLevelLoad + zSeed) * intensitevity) + offset.z);
        transform.parent.Rotate(new Vector3(0,1,0) * Time.timeScale);
	}
}
