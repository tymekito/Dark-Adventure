
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Logika przedmiotów które mog¹ byæ zebrane przez gracza
/// </summary>
public class Pickup : MonoBehaviour
{
    ParticleSystem particle;
    AudioSource audioo;
    [SerializeField] private AudioClip pickUpSound;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        audioo = GetComponent<AudioSource>();
        particle.Stop();
    }
    /// <summary>
    /// Kolizja przedmiotu z innym  obiektem.
    /// W zale¿noœci od tagu jaki posiada GameObject wywo³ywana jest odpowiednia logika dla danego przedmiotu
    /// </summary>
    /// <param name="collision"> obiekt koliduj¹cy</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player _player = collision.gameObject.GetComponent<Player>();
        //je¿eli dosz³o do kolizji z graczem
        //w zale¿noœæi od tagu wywo³aj zdarzenie
        if (collision.tag == "Player")
        {
            
            switch (gameObject.tag)
            {
            case "Collectable Ammo":
           
            break;
            case "Collectable Health":
            if (_player.Heal())//je¿eli ¿ycie gracza mniejsze od maximum
            {
                        audioo.PlayOneShot(pickUpSound);
                        particle.Play();
                        Destroy(this.gameObject);

            }
            break;
            case "Mixture":
                    audioo.PlayOneShot(pickUpSound);
                    gameObject.GetComponent<MixtureController>().MixtureEffect();
                    
                    break;
            case "Keys":
                    if (PermanentUI.perm.greenKey == true)
                        PermanentUI.perm.redKey = true;
                    else
                    PermanentUI.perm.greenKey = true;
                    Destroy(gameObject);
            break;
                default:
            break;
            }
        }
      
    }
}
