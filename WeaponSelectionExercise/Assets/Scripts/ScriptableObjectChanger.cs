using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    private int currentIndex;
    [SerializeField] private WeaponDisplay weaponDisplay;
    private void Awake()
    {
        ChangeScriptableObj(0);
    }

    public void ChangeScriptableObj(int change)
    {
        currentIndex += change;

        if (currentIndex < 0)
        {
            currentIndex = scriptableObjects.Length - 1;
        }else if(currentIndex > scriptableObjects.Length - 1)
        {
            currentIndex = 0;
        }


        if (weaponDisplay != null)
        {
            weaponDisplay.DisplayWeapon((Weapon)scriptableObjects[currentIndex]);
        }
    }
}
