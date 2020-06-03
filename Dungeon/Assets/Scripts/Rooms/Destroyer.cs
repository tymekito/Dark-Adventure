using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gdy zajdzie kolizja pomiędzy dwoma obiektami o tagu SpawnPoint niszczony jest ten kolidujący 
/// </summary>
public class Destroyer : MonoBehaviour
{
    // action what happend when we want destroy spawn point
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if spawn point collide with another spawn point destroy another spawn point
        if (collision.CompareTag("SpawnPoint"))
            Destroy(collision.gameObject); 
    }
}
