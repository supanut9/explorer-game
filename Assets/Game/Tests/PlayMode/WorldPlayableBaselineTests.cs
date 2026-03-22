using System.Collections;
using ExplorerGame.Core;
using ExplorerGame.Interaction;
using ExplorerGame.Player;
using ExplorerGame.World;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ExplorerGame.Tests.PlayMode
{
    public sealed class WorldPlayableBaselineTests
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return LoadSceneAsync(GameConstants.BootstrapScene);

            var session = GameSession.EnsureInstance();
            session.ResetState();
            session.SelectCharacter(CharacterOption.Male);
            session.SetActiveZone(GameConstants.VillageZoneScene);

            yield return LoadSceneAsync(GameConstants.WorldPersistentScene);
            yield return WaitForFrames(5);
        }

        [UnityTest]
        public IEnumerator WorldStartup_SpawnsOnePlayer_AndOneAudioListener()
        {
            yield return WaitForFrames(5);

            var players = Object.FindObjectsByType<ThirdPersonExplorerController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            Assert.AreEqual(1, players.Length, "Expected exactly one spawned player in the world flow.");

            var listeners = Object.FindObjectsByType<AudioListener>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            var enabledListeners = 0;
            foreach (var listener in listeners)
            {
                if (listener != null && listener.enabled && listener.gameObject.activeInHierarchy)
                {
                    enabledListeners++;
                }
            }

            Assert.AreEqual(1, enabledListeners, "Expected exactly one active audio listener after world startup.");
        }

        [UnityTest]
        public IEnumerator WorldStartup_AllowsReachableNpcInteraction()
        {
            yield return WaitForFrames(5);

            var player = Object.FindAnyObjectByType<ThirdPersonExplorerController>();
            Assert.IsNotNull(player, "Expected a spawned player controller.");

            var probe = player.GetComponent<InteractionProbe>();
            Assert.IsNotNull(probe, "Expected the spawned player to have an interaction probe.");

            var npc = Object.FindAnyObjectByType<DialogueNpc>();
            Assert.IsNotNull(npc, "Expected a reachable dialogue NPC in the generated world.");

            player.transform.position = npc.transform.position + new Vector3(0.25f, 0f, 0f);
            yield return WaitForFrames(2);

            Assert.AreSame(npc, probe.CurrentTarget, "Expected the nearest interaction target to resolve to the village NPC.");

            LogAssert.Expect(LogType.Log, "The forest trail is straight ahead. Follow the marked path past the sign and step through the green arch.");
            Assert.IsTrue(probe.TriggerCurrentTarget(), "Expected the probe to trigger the current NPC target.");
        }

        [UnityTest]
        public IEnumerator WorldTraversal_ReachesForest_AndAllowsDestinationInteraction()
        {
            yield return WaitForFrames(5);

            var player = Object.FindAnyObjectByType<ThirdPersonExplorerController>();
            Assert.IsNotNull(player, "Expected a spawned player controller before traversal.");

            var probe = player.GetComponent<InteractionProbe>();
            Assert.IsNotNull(probe, "Expected the spawned player to have an interaction probe.");

            var villagePortal = FindPortalInScene(GameConstants.VillageZoneScene);
            Assert.IsNotNull(villagePortal, "Expected a traversal portal in the village scene.");

            villagePortal.TravelAsync();
            yield return WaitForFrames(10);

            Assert.AreEqual(GameConstants.ForestZoneScene, GameSession.Instance.ActiveZoneScene, "Expected the active zone to switch to the forest after portal travel.");
            Assert.IsTrue(SceneManager.GetSceneByName(GameConstants.ForestZoneScene).isLoaded, "Expected the forest scene to be loaded after traversal.");
            Assert.IsFalse(SceneManager.GetSceneByName(GameConstants.VillageZoneScene).isLoaded, "Expected the village scene to unload after traversal.");

            player = Object.FindAnyObjectByType<ThirdPersonExplorerController>();
            Assert.IsNotNull(player, "Expected a spawned player controller after traversal.");

            probe = player.GetComponent<InteractionProbe>();
            Assert.IsNotNull(probe, "Expected the respawned player to keep an interaction probe after traversal.");

            var forestMarker = Object.FindAnyObjectByType<InspectableObject>();
            Assert.IsNotNull(forestMarker, "Expected a reachable inspectable in the forest scene.");

            player.transform.position = forestMarker.transform.position + new Vector3(0.25f, 0f, 0f);
            yield return WaitForFrames(2);

            Assert.AreSame(forestMarker, probe.CurrentTarget, "Expected the forest inspectable to become the active target after traversal.");

            LogAssert.Expect(LogType.Log, "The air is cooler here. Animal tracks cut between the trees and the trail bends back toward the village arch.");
            Assert.IsTrue(probe.TriggerCurrentTarget(), "Expected the probe to trigger the forest inspectable target.");
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return LoadSceneAsync(GameConstants.BootstrapScene);

            if (GameSession.HasInstance)
            {
                Object.Destroy(GameSession.Instance.gameObject);
            }

            yield return WaitForFrames(1);
        }

        private static IEnumerator LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            Assert.IsNotNull(operation, $"Expected to start loading scene '{sceneName}'.");
            while (!operation.isDone)
            {
                yield return null;
            }
        }

        private static IEnumerator WaitForFrames(int frameCount)
        {
            for (var i = 0; i < frameCount; i++)
            {
                yield return null;
            }
        }

        private static ZonePortal FindPortalInScene(string sceneName)
        {
            var portals = Object.FindObjectsByType<ZonePortal>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var portal in portals)
            {
                if (portal != null && portal.gameObject.scene.name == sceneName)
                {
                    return portal;
                }
            }

            return null;
        }
    }
}
