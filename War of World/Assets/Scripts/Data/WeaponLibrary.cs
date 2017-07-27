using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tanks.Utilities;





///
/// weapon
namespace Tanks.Rules
{
   
    [CreateAssetMenu(fileName = "WeaponList", menuName = "Modes/Weapons/Create List", order = 1)]
    public class WeaponLibrary : WeaponListBase<TankWeaponDefinition>
    {

    }
}
