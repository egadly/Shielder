using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : Collision {

  Character _character;
  protected Hitbox[] _hitboxList;
  
  protected override void CollisionStart() {
    _color = Color.green;
    _character = GetComponentInParent<Character>();
    _hitboxList = transform.parent.GetComponentsInChildren<Hitbox>();
  }

  protected override void CollisionUpdate() {

    foreach ( Hitbox hitbox in _hitboxList ) {
      hitbox.gameObject.layer = LayerMask.NameToLayer( "Ignore" );
    }

    CheckCollisions( "Hitbox" );
    if ( _results[0] != null ) {
      Hitbox hit = _results[0].gameObject.GetComponent<Hitbox>();
      _character.Damage( this, hit );
    }

    for ( int i = 0; i < _hitboxList.Length; i++ ) { 
      _hitboxList[i].gameObject.layer = LayerMask.NameToLayer( "Hitbox" );
    }

  }

}
