using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Algorytm tworzenia pokoji:
/// Jeżeli flaga jest ustwiona na true,
/// w zależności od kierunku stwórz losowy pokój z listy 
/// </summary>
public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private int openningDirection;
    // 1-> need bottom room;
    // 2-> top
    // 3-> left
    // 4-> right
    private RoomTemplate templates;
    private int and = 0;
    private bool spawns = false;
    public float waitTime = 0.1f;
    private void Start()
    {
        Destroy(gameObject, waitTime);//destroy spawning point after time 
        templates = GameObject.FindGameObjectWithTag("RoomsTemplate")?.GetComponent<RoomTemplate>();//find room template class
        Invoke("Spawn",0.1f);
    }
    void Spawn()
    {
        if (spawns == false)
        {
            switch (openningDirection)//create room from array
            {
                case 1:
                    Instantiate(templates.bottomRooms[Random.Range(0, templates.bottomRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case 2:
                   
                    Instantiate(templates.topRooms[Random.Range(0, templates.topRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case 3:
                
                    Instantiate(templates.leftRooms[Random.Range(0, templates.leftRooms.Length)], transform.position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(templates.rightRooms[Random.Range(0, templates.rightRooms.Length)], transform.position, Quaternion.identity);
                    break;

                default:
                    break;
            }
            spawns = true;
        }

    }
    /// <summary>
    /// Jeżeli spawn point kloduje z innym punktem zniszcz ten SpawnPoint i pozwój stworzyć inny pokój 
    /// </summary>
    /// <param name="collision"> obiekt z którym zaszła kolizja</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint" ) )
        {

            if (collision.GetComponent<RoomSpawner>()?.spawns == false && spawns == false)
            {
                try
                {
                    Instantiate(templates.closeRooms, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                catch(System.Exception e)
                {
                    Debug.Log("nie mozna stworzyc pokoju");
                }
            }
           
            spawns = true;
        }
    }
}
