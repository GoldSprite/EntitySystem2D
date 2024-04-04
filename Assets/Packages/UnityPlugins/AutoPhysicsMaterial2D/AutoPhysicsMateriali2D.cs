using GoldSprite.UFsm;
using GoldSprite.UnityPlugins.PhysicsManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPhysicsMateriali2D : MonoBehaviour {
    [ManualRequire]
    public PhysicsManager PhysicsManager;
    public PhysicsMaterial2D[] SmoothOrRoughMaterial;
    [ManualRequire]
    public Rigidbody2D rb;
    public PhysicsMaterial2D PhysicsMaterial { get => rb.sharedMaterial;set => rb.sharedMaterial = value; }


    void Update()
    {
        PhysicsMaterial = SmoothOrRoughMaterial[PhysicsManager.IsGround ? 1 : 0];
    }
}
