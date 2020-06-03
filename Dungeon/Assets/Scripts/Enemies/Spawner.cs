using System.Collections;
using UnityEngine;

/// <summary>
/// Tworzenie nowych przeciwnik�w
/// </summary>

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject [] enemies;
    [SerializeField] private float respawnTime;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float trigerDistance=200;
    private bool check=false;
    private void FixedUpdate()
    {
        if (!check)
        {
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position, gameObject.transform.position) < trigerDistance)
            {
                InvokeRepeating("spawnEnemy", 0, respawnTime);   //wywo�uj funkcje co okre�lony czas
                check = true;
            }
        }
    }
    /// <summary>
    /// Tworzenie przeciwnika na podstawie obiektu z tablicy enemies, gdy liczba numberOfEnemies b�dzie mniejsza od 0 funkcja nie b�dzie ju� wywo�ywana
    /// </summary>
    public void spawnEnemy()
    {
            int cell = Random.Range(0, enemies.Length);//kom�rka tablicy 
            Instantiate(enemies[cell], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,0), gameObject.transform.rotation);//stworzenie przeciwnika
            numberOfEnemies--;
            if (numberOfEnemies < 0)
                CancelInvoke("spawnEnemy");//koniec wywo�ywaniad
    }
}
