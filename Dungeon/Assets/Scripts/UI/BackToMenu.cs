using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Zawiera funkcje która ładuje zceneę o określonym tagu
/// </summary>
public class BackToMenu : MonoBehaviour
{

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
