using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//requied components is when the script only works aslong the specefic components is paired with it
//the required component for this script is Weapon component
[RequireComponent(typeof(Weapon))]
public class WeaponAction : GameAction
{
    protected Weapon weapon;

    


    public override void Awake()
    {
        // this is the required component 
        weapon = GetComponent<Weapon>();
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
}