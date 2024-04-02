using GoldSprite.UFsm;
using NUnit.Framework;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

public class 运行测试_Idle_Move转换 : MonoBehaviour{
    public BaseProps props;
    public BaseFsm fsm;


    public void Start()
    {
        props = new BaseProps() { Name = "A" };
        fsm = new BaseFsm(props);
        fsm.Start();
    }

    public void Update()
    {
        fsm.Update();
    }

    public void FixedUpdate()
    {
        fsm.FixedUpdate();
    }


    [UnityTest]
    public IEnumerator Test1()
    {
        var obj = new GameObject();
        var t = obj.AddComponent<运行测试_Idle_Move转换>();
        yield return new WaitForSeconds(0.1f);

        var dir = new Vector2(1, 0);
        fsm = t.fsm;

        {   //idle->move
            fsm.Command(BaseFsmCommand.Move, dir);
            Assert.That(fsm.Props.Direction == dir);
            Debug.Log("方向移动事件成功.");

            yield return new WaitForSeconds(0.1f);
            //fsm.UpdateNextState();
            Assert.AreEqual(fsm.GetState<MoveState>(), fsm.CState);
            Debug.Log("->Move状态转换成功.");

            yield return new WaitForSeconds(0.1f);
            //fsm.FixedUpdate();
            Assert.That(Math.Abs(fsm.Props.Velocity.x - fsm.Props.Direction.x * fsm.Props.Speed) < 0.05f);
            Debug.Log("速度应用成功.");
        }

        yield return new WaitForSeconds(0.1f);
        {   //move->idle
            dir = Vector2.zero;
            fsm.Command(BaseFsmCommand.Move, dir);
            Assert.That(fsm.Props.Direction == dir);
            Debug.Log("方向停止事件成功.");

            yield return new WaitForSeconds(0.1f);
            //fsm.FixedUpdate();
            //fsm.UpdateNextState();
            Assert.AreEqual(fsm.GetState<IdleState>(), fsm.CState);
            Debug.Log("->Idle状态转换成功.");

            yield return new WaitForSeconds(0.1f);
            //fsm.FixedUpdate();
            Assert.That(Math.Abs(fsm.Props.Velocity.x) < 0.05f);
            Debug.Log("速度静止应用成功.");
        }
    }
}
