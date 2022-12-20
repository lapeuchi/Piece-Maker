using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    PlayerController player;
    [SerializeField] GameObject GameOverUI;
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        GameOverUI.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(player.transform);
    }

    public void GameOver()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameOverUI.SetActive(true);
        Vector3 pos = player.transform.position;
        pos.y = 1;
        pos += player.transform.forward;
        transform.position = pos;
    }
}
