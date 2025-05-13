using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    //the prefab that will be instansiated when picked up
    public GameObject weaponPrefab;

    //the transformsocket to which the weapon will be parented to the player
    public Transform weaponSocket;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //instatiate and parent directly to weapon socket
            GameObject newWeapon = Instantiate(weaponPrefab, weaponSocket.position, Quaternion.identity, weaponSocket);

            //resetting local pos and rotation to ensure it fits in the socket
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            //add it to the list 
            other.GetComponent<WeaponManager>().AddWeapon(newWeapon);

            //destroy weapon and pick up game object
            Destroy(gameObject);
            Debug.Log("Weapon Picked Up");
        }
    }

}
