using UnityEngine;
/// <summary>
/// Amunicja przeciwnika
/// </summary>
public class SpiderWeb : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject webDestroyEffect;
    void Start()
    {
        Invoke("DestroyWeb", lifeTime);
    }
    void Update()
    {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
    }
    private void DestroyWeb()
    {
        Instantiate(webDestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    /// <summary>
    /// Zda¿enie gdy dojdzie do kolizji z obiektem 
    /// </summary>
    /// <param name="collision">obiekt z którym zasz³a kolizja </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Player":
                collision.GetComponent<Player>().Attacked(3);
                DestroyWeb();
                break;
            case "Walls":
                DestroyWeb();
                break;
            case "Doors":
                DestroyWeb();
                break;
            default:
               
                break;
        }
    }
 
}
