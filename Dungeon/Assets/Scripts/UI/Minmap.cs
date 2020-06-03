using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Śledzenie pozycji gracza o podanym tagu i przypisywanie jej pozycji do kamery minimapy
/// </summary>
public class Minmap : MonoBehaviour
{
void LateUpdate() 
{
    //przypisujemy do naszego obiektu tylko wartość y, x i z są ustawione na 0 czyli bez zmian
    GameObject player= GameObject.FindGameObjectWithTag("Player");
    if(player!=null)
    {
    Vector3 newPos=player.GetComponent<Transform>().position;
    transform.position=newPos;
    //transform.rotation=Quaternion.Euler(90f,player.eulerAngles.y,0f);
    }
}
}
