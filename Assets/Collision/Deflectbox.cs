using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflectbox : Collision {

    public Vector3 direction = Vector3.zero;

    protected override void CollisionStart() {
      _color = Color.cyan;
      _results = new Collider2D[ 100 ];
    }

    protected override void CollisionUpdate() {

    int limit = CheckCollisions( "Hitbox" );
    for ( int i = 0; i < limit; i++ ) {
      _results[i].GetComponent<Hitbox>().Deflect( direction );
    }

  }
}
