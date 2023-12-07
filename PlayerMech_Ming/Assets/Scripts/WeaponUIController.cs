using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponUIController : MonoBehaviour
{
    [SerializeField] List<SlotController> weaponSlotList = new List<SlotController>();
    [SerializeField] List<GameObject> slotButtonList = new List<GameObject>();

    [SerializeField] List<WeaponController> weaponList = new List<WeaponController>();

    private WeaponController weapon;

    [SerializeField] PlayerController player;

    int lastActiveSlot;

    //0 = leftArm
    //1 = leftShoulder
    //2 = rightArm
    //3 = rightShoulder

    void Start()
    {
        HideSelectSlot();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.rKey.wasPressedThisFrame)
            {
                if(weaponSlotList.Count(x => x.isUsing && x.isActive == true) > 0)
                {
                    int activeSlot = GetActiveSlot();
                    int releaseWeaponIndex = weaponList.FindIndex(x => x.currentSlot == activeSlot);
                    weaponList[releaseWeaponIndex].ReleaseWeapon();
                    weaponSlotList[activeSlot].isActive = false;
                    weaponSlotList[activeSlot].isUsing = false;
                }
            }
        }
        if (!player.isShowText)
        {
            HideSelectSlot();
        }
    }

    private int GetActiveSlot()
    {
        return weaponSlotList.FindIndex(x => x.isActive == true);
    }

    public void SlotActiveButtonClick(int slotNumber)
    {
        InActivateAllSlot();
        weaponSlotList[slotNumber].isActive = true;
        int weaponIndex = weaponList.FindIndex(x => x.currentSlot == slotNumber);
        weaponList[weaponList.FindIndex(x => x.currentSlot == lastActiveSlot)].isWeaponActive = false;
        weaponList[weaponIndex].isWeaponActive = true;
        lastActiveSlot = slotNumber;
    }

    public void SelectedSlot(int slotNumber)
    {
        player.isShowText = false;
        HideSelectSlot();
        if (weaponSlotList[slotNumber].isUsing)
        {
            int releaseWeaponIndex = weaponList.FindIndex(x => x.currentSlot == slotNumber);
            weaponList[releaseWeaponIndex].ReleaseWeapon();
            weaponSlotList[slotNumber].isActive = false;
            weaponSlotList[slotNumber].isUsing = false;
        }
        weapon.currentSlot = slotNumber;
        weapon.isOnTheGround = false;
        weapon.isWeaponActive = true;
        weaponSlotList[slotNumber].isUsing = true;
        weaponSlotList[slotNumber].image.texture = weapon.weaponImage;
        weaponSlotList[slotNumber].ammo.text = weapon.ammo.ToString();
        if (CheckActiveSlot())
        {
            lastActiveSlot = slotNumber;
            weaponSlotList[slotNumber].isActive = true;
        }
    }

    private void InActivateAllSlot()
    {
        for (int index = 0; index < weaponSlotList.Count; index++)
        {
            weaponSlotList[index].isActive = false;
        }
    }

    private bool CheckActiveSlot()
    {
        for (int index = 0; index < weaponSlotList.Count; index++)
        {
            if (weaponSlotList[index].isActive)
            {
                return false;
            }
        }
        return true;
    }

    public void ShowAvailableSlot(WeaponController weaponC)
    {
        weapon = weaponC;
        for (int index = 0; index < weaponSlotList.Count; index++)
        {
            //if (!weaponSlotList[index].isUsing)
            //{
            slotButtonList[index].SetActive(true);
            //}
        }
    }

    public void HideSelectSlot()
    {
        for (int index = 0; index < weaponSlotList.Count; index++)
        {
            slotButtonList[index].SetActive(false);
        }
    }
}
