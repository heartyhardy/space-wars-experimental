using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float scrollSpeed = 0.03f;

    Material scrollableMaterial;
    Vector2 scrollOffset;
	// Use this for initialization
	void Start () {

        scrollableMaterial = GetComponent<Renderer>().material;
        scrollOffset = new Vector2(0f, scrollSpeed);

	}
	
	// Update is called once per frame
	void Update () {
        scrollableMaterial.mainTextureOffset += scrollOffset * Time.deltaTime;
    }
}
