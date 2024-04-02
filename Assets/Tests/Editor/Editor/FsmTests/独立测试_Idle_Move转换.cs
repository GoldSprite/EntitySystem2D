using GoldSprite.UFsm;
using NUnit.Framework;
using System;
using UnityEngine;

namespace GoldSprite.UFsm.Tests {
    public class ��������_Idle_Moveת�� {
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
                Debug.Log("�����ƶ��¼��ɹ�.");

                fsm.UpdateNextState();
                Assert.AreEqual(fsm.GetState<MoveState>(), fsm.CState);
                Debug.Log("->Move״̬ת���ɹ�.");

                fsm.FixedUpdate();
                Assert.That(Math.Abs(fsm.Props.Velocity.x - fsm.Props.Direction.x * fsm.Props.Speed) < 0.05f);
                Debug.Log("�ٶ�Ӧ�óɹ�.");
            }

            {   //move->idle
                dir = Vector2.zero;
                fsm.Command(BaseFsmCommand.Move, dir);
                Assert.That(fsm.Props.Direction == dir);
                Debug.Log("����ֹͣ�¼��ɹ�.");

                fsm.FixedUpdate();
                fsm.UpdateNextState();
                Assert.AreEqual(fsm.GetState<IdleState>(), fsm.CState);
                Debug.Log("->Idle״̬ת���ɹ�.");

                fsm.FixedUpdate();
                Assert.That(Math.Abs(fsm.Props.Velocity.x) < 0.05f);
                Debug.Log("�ٶȾ�ֹӦ�óɹ�.");
            }
        }
    }
}