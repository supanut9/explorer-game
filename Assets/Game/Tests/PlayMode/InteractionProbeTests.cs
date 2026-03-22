using System.Collections;
using ExplorerGame.Interaction;
using ExplorerGame.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ExplorerGame.Tests.PlayMode
{
    public sealed class InteractionProbeTests
    {
        [UnityTest]
        public IEnumerator Probe_SelectsNearestTarget_UpdatesPrompt_AndTriggersInteraction()
        {
            var canvasObject = new GameObject("Canvas", typeof(Canvas));
            var labelObject = new GameObject("PromptLabel", typeof(Text), typeof(InteractionPromptLabel));
            labelObject.transform.SetParent(canvasObject.transform);

            var probeObject = new GameObject("Probe");
            var probe = probeObject.AddComponent<InteractionProbe>();

            var farObject = new GameObject("FarInspectable", typeof(BoxCollider), typeof(InspectableObject));
            farObject.transform.position = new Vector3(2f, 0f, 0f);

            var nearObject = new GameObject("NearNpc", typeof(BoxCollider), typeof(DialogueNpc));
            nearObject.transform.position = new Vector3(1f, 0f, 0f);

            yield return null;

            Assert.IsInstanceOf<DialogueNpc>(probe.CurrentTarget);
            Assert.AreEqual("Press E: Talk", labelObject.GetComponent<Text>().text);

            LogAssert.Expect(LogType.Log, "Hello, explorer.");
            Assert.IsTrue(probe.TriggerCurrentTarget());

            Object.Destroy(canvasObject);
            Object.Destroy(probeObject);
            Object.Destroy(farObject);
            Object.Destroy(nearObject);
        }
    }
}
