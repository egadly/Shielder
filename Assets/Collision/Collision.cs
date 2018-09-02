using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : FrameLimiter {

  public static bool isVisible;

  protected Color _color = Color.clear;
  protected SpriteRenderer _renderer;

  protected Collider2D _collider;
  public new Collider2D collider {
    get { return _collider; }
  }
  protected Collider2D[] _results = new Collider2D[1];
  protected static ContactFilter2D _filter = new ContactFilter2D();

  void Start() {
    _renderer = GetComponent<SpriteRenderer>();
    _collider = GetComponent<Collider2D>();
    CollisionStart();
    _renderer.color = _color;
  }

  protected override void UpdatePlus() {
    GetComponent<Rigidbody2D>().WakeUp();
    CollisionUpdate();
    _renderer.enabled = Collision.isVisible;
  }

  public int CheckCollisions( string layer ) {
    for ( int i = 0; i < _results.Length; i++ ) {
      _results[i] = null;
    }
    _filter.layerMask.value = 1 << LayerMask.NameToLayer( layer );
    _filter.useLayerMask = true;
    int resultsNumber = Physics2D.OverlapCollider( _collider, _filter, _results );
    return resultsNumber;
  }

  protected virtual void CollisionStart() {  }
  protected virtual void CollisionUpdate() {  }

}
