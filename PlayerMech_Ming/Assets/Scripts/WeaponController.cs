using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] GameObject fireParticle;

    [SerializeField] GameObject endBarrel;
    
    private void Start()
    {
        currentSlot = -1;
        isOnTheGround = true;
        projectileSpeed = 10.0f;
        isWeaponActive = false;
    }

    //0 = leftArm
    //1 = leftShoulder
    //2 = rightArm
    //3 = rightShoulder

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWeaponActive)
        {
            if(weaponName == "Orange")
            {
                Debug.Log("proj");
                //projectile
                //Instantiate(fireParticle, weapon.transform.position, leftArm.transform.rotation);
                //var projectile = Instantiate(projectilePrefab, weapon.transform.position, leftArm.transform.rotation);
                //projectile.Fire(projectileSpeed);
                Instantiate(fireParticle, endBarrel.transform.position, endBarrel.transform.rotation);
                var projectile = Instantiate(projectilePrefab, endBarrel.transform.position, leftArm.transform.rotation);
                projectile.Fire(projectileSpeed);
            }
            if(weaponName == "Yellow")
            {
                //laser
                Instantiate(fireParticle, endBarrel.transform.position, endBarrel.transform.rotation);
                var laser = Instantiate(laserPrefab, endBarrel.transform.position, endBarrel.transform.rotation);
                laser.GetComponent<Rigidbody>().velocity = laser.transform.TransformDirection(Vector3.right * 15.0f); ;

            }
            if (weaponName == "Grey")
            {
                //
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
    }

    public void AttachWeapon()
    {
        weaponUI.ShowAvailableSlot(this);
    }

}
