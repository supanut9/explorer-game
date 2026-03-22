using System;
using System.Collections.Generic;
using ExplorerGame.Core;
using UnityEngine;

namespace ExplorerGame.Player
{
    [CreateAssetMenu(menuName = "Explorer Game/Player/Character Catalog", fileName = "CharacterCatalog")]
    public sealed class CharacterCatalog : ScriptableObject
    {
        [SerializeField] private List<CharacterDefinition> characters = new();

        public IReadOnlyList<CharacterDefinition> Characters => characters;

        public bool TryGet(CharacterOption option, out CharacterDefinition definition)
        {
            foreach (var character in characters)
            {
                if (character != null && character.Option == option)
                {
                    definition = character;
                    return true;
                }
            }

            definition = null;
            return false;
        }

        public void Validate()
        {
            var seen = new HashSet<CharacterOption>();
            foreach (var character in characters)
            {
                if (character == null)
                {
                    throw new InvalidOperationException("Character catalog contains a null entry.");
                }

                if (!seen.Add(character.Option))
                {
                    throw new InvalidOperationException($"Character catalog contains duplicate option '{character.Option}'.");
                }
            }

            if (!seen.Contains(CharacterOption.Male) || !seen.Contains(CharacterOption.Female))
            {
                throw new InvalidOperationException("Character catalog must contain both Male and Female definitions.");
            }
        }
    }
}
