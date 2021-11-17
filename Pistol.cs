using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : Weapon
{
    public override void Use(InputAction.CallbackContext context)
    {
        Shoot(context);
    }
    
    public override void OnInput(InputAction.CallbackContext aContext)
    {
        Use(aContext);
    }
}