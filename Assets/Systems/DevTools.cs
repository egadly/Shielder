using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour {

  public static DevTools main = null;
  public bool collisionVisibility = true;
  
  void Start() {
    if ( main == null ) main = this;
    else Destroy( this );
  }

  void Update() {
    Collision.isVisible = collisionVisibility;
  }

}
