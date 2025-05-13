using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //list to hold all weapon instances the player had picked up
    public List<GameObject> weaponList = new List<GameObject>();
    //index to track currently active weapon
    private int currentWeaponIndex = -1; //start with no weapon


    // Update is called once per frame
    void Update()
    {
        //ket to switch weapons
        if (Input.GetKeyUp(KeyCode.Q) && weaponList.Count > 0)
        {
            //+1 increments the currentweaponindex by moving 1 to next weapon list
            //and then divide symbol wraps around to beginning of list
            int nextWeaponIndex = (currentWeaponIndex + 1) % weaponList.Count;
            SwitchWeapon(nextWeaponIndex);
        }
    }
    public void AddWeapon(GameObject weaponPrefab)
    {
        //add the instatinated weapon to the list
        weaponList.Add(weaponPrefab);
        //prevents multiple active weapons
        weaponPrefab.SetActive(false);//start with weapon disabled

        if (weaponList.Count == 1) //if its the first weapon picked up, activate it
        {
            //switch weapon function
            SwitchWeapon(0);
        }
    }
    void SwitchWeapon(int index)
    {
        //deactive the current active weapon if there is one
        if (currentWeaponIndex != -1)
        {
            //ensures when switching weapons the previous one if off
            weaponList[currentWeaponIndex].SetActive(false);

        }
        //SET THE NEW WEAPON AS ACTIVE AND UPDATE THE CURRENT INDEX
        currentWeaponIndex = index;
        weaponList[currentWeaponIndex].SetActive(true);
    }
}
