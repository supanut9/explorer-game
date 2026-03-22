using ExplorerGame.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.UI
{
    public sealed class CharacterSelectionView : MonoBehaviour
    {
        private GUIStyle panelStyle;
        private GUIStyle eyebrowStyle;
        private GUIStyle titleStyle;
        private GUIStyle bodyStyle;
        private GUIStyle cardStyle;
        private GUIStyle hintStyle;
        private Texture2D panelTexture;
        private Texture2D cardTexture;

        public void SelectMale()
        {
            Select(CharacterOption.Male);
        }

        public void SelectFemale()
        {
            Select(CharacterOption.Female);
        }

        public void ApplySelection(CharacterOption option)
        {
            var session = GameSession.EnsureInstance();
            session.SelectCharacter(option);
            session.SetActiveZone(GameConstants.VillageZoneScene);
        }

        private async void Select(CharacterOption option)
        {
            ApplySelection(option);

            var operation = SceneManager.LoadSceneAsync(GameConstants.WorldPersistentScene, LoadSceneMode.Single);
            while (operation != null && !operation.isDone)
            {
                await Awaitable.NextFrameAsync();
            }
        }

        private void OnGUI()
        {
            EnsureStyles();

            var panelWidth = Mathf.Min(Screen.width - 48f, 560f);
            var panelHeight = 292f;
            var panelRect = new Rect(
                (Screen.width - panelWidth) * 0.5f,
                Mathf.Max(24f, Screen.height * 0.16f),
                panelWidth,
                panelHeight);

            GUILayout.BeginArea(panelRect, panelStyle);
            GUILayout.Label("connected exploration slice", eyebrowStyle);
            GUILayout.Space(4f);
            GUILayout.Label("Choose Your Explorer", titleStyle);
            GUILayout.Space(6f);
            GUILayout.Label(
                "Start in the village and follow the marked route into the forest. Pick the silhouette you want to pilot through the first playable.",
                bodyStyle);
            GUILayout.Space(20f);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Male Explorer\nsteady silhouette\nforest route ready", cardStyle, GUILayout.Height(96f)))
            {
                SelectMale();
            }

            GUILayout.Space(16f);

            if (GUILayout.Button("Female Explorer\nwarm palette\nforest route ready", cardStyle, GUILayout.Height(96f)))
            {
                SelectFemale();
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(18f);
            GUILayout.Label("Controls: WASD move, mouse look, Left Shift sprint, E interact", hintStyle);
            GUILayout.EndArea();
        }

        private void EnsureStyles()
        {
            if (panelStyle != null)
            {
                return;
            }

            panelTexture = BuildTexture(new Color(0.12f, 0.13f, 0.11f, 0.94f));
            cardTexture = BuildTexture(new Color(0.23f, 0.22f, 0.18f, 0.96f));

            panelStyle = new GUIStyle(GUI.skin.box)
            {
                normal = { background = panelTexture, textColor = new Color(0.92f, 0.9f, 0.82f) },
                padding = new RectOffset(24, 24, 22, 22),
                border = new RectOffset(12, 12, 12, 12)
            };

            eyebrowStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12,
                fontStyle = FontStyle.Bold,
                normal = { textColor = new Color(0.73f, 0.67f, 0.42f) }
            };

            titleStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 26,
                fontStyle = FontStyle.Bold,
                wordWrap = true,
                normal = { textColor = new Color(0.96f, 0.94f, 0.89f) }
            };

            bodyStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = 13,
                wordWrap = true,
                normal = { textColor = new Color(0.82f, 0.82f, 0.76f) }
            };

            cardStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15,
                fontStyle = FontStyle.Bold,
                wordWrap = true,
                normal =
                {
                    background = cardTexture,
                    textColor = new Color(0.95f, 0.93f, 0.88f)
                },
                hover =
                {
                    background = cardTexture,
                    textColor = new Color(1f, 0.97f, 0.84f)
                },
                active =
                {
                    background = cardTexture,
                    textColor = new Color(1f, 0.97f, 0.84f)
                },
                padding = new RectOffset(14, 14, 18, 18)
            };

            hintStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12,
                wordWrap = true,
                normal = { textColor = new Color(0.72f, 0.72f, 0.67f) }
            };
        }

        private static Texture2D BuildTexture(Color color)
        {
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        private void OnDestroy()
        {
            if (panelTexture != null)
            {
                Destroy(panelTexture);
            }

            if (cardTexture != null)
            {
                Destroy(cardTexture);
            }
        }
    }
}
