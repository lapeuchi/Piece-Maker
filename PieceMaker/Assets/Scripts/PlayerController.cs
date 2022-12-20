using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float hp = 100f;
    public float speed = 3.5f;

    [SerializeField] TextMeshProUGUI hp_Text;
    
    void Start()
    {
        SetHPText();
    }

    public void Hit(float damage)
    {
        hp -= damage;
        
        if(hp <= 0)
        {
            hp = 0;
            Die();
        }
        SetHPText();
    }
    
    void Die()
    {
        GameOver();
    }

    void SetHPText()
    {
        hp_Text.text = $"HP: {(int)hp}";
    }

    public void GameOver()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/UI_GameOver");
        Instantiate(go, transform.position + Vector3.forward, transform.rotation);
    }
}
