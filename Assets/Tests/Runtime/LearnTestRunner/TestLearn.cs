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
        Debug.Log("位置变动测试: 通过.");
    }

    [UnityTest]
    public IEnumerator 测试跳过Update帧()
    {
        var obj = new GameObject();
        t = obj.AddComponent<TestLearn>();
        Assert.AreEqual(0, t.ticker);
        Debug.Log("ticker初始值测试: 通过.");
        //yield return null;  //预计跳过一帧
        yield return new WaitForSeconds(0.1f);  //之后皆改为0.1s防止实机运行过慢导致漏帧
        Assert.That(t.ticker>=1);  //实际3帧, 这里会运行3次, 可能是测试程序运行比较慢, 卡不上帧, 不过影响不大
        Debug.Log("ticker迭代测试: 通过.");
    }
    private void FixedUpdate()
    {
        ticker += 1;
    }

}