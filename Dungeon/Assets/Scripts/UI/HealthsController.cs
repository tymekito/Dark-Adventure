using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa ustawia ilosc aktywnych serc na UI gracza, obiekty łączymy w edytorze
/// </summary>
public class HealthsController : MonoBehaviour
{
    //number of GameObjects represent UI health
    [SerializeField] private GameObject[] healths;

    //set health every time
    private void FixedUpdate()
    {
        setHealths();
    }
    /// <summary>
    /// ustaw zdrowia w zależności od parametru w UI
    /// </summary>
    private void setHealths()
    {
        //set the right level of UI health
        switch (PermanentUI.perm?.health)
        {
            case 0:
                setHerathActive(0);
                break;
            case 1:
                setHerathActive(1);
                break;
            case 2:
                setHerathActive(2);
                break;
            case 3:
                setHerathActive(3);
                break;
            case 4:
                setHerathActive(4);
                break;
            case 5:
                setHerathActive(5);
                break;
            default:
                setHerathActive(0);
                break;

        }
    }
/// <summary>
/// 
/// </summary>
/// <param name="number"> ilosc ustawnionego zdrowia </param>
    void setHerathActive(int number)
    {
        for (int i = 0; i < healths.Length; i++)
            healths[i].SetActive(false);
        for (int i = 0; i < number; i++)
            healths[i].SetActive(true);
    }
}
