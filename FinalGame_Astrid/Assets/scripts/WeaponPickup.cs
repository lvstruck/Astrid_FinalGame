using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;

    public Transform weaponSocket;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject newWeapon = Instantiate(weaponPrefab, weaponSocket.position, Quaternion.identity, weaponSocket);

            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            
            other.GetComponent<WeaponManager>().AddWeapon(newWeapon);

            Destroy(gameObject);
        }
    }

}
