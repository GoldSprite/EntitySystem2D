

using GoldSprite.UnityPlugins.MyAnimator;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldSprite.UFsm {
    public class AIFsm : BaseFsm {
        //protected override SortedDictionary<Type, IState> states { get; set; }
        public new IAIProps Props { get; protected set; }
        protected new AIFsmCommandManager Cmd { get; set; }
        public BaseFsm ctrlFsm;

        protected override void InitCommands()
        {
            //Cmd.RegisterCommand(AIFsmCommand.Roam, (Action<bool>)((down) => {
            //    Props.Direction = dir;
            //}));
            //Cmd.RegisterCommand(AIFsmCommand.Attack, (Action<bool>)((down) => {
            //    Props.AttackKey = down;
            //}));
            //Cmd.RegisterCommand(AIFsmCommand.Hurt, (Action<IAttacker>)((attacker) => {
            //    Props.HurtKey = true;
            //}));
            //Cmd.RegisterCommand(AIFsmCommand.Death, (Action<bool>)((down) => {
            //    Props.DeathKey = down;
            //}));
            //Cmd.RegisterCommand(AIFsmCommand.Jump, (Action<bool>)((down) => {
            //    Props.JumpKey = down;
            //}));
            //Cmd.RegisterCommand(AIFsmCommand.MoveBoost, (Action<bool>)((down) => {
            //    if (Props.MoveBoostKeyType == IEntityProps.KeySwitchType.Key)
            //        Props.MoveBoostKey = down;
            //    else
            //    if (Props.MoveBoostKeyType == IEntityProps.KeySwitchType.KeyDown && down)
            //        Props.MoveBoostKey = !Props.MoveBoostKey;
            //}));
        }
    }
}
