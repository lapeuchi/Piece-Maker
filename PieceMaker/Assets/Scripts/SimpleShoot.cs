using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab; // 총알
    public GameObject casingPrefab; // 탄피
    public GameObject muzzleFlashPrefab; // 머즐 빛

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator; // 발사 애니메이션
    [SerializeField] private Transform barrelLocation;  // 총구 위치 (발사 지점)
    [SerializeField] private Transform casingExitLocation; // 탄피 배출구

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f; // 떨어진 탄피 제거 시간
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 100f; // 총알 힘
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f; // 탄피 배출 힘
    
    [SerializeField] private MagazineController magazine;
    public OVRInput.Button shootButton;
    public OVRInput.Button reloadButton;

    private static OVRGrabbable grabbable;
    private AudioSource audioSource;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        grabbable = GetComponentInParent<OVRGrabbable>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    
    void Update()
    {
        // If you want a different input, change it here
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     // Calls animation on the gun that has the relevant animation events that will fire
        //     gunAnimator.SetTrigger("Fire");
        // }
        if(magazine.curBullet > 0)
        {
            if (OVRInput.GetDown(shootButton, OVRInput.Controller.RTouch) && !gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            {
                gunAnimator.SetTrigger("Fire");
            }
            else if (OVRInput.GetDown(reloadButton, OVRInput.Controller.RTouch) && magazine.curBullet != magazine.maxBullet)
            {
                if (magazine.isReloading == false)
                    magazine.Reload();
            }
        }
    }
    

    public void StartShoot()
    {
        gunAnimator.SetTrigger("Fire");
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);//.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower, ForceMode.Impulse);

        if (magazine != null)
        {
            magazine.curBullet--;
            audioSource.Play();
            magazine.SetBulletText();

            if (magazine.curBullet == 0)
            {
                magazine.Reload();
            }
        }
    }

    //This function creates a casing at the ejection slot
    // 탄피 떨어지는 이펙트
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
