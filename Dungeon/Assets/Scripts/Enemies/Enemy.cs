using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa bazowa dla ka¿dego przecinwika
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected GameObject destroyParticles;
    [SerializeField] protected GameObject []pickupIteam;//drop iteams when enemy dying
    protected Animator _anim;
    protected int dropChance = 40;//chance on iteam drop
    protected Transform player;// player position
    public void Start()
    {
        _anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }
    public void Update()
    {
        if (health <= 0)
            DestroyEnemy(gameObject);
    }
    /// <summary>
    /// Gdy przeciwnik otrzyma³ obra¿enia, uruchamiana jest animacja a je¿eli poziom zdrowia spad³ poni¿ej 0 to obiekt jest niszczony 
    /// </summary>
    /// <param name="damage">ilosc otrzymanych obra¿eñ </param>
    public void Attacked(int damage)
    {
        health -= damage;
        _anim.SetBool("isHurted", true);//set animator values
        if(health <= 0)
        {
            DestroyEnemy(gameObject);
        }
    }
    public void EndHurtAnimation()
    {
        _anim.SetBool("isHurted", false);//set animator value
    }
    /// <summary>
    /// Dodatkowe zdarzenia wystêpuj¹ce podczas niszczenia obiektu 
    /// </summary>
    /// <param name="gameObject"> obiekt który ma zostaæ zniszczony </param>
    public virtual void DestroyEnemy(GameObject gameObject)//action when enemy dying
    {
            PermanentUI.perm.score++;
            Destroy(gameObject);
            DropIteam();
           // Instantiate(destroyParticles, gameObject.transform.position, Quaternion.identity);
    }
    /// <summary>
    /// Funkcja jest mo¿liwa do nadpisania w klasie potomnej, tworzy przedmioty w miejscu zniszecznia obiektu
    /// Pojawienie siê przedmiotu zalezy od parametru dropChance
    /// </summary>
    public virtual void DropIteam()//dropping the item by enemy
    {
        int dropChance = Random.Range(0, 100),cell=Random.Range(0,pickupIteam.Length);

        if(dropChance>60)
        Instantiate(pickupIteam[cell], gameObject.transform.position, Quaternion.identity);
    }
}
