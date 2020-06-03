using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Przecinwik atakujacy z ma³ego zajêgu
/// </summary>
///<inheritdoc cref="Enemy"/>
public class Bat : Enemy
{
    [SerializeField] private float velocity;
    [SerializeField] private float atackDistance;
    
    new void Start()
    {
        base.Start();//invoke anbstract class method
        destroyParticles.GetComponent<ParticleSystem>().Play();

    }
    /// <summary>
    /// Sprawdzenie czy œledzony obiekt znajduje siê w zasiêgu, jeœli tak to poruszaj siê w jego stronê
    /// </summary>
    new void Update()
    {
        if (player)
        {
            if (Vector2.Distance(player.transform.position, this.transform.position) <= atackDistance)       
                transform.position = Vector2.MoveTowards(transform.position, player.position, velocity*Time.deltaTime); //move object
        }
    }
    /// <summary>
    /// Logika w przypadku kolizji 
    /// </summary>
    /// <param name="collision"> obiekt koliduj¹cy </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //what happedn when object colide with other object
        if(collision.gameObject.tag=="Player")//by player tag
        {
            collision.gameObject.GetComponent<Player>().Attacked(2);//player takes damage
            
        }
    }
}
