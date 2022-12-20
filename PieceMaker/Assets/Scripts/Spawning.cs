using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float currentTime = 0;
    [SerializeField] float createTime = 12;

    void Start()
    {
        enemy = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > createTime)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            currentTime = 0;
        }
    }
}
