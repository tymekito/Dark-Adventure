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
    /// Gdy obiekt jest tworzony przypisujemy jego komponenty do poszczeg�lnych zmiennych
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
    /// Przesuni�cie postaci o pozyci� w czasie
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
    /// Logika podczas kolizji, je�eli jego tag to Enemy to nadaj si�� obiketowi gracza i go odepchnij
    /// </summary>
    /// <param name="other">obiekt z kt�rym zasz�a kolizja</param>
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
    /// <param name="damage">ilo�� otrzymanych obra�e� </param>
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
    /// Funkcja u�ywana jako event w animatorze po wykonaniu animacji wywo�ywana jest funkcja.
    /// </summary>
    public void IsDead()
    {
        SceneManager.LoadScene("Closing Credits");
        Destroy(gameObject);
    }
    /// <summary>
    /// Przesu� gracza i o vektor oraz ustaw flag� animatora aby wywo�a� odpowiedni� animacj� 
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
    /// Gracz zostaje uleczony a jego pozim �y� si� inkrementuje.
    /// </summary>
    /// <returns>true gdy gracz zosta� uleczony, false gdy nie dosz�o do uleczenia </returns>
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
    /// Zwi�kszenie parametru speed gracza
    /// </summary>
    /// <param name="level">warto�� o kt�r� zwi�kszona zosta�a pr�dko��</param>
    public void UpgradeSpeed(float level)
    {
        this.speed += level;
    }
    /// <summary>
    /// Funkcja ustawia flag� animatora na false co powowduje koniec animacji.
    /// </summary>
    public void EndHurtAnim()
    {
        anim.SetBool("isHurting", false);
    }
    //corutine
    /// <summary>
    /// Korutyna wy��cza komponent BoxCollider2D przez co posta� staje si� niewra�liwa na kolizj� przez okreslony czas.
    /// Po up�yni�ciu czasu komponenty oraz zostaj� w��czone i gracz zn�w mo�e zosta� zatakowany
    /// </summary>
    /// <returns>czas kt�ry zostaje odczekany przez dalsz� realizacj� cia�a funkcji</returns>
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
