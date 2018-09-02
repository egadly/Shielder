using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class World : FrameLimiter
{

  public static World main = null;

  protected Vector3 _position;

  protected Timer _restartTimer = new Timer(0);
  protected bool _isRestarting = false;

  void Start()
  {
    if (main == null) main = this;
    else Destroy(this);

    _isRestarting = false;
    _position = transform.position;
  }
  
  protected override void UpdateThis() {
    float ratio = Time.deltaTime / FrameLimiter.frameTime;
    float weight = Mathf.Pow( 0.95f, ratio );
    if ( Shielder.main != null ) {
      _position = _position *weight + Shielder.main.transform.position * (1f-weight);
      _position.z = -10f;
      transform.position = _position;
    }

    if ( Input.GetKeyDown( KeyCode.Escape) ) SceneManager.LoadScene( "title" );


  }

  protected override void UpdatePlus() {

    if ( _isRestarting ) {
      if ( _restartTimer ) {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
      } else _restartTimer.Increment();
    }

  }
  public static void Message(string input)
  {
    if ( main != null ) {

    if (input == "shielda")
    {
      Volumizer.toSilence = true;
      main._isRestarting = true;
      main._restartTimer.Set( 5 * 60 );
    }

    if (input == "winna")
    {
      SceneManager.LoadScene("credits");
    }

  }

  }


}
