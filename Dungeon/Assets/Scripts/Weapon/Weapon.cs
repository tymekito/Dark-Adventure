using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Logika broni gracza
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform startPoint;
    private bool canShoot = true;
    public float shootDealey;
    private AudioSource audioSrc;
    void Update()
    {
        audioSrc = GetComponent<AudioSource>();
        //follow mouse direction and save it to vector variable
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculate angle of rotation
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                StartCoroutine(shoot());
                audioSrc?.Play();
            }
        }
    }
    /// <summary>
    /// Korutyna tworz¹ca obiekt ammo po up³yniêciu czasu zdefiniowanego w shootDealey gracz znów mo¿e wywo³aæ funkcjê poprzez ustawienie flagi canShoot
    /// </summary>
    /// <returns></returns>
    public IEnumerator shoot()
    {
        // shoot logic
        Instantiate(ammo, startPoint.position, transform.rotation);
        canShoot = false;
        yield return new WaitForSeconds(shootDealey);
        canShoot = true;
    }
    /// <summary>
    /// Dekrementacja zmiennej odpwowiedzialnej za czas pomiêdzy stra³ami
    /// </summary>
    /// <param name="level"> o ile zmniejszyæ zmiennn¹ </param>
    public void DecrementShotDelay(float level)
    {
        shootDealey -= level;
        if (shootDealey < 0.1f)
            shootDealey = 0.1f;
    }
}
