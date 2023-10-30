using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Transform weaponHolder;

    public void DisplayWeapon(Weapon weapon)
    {
        weaponName.text = weapon.weaponName;

        if(weaponHolder.childCount > 0)
        {
            Destroy(weaponHolder.GetChild(0).gameObject);
        }

        Instantiate(weapon.weapon, weaponHolder.position, weaponHolder.rotation, weaponHolder);
    }
}
