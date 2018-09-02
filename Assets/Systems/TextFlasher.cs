using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFlasher : MonoBehaviour {

  protected float _time = 0;
  protected TextMesh text;

  void Start() {
    text = GetComponent<TextMesh>();
  }

  void Update() {
    _time = (_time + Time.deltaTime)%1;
    text.color = (_time/1f) * Color.black + ( 1f - _time/1f) * Color.white;

    if ( Input.GetKeyDown( KeyCode.Space) ) SceneManager.LoadScene( "level", LoadSceneMode.Single );
    if ( Input.GetKeyDown( KeyCode.Escape) ) Application.Quit();
  }

}
