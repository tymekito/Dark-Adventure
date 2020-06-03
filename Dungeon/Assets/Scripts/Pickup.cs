
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Logika przedmiot�w kt�re mog� by� zebrane przez gracza
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
    /// W zale�no�ci od tagu jaki posiada GameObject wywo�ywana jest odpowiednia logika dla danego przedmiotu
    /// </summary>
    /// <param name="collision"> obiekt koliduj�cy</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player _player = collision.gameObject.GetComponent<Player>();
        //je�eli dosz�o do kolizji z graczem
        //w zale�no��i od tagu wywo�aj zdarzenie
        if (collision.tag == "Player")
        {
            
            switch (gameObject.tag)
            {
            case "Collectable Ammo":
           
            break;
            case "Collectable Health":
            if (_player.Heal())//je�eli �ycie gracza mniejsze od maximum
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
