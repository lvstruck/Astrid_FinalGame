using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   //bullets!
    public GameObject bulletPrefab;
    public Transform bulletSpawnPos;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        //spawning bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);

        //shooting the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPos.forward.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestoryBulletAfterTime(bullet, bulletPrefabLifeTime));

    }
    //destroying the bullet
    IEnumerator DestoryBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
