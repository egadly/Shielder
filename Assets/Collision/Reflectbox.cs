using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflectbox : Collision {

    protected override void CollisionStart() {
      _color = Color.magenta;
      _results = new Collider2D[ 100 ];
    }

    protected override void CollisionUpdate() {

    int limit = CheckCollisions( "Hitbox" );
    for ( int i = 0; i < limit; i++ ) {
      _results[i].GetComponent<Hitbox>().Reflect();
    }

  }

}
