using GoldSprite.UFsm;
using NUnit.Framework;
using System;
using UnityEngine;

namespace GoldSprite.UFsm.Tests {
    public class 独立测试_Idle_Move转换 {
        [Test]
        public static void Test1()
        {
            var props = new BaseProps() { Name = "A" };
            var fsm = new BaseFsm(props);
            fsm.Start();
            var dir = new Vector2(1, 0);

            {   //idle->move
                fsm.Command(BaseFsmCommand.Move, dir);
                Assert.That(fsm.Props.Direction == dir);
                Debug.Log("方向移动事件成功.");

                fsm.UpdateNextState();
                Assert.AreEqual(fsm.GetState<MoveState>(), fsm.CState);
                Debug.Log("->Move状态转换成功.");

                fsm.FixedUpdate();
                Assert.That(Math.Abs(fsm.Props.Velocity.x - fsm.Props.Direction.x * fsm.Props.Speed) < 0.05f);
                Debug.Log("速度应用成功.");
            }

            {   //move->idle
                dir = Vector2.zero;
                fsm.Command(BaseFsmCommand.Move, dir);
                Assert.That(fsm.Props.Direction == dir);
                Debug.Log("方向停止事件成功.");

                fsm.FixedUpdate();
                fsm.UpdateNextState();
                Assert.AreEqual(fsm.GetState<IdleState>(), fsm.CState);
                Debug.Log("->Idle状态转换成功.");

                fsm.FixedUpdate();
                Assert.That(Math.Abs(fsm.Props.Velocity.x) < 0.05f);
                Debug.Log("速度静止应用成功.");
            }
        }
    }
}