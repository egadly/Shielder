using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

  protected Vector3 _position;

	// Use this for initialization
	void Start () {

    Cursor.visible = false;
		
	}
	
	// Update is called once per frame
	void Update () {

    _position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
    _position.z = 0;
    transform.position = _position;
		
	}
}
