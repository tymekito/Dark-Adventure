using UnityEngine;

/// <summary>
/// Przeciwnik zasiêgowy
/// </summary>
///<inheritdoc cref="Enemy"/>
public class RangedSpider : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private int attackRange;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject spiderWeb;
    private Animator anim;
    private float shootDelay = 5f;
    private float time = 0;
    new void Start()
    {
        base.Start();
        destroyParticles.GetComponent<ParticleSystem>().Play();
    }
    new void Update()
    {
        time += Time.deltaTime;
        //if exist player, find him possition a
        if (player != null)
        {
            //if player isn't in range area follow him
            if (Vector2.Distance(player.transform.position, this.transform.position) <= attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(player.transform.position, this.transform.position) <= attackRange / 2)
            {
                //if player is in shoot area and spider did not shooting eariler, shoot
                if (time > shootDelay)
                {
                    time = 0;
                    Vector2 direction = player.position - startPoint.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    startPoint.rotation = rotation;
                    Instantiate(spiderWeb, startPoint.position, startPoint.rotation);
                }
            }
        }
    }
}
