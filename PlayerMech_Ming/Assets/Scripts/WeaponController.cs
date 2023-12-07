using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public bool isOnTheGround;
    public bool isWeaponActive;
    public int currentSlot;

    private float fallSpeed = 4.0f;

    [SerializeField] GameObject weapon;
    public int ammo;
    [SerializeField] WeaponUIController weaponUI;
    public Texture weaponImage;

    [SerializeField] GameObject leftArm;
    [SerializeField] GameObject leftShoulder;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject rightShoulder;

    [SerializeField] string weaponName;

    private float projectileSpeed;
    [SerializeField] ProjectTileBullet projectilePrefab;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] GameObject fireParticle;

    [SerializeField] GameObject endBarrel;

    [SerializeField] AudioSource fireSound;

    private int initialAmmo;

    private void Start()
    {
        currentSlot = -1;
        isOnTheGround = true;
        projectileSpeed = 10.0f;
        isWeaponActive = false;
        initialAmmo = ammo;
    }

    //0 = leftArm
    //1 = leftShoulder
    //2 = rightArm
    //3 = rightShoulder

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWeaponActive && !isOnTheGround)
        {
            fireSound.Play();
            ammo -= 1;
            if (weaponName == "Orange")
            {
                Instantiate(fireParticle, endBarrel.transform.position, endBarrel.transform.rotation);
                var projectile = Instantiate(projectilePrefab, endBarrel.transform.position, leftArm.transform.rotation);
                projectile.Fire(projectileSpeed);
            }
            if(weaponName == "Yellow")
            {
                //laser
                Instantiate(fireParticle, endBarrel.transform.position, endBarrel.transform.rotation);
                var laser = Instantiate(laserPrefab, endBarrel.transform.position, endBarrel.transform.rotation);
                laser.GetComponent<Rigidbody>().velocity = laser.transform.TransformDirection(Vector3.right * 15.0f);
            }
            if (weaponName == "Grey")
            {
                //bullet
                Instantiate(fireParticle, endBarrel.transform.position, endBarrel.transform.rotation);
                var bullet = Instantiate(bulletPrefab, endBarrel.transform.position, endBarrel.transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.right * 8.0f);
            }
        }
        if (!isOnTheGround)
        {
            if(currentSlot == 0)
            {
                //left arm
                weapon.transform.SetParent(leftArm.transform);
                weapon.transform.localPosition = new Vector3(leftArm.transform.localPosition.x + .38f, leftArm.transform.localPosition.y, leftArm.transform.localPosition.z);
                weapon.transform.localRotation = Quaternion.Euler(177.957f, 91.723f, 176.893f);
            }
            else
            {
                if (currentSlot == 1)
                {
                    //left shoulder
                    weapon.transform.SetParent(leftShoulder.transform);
                    weapon.transform.localPosition = new Vector3(leftShoulder.transform.localPosition.x + .38f, leftShoulder.transform.localPosition.y, leftShoulder.transform.localPosition.z);
                    weapon.transform.localRotation = Quaternion.Euler(-4.545f, -88.7f, -5.64f);
                }
                else
                {
                    if (currentSlot == 2)
                    {
                        //right arm
                        weapon.transform.SetParent(rightArm.transform);
                        weapon.transform.localPosition = new Vector3(rightArm.transform.localPosition.x - .5f, rightArm.transform.localPosition.y, rightArm.transform.localPosition.z);
                        weapon.transform.localRotation = Quaternion.Euler(2.436f, -83.254f, -1.93f);
                    }
                    else
                    {
                        if (currentSlot == 3)
                        {
                            //right shoulder
                            weapon.transform.SetParent(rightShoulder.transform);
                            weapon.transform.localPosition = new Vector3(rightShoulder.transform.localPosition.x - .5f, rightShoulder.transform.localPosition.y, rightShoulder.transform.localPosition.z);
                            weapon.transform.localRotation = Quaternion.Euler(2.941f, -90.7f, -2.429f);
                        }
                    }
                }
            }
        }
        if(!isWeaponActive && isOnTheGround && weapon.transform.localPosition.y > 0.5f)
        {
            weapon.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    public void ReleaseWeapon()
    {
        isOnTheGround = true;
        isWeaponActive = false;
        weapon.transform.parent = null;
        currentSlot = -1;
        ammo = initialAmmo;
    }

    public void AttachWeapon()
    {
        weaponUI.ShowAvailableSlot(this);
    }

}
