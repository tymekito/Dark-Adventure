using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Dodanie obiektu do listy obiektów typu RoomTemplate o tagu 
/// </summary>
public class AddRoom : MonoBehaviour
{
    private RoomTemplate template;
    void Start()
    {
        // find rooms dynamic list and add to it this object 
        template = GameObject.FindGameObjectWithTag("RoomsTemplate").GetComponent<RoomTemplate>();
        // is used when creating a room list
        template.rooms.Add(this.gameObject);
    }


}
