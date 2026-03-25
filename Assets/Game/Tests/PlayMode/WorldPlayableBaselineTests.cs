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
        private const string VillageNpcPrompt = "The forest trail is straight ahead. Follow the marked path past the sign and step through the green arch.";
        private const string ForestMarkerDescription = "The air is cooler here. Animal tracks cut between the trees and the trail bends back toward the village arch.";
        private const string MountainMarkerDescription = "The higher path opens onto a quiet lookout. The mountain route is still rough, but the ridge is visible from here.";

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

            AssertSinglePlayerAndAudioListener("after world startup");
        }

        [UnityTest]
        public IEnumerator WorldStartup_AllowsReachableNpcInteraction()
        {
            yield return WaitForFrames(5);

            var npc = Object.FindAnyObjectByType<DialogueNpc>();
            Assert.IsNotNull(npc, "Expected a reachable dialogue NPC in the generated world.");

            yield return ExpectReachableInteractionTarget(
                npc,
                VillageNpcPrompt,
                "Expected the nearest interaction target to resolve to the village NPC.");
        }

        [UnityTest]
        public IEnumerator WorldTraversal_ReachesForest_AndAllowsDestinationInteraction()
        {
            yield return WaitForFrames(5);

            var villagePortal = FindPortalInScene(GameConstants.VillageZoneScene, GameConstants.ForestZoneScene);
            Assert.IsNotNull(villagePortal, "Expected a traversal portal in the village scene.");

            villagePortal.TravelAsync();
            yield return WaitForFrames(10);

            Assert.AreEqual(GameConstants.ForestZoneScene, GameSession.Instance.ActiveZoneScene, "Expected the active zone to switch to the forest after portal travel.");
            Assert.IsTrue(SceneManager.GetSceneByName(GameConstants.ForestZoneScene).isLoaded, "Expected the forest scene to be loaded after traversal.");
            Assert.IsFalse(SceneManager.GetSceneByName(GameConstants.VillageZoneScene).isLoaded, "Expected the village scene to unload after traversal.");
            AssertSinglePlayerAndAudioListener("after village-to-forest traversal");

            var forestMarker = Object.FindAnyObjectByType<InspectableObject>();
            Assert.IsNotNull(forestMarker, "Expected a reachable inspectable in the forest scene.");

            yield return ExpectReachableInteractionTarget(
                forestMarker,
                ForestMarkerDescription,
                "Expected the forest inspectable to become the active target after traversal.");
        }

        [UnityTest]
        public IEnumerator WorldTraversal_ReturnsToVillage_AndKeepsNpcInteractionStable()
        {
            yield return WaitForFrames(5);

            var villagePortal = FindPortalInScene(GameConstants.VillageZoneScene, GameConstants.ForestZoneScene);
            Assert.IsNotNull(villagePortal, "Expected a traversal portal in the village scene.");

            villagePortal.TravelAsync();
            yield return WaitForFrames(10);

            var forestReturnPortal = FindPortalInScene(GameConstants.ForestZoneScene, GameConstants.VillageZoneScene);
            Assert.IsNotNull(forestReturnPortal, "Expected a return portal in the forest scene.");

            forestReturnPortal.TravelAsync();
            yield return WaitForFrames(10);

            Assert.AreEqual(GameConstants.VillageZoneScene, GameSession.Instance.ActiveZoneScene, "Expected the active zone to switch back to the village after return travel.");
            Assert.IsTrue(SceneManager.GetSceneByName(GameConstants.VillageZoneScene).isLoaded, "Expected the village scene to be loaded after return traversal.");
            Assert.IsFalse(SceneManager.GetSceneByName(GameConstants.ForestZoneScene).isLoaded, "Expected the forest scene to unload after return traversal.");
            AssertSinglePlayerAndAudioListener("after forest-to-village return");

            var npc = Object.FindAnyObjectByType<DialogueNpc>();
            Assert.IsNotNull(npc, "Expected the village NPC to be reachable again after returning from the forest.");

            yield return ExpectReachableInteractionTarget(
                npc,
                VillageNpcPrompt,
                "Expected the village NPC to become the active target again after returning from the forest.");
        }

        [UnityTest]
        public IEnumerator WorldTraversal_ReachesMountain_AndAllowsDestinationInteraction()
        {
            yield return WaitForFrames(5);

            var villagePortal = FindPortalInScene(GameConstants.VillageZoneScene, GameConstants.MountainZoneScene);
            Assert.IsNotNull(villagePortal, "Expected a mountain traversal portal in the village scene.");

            villagePortal.TravelAsync();
            yield return WaitForFrames(10);

            Assert.AreEqual(GameConstants.MountainZoneScene, GameSession.Instance.ActiveZoneScene, "Expected the active zone to switch to the mountain after portal travel.");
            Assert.IsTrue(SceneManager.GetSceneByName(GameConstants.MountainZoneScene).isLoaded, "Expected the mountain scene to be loaded after traversal.");
            Assert.IsFalse(SceneManager.GetSceneByName(GameConstants.VillageZoneScene).isLoaded, "Expected the village scene to unload after traversal.");
            AssertSinglePlayerAndAudioListener("after village-to-mountain traversal");

            var mountainMarker = Object.FindAnyObjectByType<InspectableObject>();
            Assert.IsNotNull(mountainMarker, "Expected a reachable inspectable in the mountain scene.");

            yield return ExpectReachableInteractionTarget(
                mountainMarker,
                MountainMarkerDescription,
                "Expected the mountain inspectable to become the active target after traversal.");
        }

        [UnityTest]
        public IEnumerator WorldTraversal_ReturnsFromMountain_AndKeepsNpcInteractionStable()
        {
            yield return WaitForFrames(5);

            var villagePortal = FindPortalInScene(GameConstants.VillageZoneScene, GameConstants.MountainZoneScene);
            Assert.IsNotNull(villagePortal, "Expected a mountain traversal portal in the village scene.");

            villagePortal.TravelAsync();
            yield return WaitForFrames(10);

            var mountainReturnPortal = FindPortalInScene(GameConstants.MountainZoneScene, GameConstants.VillageZoneScene);
            Assert.IsNotNull(mountainReturnPortal, "Expected a return portal in the mountain scene.");

            mountainReturnPortal.TravelAsync();
            yield return WaitForFrames(10);

            Assert.AreEqual(GameConstants.VillageZoneScene, GameSession.Instance.ActiveZoneScene, "Expected the active zone to switch back to the village after mountain return travel.");
            Assert.IsTrue(SceneManager.GetSceneByName(GameConstants.VillageZoneScene).isLoaded, "Expected the village scene to be loaded after mountain return traversal.");
            Assert.IsFalse(SceneManager.GetSceneByName(GameConstants.MountainZoneScene).isLoaded, "Expected the mountain scene to unload after return traversal.");
            AssertSinglePlayerAndAudioListener("after mountain-to-village return");

            var npc = Object.FindAnyObjectByType<DialogueNpc>();
            Assert.IsNotNull(npc, "Expected the village NPC to be reachable again after returning from the mountain.");

            yield return ExpectReachableInteractionTarget(
                npc,
                VillageNpcPrompt,
                "Expected the village NPC to become the active target again after returning from the mountain.");
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

        private static void AssertSinglePlayerAndAudioListener(string context)
        {
            var players = Object.FindObjectsByType<ThirdPersonExplorerController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            Assert.AreEqual(1, players.Length, $"Expected exactly one spawned player {context}.");

            var listeners = Object.FindObjectsByType<AudioListener>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            var enabledListeners = 0;
            foreach (var listener in listeners)
            {
                if (listener != null && listener.enabled && listener.gameObject.activeInHierarchy)
                {
                    enabledListeners++;
                }
            }

            Assert.AreEqual(1, enabledListeners, $"Expected exactly one active audio listener {context}.");
        }

        private static IEnumerator ExpectReachableInteractionTarget(Component target, string expectedLog, string targetMessage)
        {
            var player = Object.FindAnyObjectByType<ThirdPersonExplorerController>();
            Assert.IsNotNull(player, "Expected a spawned player controller.");

            var probe = player.GetComponent<InteractionProbe>();
            Assert.IsNotNull(probe, "Expected the spawned player to have an interaction probe.");

            player.transform.position = target.transform.position + new Vector3(0.25f, 0f, 0f);
            yield return WaitForFrames(2);

            Assert.AreSame(target, probe.CurrentTarget, targetMessage);

            LogAssert.Expect(LogType.Log, expectedLog);
            Assert.IsTrue(probe.TriggerCurrentTarget(), "Expected the probe to trigger the current interaction target.");
        }

        private static ZonePortal FindPortalInScene(string sceneName, string targetZoneScene)
        {
            var portals = Object.FindObjectsByType<ZonePortal>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var portal in portals)
            {
                if (portal != null &&
                    portal.gameObject.scene.name == sceneName &&
                    portal.TargetZoneScene == targetZoneScene)
                {
                    return portal;
                }
            }

            return null;
        }
    }
}
