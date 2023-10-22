using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] GameObject bullet, endWeapon;
    GameObject[] bulletPool;
    AudioSource fireSource;
    [SerializeField] GameObject fireParticle;

    private float recoil = 0.0f;
    private float maxRecoilX = 10f;
    private float maxRecoilY;
    private float recoilSpeed = 10f;

    public float maxTransZ = -1.0f;

    float fireRate, velocity, timeStamp;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.5f;
        velocity = 15.0f;
        index = 0;

        bulletPool = new GameObject[10];
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(bullet, transform);
            bulletPool[i].SetActive(false);
        }
        fireSource = GetComponent<AudioSource>();
        //fireParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > timeStamp + fireRate)
            {
                fireSource.Play();
                StartRecoil(0.2f, 10f, 10f);
                timeStamp = Time.time;
                if (fireParticle != null)
                {
                    //fireParticle.SetActive(true);
                    Instantiate(fireParticle, endWeapon.transform.position, endWeapon.transform.rotation);
                }
                bulletPool[index].SetActive(true);
                bulletPool[index].transform.position = endWeapon.transform.position;
                bulletPool[index].transform.rotation = endWeapon.transform.rotation;
                bulletPool[index].GetComponent<Rigidbody>().velocity = transform.forward * velocity;
                index++;
                if (index == bulletPool.Length - 1)
                {
                    index = 0;
                }
            }
            else
            {
                fireSource.Stop();
            }
        }
        Recoiling();
    }

    void Recoiling()
    {
        if (recoil > 0f)
        {
            var maxTranslation = new Vector3(transform.localPosition.x, transform.localPosition.y, Random.Range(transform.localPosition.z, maxTransZ));

            transform.localPosition = Vector3.Slerp(transform.localPosition, maxTranslation, Time.deltaTime * recoilSpeed);

            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0f;

            var minTranslation = new Vector3(transform.localPosition.x, transform.localPosition.y, Random.Range(0, transform.localPosition.z));

            transform.localPosition = Vector3.Slerp(transform.localPosition, minTranslation, Time.deltaTime * recoilSpeed);
        }
    }

    public void StartRecoil(float recoilParam, float maxRecoilXParam, float recoilSpeedParam)
    {
        recoil = recoilParam;
        maxRecoilX = maxRecoilXParam;
        recoilSpeed = recoilSpeedParam;
        maxRecoilY = Random.Range(-maxRecoilX, maxRecoilX);
    }
}
