using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// Implementacja logiki dzwięku, funkcja operuje nA komponencie AudioMixer,  w zależności od ponadego argumentu ustawia próg dzwięku
/// </summary>
public class SoundController : MonoBehaviour
{
  public AudioMixer audioMixer;

  public void SetVolume(float volume)
  {   
      audioMixer.SetFloat("volume",volume);
  }
}
