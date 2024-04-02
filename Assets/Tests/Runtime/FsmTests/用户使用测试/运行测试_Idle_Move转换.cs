using GoldSprite.UFsm;
using NUnit.Framework;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

public class ���в���_Idle_Moveת�� : MonoBehaviour{
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
        var t = obj.AddComponent<���в���_Idle_Moveת��>();
        yield return new WaitForSeconds(0.1f);

        var dir = new Vector2(1, 0);
        fsm = t.fsm;

        {   //idle->move
            fsm.Command(BaseFsmCommand.Move, dir);
            Assert.That(fsm.Props.Direction == dir);
            Debug.Log("�����ƶ��¼��ɹ�.");

            yield return new WaitForSeconds(0.1f);
            //fsm.UpdateNextState();
            Assert.AreEqual(fsm.GetState<MoveState>(), fsm.CState);
            Debug.Log("->Move״̬ת���ɹ�.");

            yield return new WaitForSeconds(0.1f);
            //fsm.FixedUpdate();
            Assert.That(Math.Abs(fsm.Props.Velocity.x - fsm.Props.Direction.x * fsm.Props.Speed) < 0.05f);
            Debug.Log("�ٶ�Ӧ�óɹ�.");
        }

        yield return new WaitForSeconds(0.1f);
        {   //move->idle
            dir = Vector2.zero;
            fsm.Command(BaseFsmCommand.Move, dir);
            Assert.That(fsm.Props.Direction == dir);
            Debug.Log("����ֹͣ�¼��ɹ�.");

            yield return new WaitForSeconds(0.1f);
            //fsm.FixedUpdate();
            //fsm.UpdateNextState();
            Assert.AreEqual(fsm.GetState<IdleState>(), fsm.CState);
            Debug.Log("->Idle״̬ת���ɹ�.");

            yield return new WaitForSeconds(0.1f);
            //fsm.FixedUpdate();
            Assert.That(Math.Abs(fsm.Props.Velocity.x) < 0.05f);
            Debug.Log("�ٶȾ�ֹӦ�óɹ�.");
        }
    }
}
