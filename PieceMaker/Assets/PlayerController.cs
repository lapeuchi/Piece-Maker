using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float hp;
    public float speed;

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
        Debug.Log("Player is dead.");
        
    }

    void SetHPText()
    {
        hp_Text.text = $"HP: {(int)hp}";
    }
}
