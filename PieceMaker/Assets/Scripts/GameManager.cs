using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    PlayerController player;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject botPrefab;
    [SerializeField] Transform spawnPos;

    int[] spawnCount = new int[3];

    public int cnt = 0;

    [SerializeField] TMP_Text result_Text;
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        GameOverUI.SetActive(false);

        spawnCount[0] = 5;
        spawnCount[1] = 10;
        spawnCount[2] = 20;

        StartCoroutine(Spwan());
    }

    void Update()
    {
        transform.LookAt(player.transform);
    }

    IEnumerator Spwan()
    {
        for(int i = 0; i < spawnCount.Length; i++)
        {
            for(int j = 0; j < spawnCount[i]; j++)
            {
                Instantiate (botPrefab, spawnPos.position, spawnPos.rotation);
                cnt++;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(20f);
        }

        yield return new WaitUntil(()=> cnt == 0);
        GameOver(true);
    }

    public void GameOver(bool clear)
    {
        StopCoroutine(Spwan());
        StopCoroutine(Spwan());
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        
        GameOverUI.SetActive(true);
        if (clear)
        {
            result_Text.text = "Win!!!";
            result_Text.color = Color.green;
        }
        else
        {
            result_Text.text = "Lose...";
            result_Text.color = Color.red;
        }

        Vector3 pos = player.transform.position;
        
        pos.y = 2f;
        pos += player.transform.forward * 2;
        transform.position = pos;
    }
}
