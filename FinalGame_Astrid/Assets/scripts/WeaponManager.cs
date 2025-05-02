using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
   public List<GameObject> weaponList = new List<GameObject>();

   private int currentWeaponIndex = -1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && weaponList.Count > 0) 
        { 
        int nextWeaponIndex = (currentWeaponIndex + 1) % weaponList.Count; SwitchWeapon(nextWeaponIndex);

        }
    }
    public void AddWeapon(GameObject weaponPrefab)
    {
        weaponList.Add(weaponPrefab);
        weaponPrefab.SetActive(false);

        if (weaponList.Count == 1)
        {
            SwitchWeapon(0);
        }
    }

    void SwitchWeapon(int index)
    {
        if (currentWeaponIndex != -1)
        {
            weaponList[currentWeaponIndex].SetActive(false);
        }

        weaponList[currentWeaponIndex].SetActive(true);
    }
}
