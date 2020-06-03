using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
///<inheritdoc cref="Enemy"/>
public class Boss : Enemy
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject startPoint;//point to calculations
    [SerializeField] private GameObject bossKey;
    [SerializeField] private float speed;
    private LineRenderer line;
    private Vector3 direction;
    private bool smashPlayer=false,followPlayer=true;
    private int speedMultipler = 5;
    /// <summary>
    /// znalezienie komponentu o tagu który ma reprezentować zdrowie na UI
    /// </summary>
    new private void Start()
    {
       
        base.Start();
        GameObject.FindGameObjectWithTag("Health Bar")?.SetActive(true);
     
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        //  InvokeRepeating("SmashAbility", 0.3f, 5.0f);//starts invoke method in time every time
        // InvokeRepeating("FlashAbility",1.0f,2.0f);
        //  line=startPoint.GetComponent<LineRenderer>();
    }
    new private void Update()
    {
        
        GameObject.FindGameObjectWithTag("Health Bar").GetComponent<Slider>().value = this.health;
        Move();
    }
    /// <summary>
    /// Logika kolizji 
    /// </summary>
    /// <param name="other">kolidujący komponent</param>
    private void OnCollisionEnter2D(Collision2D other) 
    {
    switch (other.gameObject.tag)
    {
        case"Walls":
        if(smashPlayer)
        {
            speed/= speedMultipler;
            this.GetComponent<Animator>().SetBool("smash", false);
            smashPlayer =false;
            followPlayer=true;
        }
        break;
        case "Doors":
        if (smashPlayer)
        {
             speed /= speedMultipler;
             this.GetComponent<Animator>().SetBool("smash", false);
             smashPlayer = false;
             followPlayer = true;
        }
          break;
        case "Player":
        if(smashPlayer)//if boss is in smash state
        { 
            speed/= speedMultipler;
            other.gameObject.GetComponent<Player>().Attacked(2);
            this.GetComponent<Animator>().SetBool("smash", false);
            smashPlayer=false;
            followPlayer=true;
          //  FlashAbility();
        }
        break;

        case "Enemy":
       
        break;

        default:
        break;
    }    
    }
    /// <summary>
    /// Logika poruszania 
    /// </summary>
    public void Move() 
    {
        
        if(smashPlayer)
        transform.position+=direction*speed*Time.deltaTime;
        if(followPlayer)
        transform.position=Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);
    }
    /// <summary>
    /// Stan w którym obiekt ma podążać za celem
    /// </summary>
    public void Idle()
    {
        followPlayer = true;
        smashPlayer = false;
    }
    /// <summary>
    /// Stan w którym obiekt ma szarżować na cel w zadanym kierunku 
    /// </summary>
    public void SmashAbility()
    {
        if (player)
        {
            direction = (player.position - transform.position).normalized;
            this.GetComponent<Animator>().SetBool("smash", true);
            smashPlayer = true;
            followPlayer = false;
            speed *= speedMultipler;
        }
    
    }
    /// <summary>
    /// Stan w którym obiekt ma strzelać do namierzonego celu 
    /// </summary>
    public void ShootingAbility()
    {
        // line setting
        //  line.SetPosition(0,new Vector3(transform.position.x,transform.position.y,0f));
        //  line.SetPosition(1,new Vector3(player.position.x,player.position.y,0f));
        followPlayer = false;
        smashPlayer = false;
            for (int i = 0; i < 6; i++)
            {
            Vector2 direction = player.position - startPoint.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            startPoint.transform.rotation = rotation;
            Instantiate(ammo, startPoint.transform.position+new Vector3(Random.Range(-10,10),Random.Range(-10,10),0), startPoint.transform.rotation);
        }
    }
    /// <summary>
    /// Korutyna odpowiedzialna za wywoływanie funkcji strzelania okresloną ilość razy o okreslony czas
    /// </summary>
    /// <param name="number"> ilość strzałów </param>
    /// <returns> czas pozostały do zakonczenia </returns>
    IEnumerator RepeatShootingAbility(int number)
    {
        int i = 0;
        while (i < number)
        {
            ShootingAbility();
            // yield return 0;
            yield return new WaitForSeconds(0.5f); //wait 1 second per interval
            i++;
        }
        
    }
    /// <summary>
    /// umiejęstnośc trzelania, wywołunie podaną ilośc razy funkcjię odpowiedzilną za strzelanie
    /// </summary>
    /// <param name="number"> ilośc strałów </param>
    public void StartShootingAbility(int number)
    {
        StartCoroutine(RepeatShootingAbility(number));
    }
    /// <summary>
    /// Pojawienie się w losowej odległości od śledzonego celu 
    /// </summary>
    public void FlashAbility()
    {     
        if (player) 
        {
            transform.position = player.position - new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0.0f);
        }
    }
    /// <summary>
    /// Stworznie po zniszczeniu  przedmiotów.
    /// </summary>
    public override void DropIteam()
    {
        for (int i = 0; i < 5; i++)
            base.DropIteam();

        if (bossKey)
            Instantiate(bossKey, gameObject.transform.position + new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0), Quaternion.identity);
    }
    /// <summary>
    /// Funkcja wywołwyana w momęcie śmierci obiektu 
    /// Gracz otrzymuję punkt i wywoływana jest funkcja niszcząca 
    /// </summary>
    /// <param name="gameObject"></param>
    public override void DestroyEnemy(GameObject gameObject)
    {
        base.DestroyEnemy(gameObject);
        PermanentUI.perm.score += 100;
    }
    /// <summary>
    /// Zablowanie czynności poruszania sie poprzez ustawienie odpowiednich flag
    /// </summary>
    public void StopMoving()
    {
        followPlayer = false;
        this.smashPlayer = false;
    }

}
