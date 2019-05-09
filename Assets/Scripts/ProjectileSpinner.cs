using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpinner : MonoBehaviour {
    float spinSpeed = 720f;
	
	// Update is called once per frame
	void Update () {
        var rot = spinSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rot);
    }
}
