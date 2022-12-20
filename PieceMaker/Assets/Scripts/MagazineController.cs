using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineController : MonoBehaviour
{
    public int maxBullet;
    public int curBullet;
    
    public float reloadTime;
    bool isReloading;

    [SerializeField] public TMP_Text Bullet_Text;

    void Start()
    {
        curBullet = maxBullet;
    }
    
    void Update()
    {
        
    }

    public void Reload()
    {
        if (isReloading) return;
        isReloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {   
        float timer = 0;
        
        while (timer < reloadTime)
        {
            Bullet_Text.text = "Reloading " + (int)(reloadTime - timer);
            yield return null;
        }

        curBullet = maxBullet;
        SetBulletText();
        isReloading = false;
    }

    void SetBulletText()
    {
        Bullet_Text.text = $"{curBullet} / {maxBullet} ";
    }
}
