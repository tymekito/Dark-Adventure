using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Logika stanu gry w którym wstrzymane są wszytskie czynności gracza.
/// Po wciśnięciu przycisku, gra przechodzi w stan pauzy poprzez ustawienie parametru czasu w grze na 0.
/// </summary>
public class PauseController : MonoBehaviour
{
    static bool isPaused=false;
    bool isCheck;
    [SerializeField] private GameObject pausePanel;
    
    private void Update() 
    {
    
       if(Input.GetKey(KeyCode.Escape))
       {
      
           if(isPaused) 
           {
           resumeGame();
           }
           else
           {
           pauseGame();
           }
        }
        
    }
    /// <summary>
    /// Stan pauzy, czas ustawiony na 0 a panel z przyciksami staje się aktywny
    /// </summary>
    public void pauseGame()
    {
        pausePanel.gameObject.SetActive(true);
        Time.timeScale=0f;
        isPaused=true;
    }
    /// <summary>
    /// Stan wznowienia rozgrywki, czas ustawiony na 0 a panel z przyciksami staje się nie aktywny
    /// </summary>
    public void resumeGame()
    {
        pausePanel.gameObject.SetActive(false);
        Time.timeScale=1f;
        isPaused=false;
    }
    /// <summary>
    /// Powrót do menu głównego, po wywołaniu funkcji gra wraca do menu głównego a UI gracza zostaje zniszone 
    /// </summary>
    public void menuReturn()
    {
        SceneManager.LoadScene("Menu");
        resumeGame();
        if(PermanentUI.perm)
        {
        PermanentUI.perm.DestroyUI();
        }
    }
}
