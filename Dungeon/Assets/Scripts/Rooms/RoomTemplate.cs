using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /// <summary>
 /// Przechowywanie listy poki, dostępne do wygnerowania pokoje dla danego kierunku dodajemy w edytorze
 /// </summary>
public class RoomTemplate : MonoBehaviour
{
    // arrys of room types
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject closeRooms;
    public GameObject bossRooms;
    public List<GameObject> rooms;
    public float waitTime;
    private bool spawnedBoos;
    public GameObject boss;

    /// <summary>
    /// Gdy wszystkie pokoje zostały stworzone ostatni z nich zostaje zastapiony pokojem Bossa
    /// </summary>
    private void Update()
    {
        if(waitTime<=1&& spawnedBoos==false)
        {
            int index = rooms.Count - 1;
            Instantiate(bossRooms, rooms[index].transform.position, Quaternion.identity);
            Destroy(rooms[index].gameObject);
            rooms.Add(bossRooms);
            Destroy(gameObject);
            GameObject [] gameObjects=GameObject.FindGameObjectsWithTag("SpawnPoint");
            foreach (var gm in gameObjects)
            Destroy(gm);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
