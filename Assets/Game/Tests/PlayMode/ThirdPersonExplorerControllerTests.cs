using System.Collections;
using ExplorerGame.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TestTools;

namespace ExplorerGame.Tests.PlayMode
{
    public sealed class ThirdPersonExplorerControllerTests
    {
        private Keyboard keyboard;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            keyboard = InputSystem.GetDevice<Keyboard>() ?? InputSystem.AddDevice<Keyboard>();
            yield return null;
        }

        [UnityTest]
        public IEnumerator Controller_JumpsFromGroundedState()
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0f, -0.5f, 0f);
            ground.transform.localScale = new Vector3(20f, 1f, 20f);

            var player = new GameObject("Player");
            player.transform.position = new Vector3(0f, 1.05f, 0f);
            player.AddComponent<CharacterController>();
            player.AddComponent<ThirdPersonExplorerController>();

            yield return WaitForFrames(5);
            var groundedY = player.transform.position.y;

            InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.Space));
            InputSystem.Update();
            yield return null;

            InputSystem.QueueStateEvent(keyboard, new KeyboardState());
            InputSystem.Update();

            yield return WaitForFrames(10);

            Assert.Greater(player.transform.position.y, groundedY + 0.05f, "Expected jump input to lift the player above the grounded baseline.");

            Object.Destroy(player);
            Object.Destroy(ground);
        }

        private static IEnumerator WaitForFrames(int frameCount)
        {
            for (var i = 0; i < frameCount; i++)
            {
                yield return null;
            }
        }
    }
}
