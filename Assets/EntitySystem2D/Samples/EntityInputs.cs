using GoldSprite.UnityPlugins.MyInputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityInputs : MyUInputManager {
    protected override Dictionary<InputActionMap, bool> SetInputActionMaps()
    {
        var maps = new Dictionary<InputActionMap, bool>() {
                { InputActions.GamePlay, true },
                { InputActions.UIPlay, true }
            };
        return maps;
    }

    protected override void InitActions()
    {
        AddActionListener(InputActions.TestPlay.Move, (Action<Vector2>)Move);
        AddActionListener(InputActions.TestPlay.MoveBoost, (Action<bool>)MoveBoost);
        AddActionListener(InputActions.TestPlay.Attack, (Action<bool>)Attack);
        AddActionListener(InputActions.TestPlay.SpecialAttack, (Action<bool>)SpecialAttack);
        AddActionListener(InputActions.TestPlay.Hurt, (Action<bool>)Hurt);
    }

    private void MoveBoost(bool down)
    {
    }

    public void Move(Vector2 dir)
    {
        
    }

    public void Attack(bool down)
    {
    }

    public void SpecialAttack(bool down)
    {
    }

    public void Hurt(bool down)
    {
    }
}
