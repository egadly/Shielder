using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUI : FrameLimiter {

  protected Character _character;
  protected Vector3 _scale = Vector3.one;
  protected Vector3 _position = Vector3.one;
  protected Color _color;

	// Use this for initialization
	void Start () {
    _character = GetComponentInParent<Character>();
    _scale = transform.localScale;
    _position = transform.localPosition;
		
	}
	
	// Update is called once per frame
	protected override void UpdatePlus() {
		_scale.x = _character.stamina/100f;
    _position.x = (_scale.x - 1f)/2f;
    if ( _character.stamina >= 100 ) _color = Color.cyan;
    else _color = new Color( 0, 0, 0.75f, 1f );
    
    transform.localScale = _scale;
    transform.localPosition = _position;
    GetComponent<SpriteRenderer>().color = _color;
	}
}
