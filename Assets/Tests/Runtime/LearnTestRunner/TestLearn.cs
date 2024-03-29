using UnityEngine.TestTools;
using UnityEngine;
using NUnit.Framework;
using System.Collections;

public class TestLearn : MonoBehaviour {
    public int ticker = 0;
    private TestLearn t;

    [UnityTest]
    public IEnumerator GameObject_WithRigidBody_WillBeAffectedByPhysics()
    {
        var go = new GameObject();
        go.AddComponent<Rigidbody>();
        var originalPosition = go.transform.position.y;

        yield return new WaitForFixedUpdate();

        Assert.AreNotEqual(originalPosition, go.transform.position.y);
        Debug.Log("λ�ñ䶯����: ͨ��.");
    }

    [UnityTest]
    public IEnumerator ��������Update֡()
    {
        var obj = new GameObject();
        t = obj.AddComponent<TestLearn>();
        Assert.AreEqual(0, t.ticker);
        Debug.Log("ticker��ʼֵ����: ͨ��.");
        //yield return null;  //Ԥ������һ֡
        yield return new WaitForSeconds(0.1f);  //֮��Ը�Ϊ0.1s��ֹʵ�����й�������©֡
        Assert.That(t.ticker>=1);  //ʵ��3֡, ���������3��, �����ǲ��Գ������бȽ���, ������֡, ����Ӱ�첻��
        Debug.Log("ticker��������: ͨ��.");
    }
    private void FixedUpdate()
    {
        ticker += 1;
    }

}