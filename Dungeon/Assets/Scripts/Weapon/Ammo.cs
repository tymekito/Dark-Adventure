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
    /// W mom�cie stworzenia obiektu tworzymy jego efekt cz�teczkowy oraz dektarujemy za ile obiekty maj� zosta� zniszczone
    /// </summary>
    private void Awake() 
    {
        destroyer = Instantiate(ammoEffect, gameObject.transform.position, Quaternion.identity);
        Invoke("DestroyAmmo", lifeTime);
        Destroy(destroyer,lifeTime);
    }
    /// <summary>
    /// Poruszanie si� obiektu
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
    /// <param name="collision"> obiekt koliduj�cy </param>
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
