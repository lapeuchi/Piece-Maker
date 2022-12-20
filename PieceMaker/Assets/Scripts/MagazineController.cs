using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagazineController : MonoBehaviour
{
    public int maxBullet;
    public int curBullet;
    
    public float reloadTime;
    public bool isReloading;

    [SerializeField] public TMP_Text Bullet_Text;

    void Start()
    {
        curBullet = maxBullet;
        SetBulletText();
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
            timer += Time.deltaTime;
            Bullet_Text.text = "Reloading " + (int)(reloadTime - timer);
            yield return null;
        }

        curBullet = maxBullet;
        SetBulletText();
        isReloading = false;
    }

    public void SetBulletText()
    {
        Bullet_Text.text = $"{curBullet} / {maxBullet} ";
    }
}
