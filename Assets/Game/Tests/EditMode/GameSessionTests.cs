using ExplorerGame.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.Tests.EditMode
{
    public sealed class GameSessionTests
    {
        [Test]
        public void EnsureInstance_CreatesSingleton()
        {
            var session = GameSession.EnsureInstance();

            Assert.IsNotNull(session);
            Assert.IsTrue(GameSession.HasInstance);
        }

        [Test]
        public void SessionState_PersistsAcrossSceneActivationChanges()
        {
            var session = GameSession.EnsureInstance();
            session.ResetState();
            session.SelectCharacter(CharacterOption.Female);
            session.SetActiveZone(GameConstants.ForestZoneScene);

            var transitionScene = SceneManager.CreateScene("SessionTransitionTest");
            SceneManager.SetActiveScene(transitionScene);

            Assert.AreEqual(CharacterOption.Female, GameSession.Instance.SelectedCharacter);
            Assert.AreEqual(GameConstants.ForestZoneScene, GameSession.Instance.ActiveZoneScene);
        }

        [Test]
        public void SelectCharacter_UpdatesSelection()
        {
            var session = GameSession.EnsureInstance();
            session.ResetState();

            session.SelectCharacter(CharacterOption.Female);

            Assert.AreEqual(CharacterOption.Female, session.SelectedCharacter);
        }

        [Test]
        public void ResetState_ReturnsDefaultValues()
        {
            var session = GameSession.EnsureInstance();
            session.SelectCharacter(CharacterOption.Female);
            session.SetActiveZone(GameConstants.MountainZoneScene);

            session.ResetState();

            Assert.AreEqual(CharacterOption.Male, session.SelectedCharacter);
            Assert.AreEqual(GameConstants.VillageZoneScene, session.ActiveZoneScene);
        }

        [TearDown]
        public void TearDown()
        {
            if (GameSession.HasInstance)
            {
                Object.DestroyImmediate(GameSession.Instance.gameObject);
            }
        }
    }
}
