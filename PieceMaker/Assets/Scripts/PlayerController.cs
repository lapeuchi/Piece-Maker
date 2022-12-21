using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxHp;
    public float hp;
    float dif;
    CharacterController controller;

    [SerializeField] TextMeshProUGUI hp_Text;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        maxHp = 100;

        hp = maxHp;
        dif = 20;
        SetHPText();
    }

    void Update()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.X))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
       
    }
    
    public void Hit()
    {
        if(hp <= 0) return;

        hp -= dif;
        
        if(hp <= 0)
        {
            hp = 0;
            Die();
        }

        SetHPText();
    }

    public void Heal()
    {
        hp += dif;
        if(hp >= maxHp) hp = maxHp;
        SetHPText();
    }
    
    void Die()
    {
        Debug.Log("Player is dead.");
        GameManager.instance.GameOver(false);
        SetHPText();
    }

    void SetHPText()
    {
        hp_Text.text = $"HP: {(int)hp}";
    }

}
