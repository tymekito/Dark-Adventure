using UnityEngine;
using System.Collections;
/// <summary>
/// Reprezentacja amunicji gracza 
/// </summary>
public class Ammo : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject ammoEffect;//particle effect ammo
    public AudioClip hurtAudio;
    private GameObject destroyer;
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    /// <summary>
    /// W momêcie stworzenia obiektu tworzymy jego efekt cz¹teczkowy oraz dektarujemy za ile obiekty maj¹ zostaæ zniszczone
    /// </summary>
    private void Awake() 
    {
        destroyer = Instantiate(ammoEffect, gameObject.transform.position, Quaternion.identity);
        Invoke("DestroyAmmo", lifeTime);
        Destroy(destroyer,lifeTime);
    }
    /// <summary>
    /// Poruszanie siê obiektu
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
    }
    private void DestroyAmmo()
    {
         Destroy(gameObject);
        
    }
    /// <summary>
    /// Logika kolizji obiektu 
    /// </summary>
    /// <param name="collision"> obiekt koliduj¹cy </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy" || collision.tag=="Boss")
        {
            collision.GetComponent<Enemy>().Attacked(PermanentUI.perm.playerDMG);
            audio.PlayOneShot(hurtAudio);
            DestroyAmmo();
        }
        if(collision.tag=="Walls")
        {
            DestroyAmmo();
        }
    }

}
