using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Logika pokoju bossa i elementów w nim występujących 
/// </summary>
public class BossRoom : MonoBehaviour
{
    /// <summary>
    /// Komponenty dodajemy w edytorze
    /// </summary>
    [SerializeField] GameObject boss;
    [SerializeField] List<GameObject> doors;
    [SerializeField] GameObject bossHealthBar;
    [SerializeField] GameObject spawnEffect;
    [SerializeField] Sprite OpenSkullEffect;
    //public AudioSource ColissionAudio;
    public AudioClip ColissionAudio;
    public AudioClip WinSound;
    private bool bossSpawned = false;
    /// <summary>
    /// Drzwi stają się nie możliwe dla przejścia żadnego z obiektów gry 
    /// </summary>
    public void CloseDoors()
    {
        foreach(GameObject cell in doors)
        {
            cell.GetComponent<BoxCollider2D>().enabled = true;//turn on colider
            cell.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    /// <summary>
    /// Drzwi są możliwe do przejścia 
    /// </summary>
    public void OpenDoors()
    {
       foreach (GameObject cell in doors)
       {
          cell.GetComponent<BoxCollider2D>().enabled = false;//set component status
          cell.GetComponent<SpriteRenderer>().enabled = true;
       }     
    }
    /// <summary>
    /// zmiana grafiki obiektu 
    /// </summary>
    public void OpenSkull()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OpenSkullEffect;
    }
    /// <summary>
    /// Logika pojawienia się bossa 
    /// </summary>
    public void SpawnBoss()
    {
        try
        {
            PermanentUI.perm.CreateBossHealthBar(bossHealthBar);
            Instantiate(boss, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            Instantiate(spawnEffect, transform.position, Quaternion.identity);
            spawnEffect.GetComponent<Animator>().Play("ExpAnimator", 1);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    /// <summary>
    /// w przypadku kolizji z obiektem wywołującym bossa 
    /// </summary>
    /// <param name="collision"> obiekt z którym zaszła kolizja </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource audio = GetComponent<AudioSource>();
        //ColissionAudio = GetComponent<AudioSource>();
        if (collision.tag == "Player")
        {
            if (bossSpawned == false)
            {
                CloseDoors();
                SpawnBoss();
                //ColissionAudio.Play();
                audio.PlayOneShot(ColissionAudio);
                OpenSkull();
                bossSpawned = true;
            }
            if (!GameObject.FindGameObjectWithTag("Boss"))//when boss diead
            {
                PermanentUI.perm.DestroyBossHealthBar(bossHealthBar);
                OpenDoors();//after fight open doors
                if (PermanentUI.perm.redKey)//player have key
                {
                    //ColissionAudio.Play();
                    audio.PlayOneShot(ColissionAudio);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//go  next level
                    PermanentUI.perm.playerDMG += 1;
                    PermanentUI.perm.SavePlayerStats();
                }
                if (PermanentUI.perm.greenKey)
                {
                    audio.PlayOneShot(WinSound);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//go  next level
                    PermanentUI.perm.playerDMG += 1;
                    PermanentUI.perm.SavePlayerStats();
                }
            }
        }
    }
}
