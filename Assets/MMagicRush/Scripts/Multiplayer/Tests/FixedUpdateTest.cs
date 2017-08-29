using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class FixedUpdateTest {

    private Vector2 initial = new Vector2(0, -12);
    private Vector2 target = new Vector2(9, 15);

	[Test]
	public void FixedUpdateTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator MoveTowardsTest() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        while (initial != target) {
            initial = Vector2.MoveTowards(initial, target, Time.fixedDeltaTime * 1.25f);
            Debug.Log(initial);
            yield return new WaitForFixedUpdate();
        }

        Assert.AreEqual(target, initial);
	}
    
}
