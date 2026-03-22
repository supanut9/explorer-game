using ExplorerGame.Core;
using ExplorerGame.Interaction;
using ExplorerGame.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.World
{
    public sealed class WorldRuntimeController : MonoBehaviour
    {
        private static readonly Vector3 SpawnSafetyOffset = new(0f, 0.15f, 0f);

        [SerializeField] private CharacterCatalog characterCatalog;
        [SerializeField] private WorldCatalog worldCatalog;

        private GameObject spawnedPlayer;

        private async void Start()
        {
            var session = GameSession.EnsureInstance();
            characterCatalog ??= Resources.Load<CharacterCatalog>(GameConstants.CharacterConfigPath);
            worldCatalog ??= Resources.Load<WorldCatalog>(GameConstants.WorldConfigPath);

            if (worldCatalog == null)
            {
                Debug.LogError("WorldCatalog is missing. Run Tools/Explorer Game/Generate Project Scaffolding.");
                return;
            }

            var sceneName = session.ActiveZoneScene;
            if (!worldCatalog.TryGetZone(sceneName, out var zone))
            {
                Debug.LogError($"Zone '{sceneName}' was not found in WorldCatalog.");
                return;
            }

            await UnloadInactiveZonesAsync(zone.SceneName);

            if (!SceneManager.GetSceneByName(zone.SceneName).isLoaded)
            {
                var operation = SceneManager.LoadSceneAsync(zone.SceneName, LoadSceneMode.Additive);
                while (operation != null && !operation.isDone)
                {
                    await Awaitable.NextFrameAsync();
                }
            }

            NormalizeWorldCameras();
            SpawnPlayer(session.SelectedCharacter, zone);
        }

        private void SpawnPlayer(CharacterOption option, ZoneDefinition zone)
        {
            if (characterCatalog == null)
            {
                Debug.LogWarning("CharacterCatalog is missing. Player spawn skipped.");
                return;
            }

            if (!characterCatalog.TryGet(option, out var definition) || definition.Prefab == null)
            {
                Debug.LogWarning($"Character '{option}' is missing a prefab. Assign one in CharacterCatalog.");
                return;
            }

            DestroyExistingPlayers();

            var spawnPosition = zone.PlayerSpawnPoint + definition.SpawnOffset + SpawnSafetyOffset;
            spawnedPlayer = Instantiate(definition.Prefab, spawnPosition, Quaternion.identity);
            if (spawnedPlayer.GetComponent<InteractionProbe>() == null)
            {
                spawnedPlayer.AddComponent<InteractionProbe>();
            }

            var cameraRig = FindAnyObjectByType<ThirdPersonCameraRig>();
            if (cameraRig != null)
            {
                cameraRig.SetTarget(spawnedPlayer.transform);
                var controller = spawnedPlayer.GetComponent<ThirdPersonExplorerController>();
                if (controller != null)
                {
                    controller.SetMovementReference(cameraRig.transform);
                }
            }
        }

        private void DestroyExistingPlayers()
        {
            foreach (var controller in FindObjectsByType<ThirdPersonExplorerController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                if (controller != null)
                {
                    Destroy(controller.gameObject);
                }
            }

            spawnedPlayer = null;
        }

        private void NormalizeWorldCameras()
        {
            var cameraRigs = FindObjectsByType<ThirdPersonCameraRig>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            ThirdPersonCameraRig cameraRig = null;
            foreach (var candidate in cameraRigs)
            {
                if (candidate == null)
                {
                    continue;
                }

                if (cameraRig == null)
                {
                    cameraRig = candidate;
                    continue;
                }

                Destroy(candidate.gameObject);
            }

            if (cameraRig == null)
            {
                return;
            }

            var rigCamera = cameraRig.GetComponent<Camera>();
            if (rigCamera != null)
            {
                rigCamera.enabled = true;
                rigCamera.tag = "MainCamera";
            }

            var rigListener = cameraRig.GetComponent<AudioListener>();
            if (rigListener != null)
            {
                rigListener.enabled = true;
            }

            var cameras = FindObjectsByType<Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var camera in cameras)
            {
                if (camera == null || camera == rigCamera)
                {
                    continue;
                }

                camera.enabled = false;
                var listener = camera.GetComponent<AudioListener>();
                if (listener != null)
                {
                    listener.enabled = false;
                }
            }
        }

        private async Awaitable UnloadInactiveZonesAsync(string activeZoneScene)
        {
            foreach (var zone in worldCatalog.Zones)
            {
                if (zone == null || zone.SceneName == activeZoneScene)
                {
                    continue;
                }

                var loadedScene = SceneManager.GetSceneByName(zone.SceneName);
                if (!loadedScene.isLoaded)
                {
                    continue;
                }

                var operation = SceneManager.UnloadSceneAsync(zone.SceneName);
                while (operation != null && !operation.isDone)
                {
                    await Awaitable.NextFrameAsync();
                }
            }
        }
    }
}
