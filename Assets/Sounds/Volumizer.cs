using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volumizer : FrameLimiter {

  public static bool toSilence;

  void Start() {
    toSilence = false;
  }

	
	// Update is called once per frame
	protected override void UpdatePlus() {
    if ( toSilence ) {
      GetComponent<AudioSource>().volume *= 0.95f;
    }
	}

}
