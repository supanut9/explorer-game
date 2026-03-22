using ExplorerGame.Core;
using ExplorerGame.UI;
using NUnit.Framework;
using UnityEngine;

namespace ExplorerGame.Tests.EditMode
{
    public sealed class CharacterSelectionViewTests
    {
        [Test]
        public void ApplySelection_StoresChosenCharacterAndResetsWorldToVillage()
        {
            var viewObject = new GameObject("CharacterSelectionView");
            var view = viewObject.AddComponent<CharacterSelectionView>();
            var session = GameSession.EnsureInstance();

            session.SelectCharacter(CharacterOption.Male);
            session.SetActiveZone(GameConstants.MountainZoneScene);

            view.ApplySelection(CharacterOption.Female);

            Assert.AreEqual(CharacterOption.Female, session.SelectedCharacter);
            Assert.AreEqual(GameConstants.VillageZoneScene, session.ActiveZoneScene);

            Object.DestroyImmediate(viewObject);
        }
    }
}
