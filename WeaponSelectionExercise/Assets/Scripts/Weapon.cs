using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Weapon Selection", menuName = "Scriptable Objects")]
public class Weapon : ScriptableObject
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public int weaponIndex;
    public string weaponName;
    public string weaponDescription;
    public GameObject weapon;
}
