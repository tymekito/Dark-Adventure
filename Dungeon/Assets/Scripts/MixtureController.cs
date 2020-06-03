using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Logika mikstur czyli przedmiotów zwiększających atrybuty gracza
/// </summary>
public class MixtureController : MonoBehaviour
{
    [SerializeField] float effectStrenght;
    [SerializeField] string mixtureType;
    /// <summary>
    /// Funkcja na podstawie nazwy mikstury wywołuje efekt.
    /// Na początku wczytujemy odpowiednie komponenty, jeżeli nasz gracz istanieje to wywołujemy w zależności od nazwy odpowiednią logikę
    /// </summary>
    public void MixtureEffect()
    {
        Player player_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() as Player;
        Weapon weapon_ = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>() as Weapon;
        if (player_)
        {
            switch (mixtureType.ToLower())
            {
                case "red":
                    if(player_.Heal())
                        Destroy(gameObject);
                    break;
                case "green":
                    weapon_.DecrementShotDelay(effectStrenght);
                    Destroy(gameObject);
                    break;
                case "purple":
                    player_.UpgradeSpeed(effectStrenght);
                    Destroy(gameObject);
                    break;
                case "blue":
                    //TODO: add blue effect
                    break;
                case "yellow":
                    //TODO: add yellow effect
                    break;

                default:
                    break;
            }
        }
    }
}
