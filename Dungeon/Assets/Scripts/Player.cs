using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
/// <summary>
/// Logika postaci gracza 
/// </summary>
public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidBody;
    private Vector2 moveInput;
    private ParticleSystem hurtEffect;
    private bool isDemaged = false;
    private Animator anim;
    private AudioSource audioSrc;
    public AudioClip loseSound;
    private bool Loaded=true;
    /// <summary>
    /// Gdy obiekt jest tworzony przypisujemy jego komponenty do poszczególnych zmiennych
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
        hurtEffect = GetComponent<ParticleSystem>();
        hurtEffect.Stop();
        rigidBody = GetComponent<Rigidbody2D>();
        Loaded = false;
        
    }
    void Update()
    {
        
        Move();   
    }
    /// <summary>
    /// Przesuniêcie postaci o pozyciê w czasie
    /// </summary>
    private void FixedUpdate()
    {
        if (!Loaded)
        {
            PermanentUI.perm.LoadPlayerStats();
            Loaded = true;
        }
        rigidBody.MovePosition(rigidBody.position + moveInput * Time.deltaTime);
    }
    /// <summary>
    /// Logika podczas kolizji, je¿eli jego tag to Enemy to nadaj si³ê obiketowi gracza i go odepchnij
    /// </summary>
    /// <param name="other">obiekt z którym zasz³a kolizja</param>
   private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
           // calculate force vector
           var force =transform.position-other.transform.position;
           // how much the character should be knocked back
           var magnitude=100000;
            // normalize force vector to get direction only and trim magnitude
           force.Normalize();
           gameObject.GetComponent<Rigidbody2D>().AddForce(force*magnitude);
        }
        }

    /// <summary>
    /// Gdy gracz zostaje zatakowany 
    /// </summary>
    /// <param name="damage">iloœæ otrzymanych obra¿eñ </param>
    public void Attacked(int damage)
    {
        audioSrc = GetComponent<AudioSource>();
        AudioSource audio = GetComponent<AudioSource>();
        
        if (!isDemaged)
        {
            PermanentUI.perm.health -= damage;
            hurtEffect.Play();
            audioSrc.Play();
            anim.SetBool("isHurting", true);
            StartCoroutine(IsInsensitive());
            if (PermanentUI.perm.health <= 0)
            {
               anim.SetBool("isDead", true);
                audio.PlayOneShot(loseSound);
               // IsDead();
            }
        }
    }
    /// <summary>
    /// Funkcja u¿ywana jako event w animatorze po wykonaniu animacji wywo³ywana jest funkcja.
    /// </summary>
    public void IsDead()
    {
        SceneManager.LoadScene("Closing Credits");
        Destroy(gameObject);
    }
    /// <summary>
    /// Przesuñ gracza i o vektor oraz ustaw flagê animatora aby wywo³aæ odpowiedni¹ animacjê 
    /// </summary>
    public void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput = input.normalized * speed;
        if (moveInput != new Vector2(0, 0))
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    /// <summary>
    /// Gracz zostaje uleczony a jego pozim ¿yæ siê inkrementuje.
    /// </summary>
    /// <returns>true gdy gracz zosta³ uleczony, false gdy nie dosz³o do uleczenia </returns>
    public bool Heal()//test player health status
    {
        if(PermanentUI.perm.health < 5)
        {
            PermanentUI.perm.health++;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Zwiêkszenie parametru speed gracza
    /// </summary>
    /// <param name="level">wartoœæ o któr¹ zwiêkszona zosta³a prêdkoœæ</param>
    public void UpgradeSpeed(float level)
    {
        this.speed += level;
    }
    /// <summary>
    /// Funkcja ustawia flagê animatora na false co powowduje koniec animacji.
    /// </summary>
    public void EndHurtAnim()
    {
        anim.SetBool("isHurting", false);
    }
    //corutine
    /// <summary>
    /// Korutyna wy³¹cza komponent BoxCollider2D przez co postaæ staje siê niewra¿liwa na kolizjê przez okreslony czas.
    /// Po up³yniêciu czasu komponenty oraz zostaj¹ w³¹czone i gracz znów mo¿e zostaæ zatakowany
    /// </summary>
    /// <returns>czas który zostaje odczekany przez dalsz¹ realizacj¹ cia³a funkcji</returns>
    private IEnumerator IsInsensitive()
    {
        isDemaged = true;
        BoxCollider2D boxColider2D =  gameObject.GetComponent<BoxCollider2D>();
        boxColider2D.enabled=false;
        yield return new WaitForSeconds(1.5f);//set under value by time
        isDemaged = false;
        boxColider2D.enabled = true;
    }

}
