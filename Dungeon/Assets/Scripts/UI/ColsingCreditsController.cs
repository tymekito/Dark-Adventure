using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Ustawienie punktów gracza i usunięcie naszego UI
/// </summary>
public class ColsingCreditsController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        scoreText.text = PermanentUI.perm?.score.ToString();
        PermanentUI.perm?.DestroyUI();
    }
}
