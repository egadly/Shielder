using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : FrameLimiter
{

  public static SoundEffects main = null;
  public const int max = 20;
  AudioSource[] _sources = new AudioSource[max];
  protected int _currentIndex;

  public AudioClip hit;

  public AudioClip deflect;
  public AudioClip reflect;
  public AudioClip death;
  public AudioClip mage;
  public AudioClip fire;
  public AudioClip skull;
  public AudioClip rush;
  public AudioClip shield;
  public AudioClip unshield;

  void Start()
  {

    if (main == null) main = this;
    else Destroy(this);

    for (int i = 0; i < max; i++)
    {
      _sources[i] = gameObject.AddComponent<AudioSource>();
    }
    _currentIndex = 0;
  }

  protected void Play(AudioClip sound, float volume)
  {
    _sources[_currentIndex].clip = sound;
    _sources[_currentIndex].volume = volume;
    _sources[_currentIndex].Play();
    _currentIndex = (_currentIndex + 1) % max;
  }

  protected void Play(AudioClip sound, float volume, int index)
  {
    _sources[index].clip = sound;
    _sources[index].volume = volume;
    _sources[index].Play();
    _currentIndex = (index + 1) % max;
  }

  public static void PlayHit()
  {
    if (main != null) main.Play(main.hit, 1f, 0);
  }
  public static void PlayDeath() {
    if(main != null) main.Play( main.death, 0.5f, 0);
  }
  public static void PlayDeflect()
  {
    if (main != null) main.Play(main.deflect, 0.25f, 1);
  }
  public static void PlayReflect()
  {
    if (main != null) main.Play(main.reflect, 0.25f, 1);
  }
  public static void PlayShield( float volume = 1.0f )
  {
    if (main != null) main.Play(main.shield, volume * 0.25f, 1);
  }
  public static void PlayUnshield()
  {
    if (main != null) main.Play(main.unshield, 0.25f, 1);
  }

  public static void PlayMage()
  {
    if (main != null) main.Play(main.mage, 0.1f, 10);
  }
  public static void PlayFire()
  {
    if (main != null) main.Play(main.fire, 0.1f, 10);
  }
  public static void PlaySkull()
  {
    if (main != null) main.Play(main.skull, 0.1f, 11);
  }
  public static void PlayRush()
  {
    if (main != null) main.Play(main.rush, 0.1f, 12);
  }

}
