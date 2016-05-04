// <copyright file="CharacterFactory.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Generate unique characters
    /// </summary>
    public sealed class CharacterFactory
    {
        #region Fields

        /// <summary>
        /// All profiles
        /// </summary>
        private static List<CharacterProfile> allProfiles = new List<CharacterProfile>();

        /// <summary>
        /// The active character profiles
        /// </summary>
        private static List<CharacterProfile> activeCharacterProfiles = new List<CharacterProfile>();

        /// <summary>
        /// The available character profiles
        /// </summary>
        private static List<CharacterProfile> availableCharacterProfiles = new List<CharacterProfile>();

        /// <summary>
        /// The characters
        /// </summary>
        private static List<Character> characters = new List<Character>();

        /// <summary>
        /// The last character identifier
        /// </summary>
        private static int lastCharacterId = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets all characters.
        /// </summary>
        /// <value>
        /// All characters.
        /// </value>
        public static IEnumerable<Character> AllCharacters
        {
            get
            {
                return characters;
            }
        }

        /// <summary>
        /// Gets the total number of characters.
        /// </summary>
        /// <value>
        /// The total number of characters.
        /// </value>
        public static int TotalNumberOfCharacters
        {
            get
            {
                if (allProfiles.Count == 0)
                {
                    PopulateLists();
                }

                return allProfiles.Count;
            }
        }

        /// <summary>
        /// Gets the number of active characters.
        /// </summary>
        /// <value>
        /// The number of active characters.
        /// </value>
        public static int NumberOfActiveCharacters
        {
            get
            {
                return activeCharacterProfiles.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can spawn character.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can spawn character; otherwise, <c>false</c>.
        /// </value>
        public static bool CanSpawnCharacter
        {
            get
            {
                if (allProfiles.Count == 0)
                {
                    PopulateLists();
                }

                return availableCharacterProfiles.Count > 0;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the character.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns>the created character</returns>
        public static Character CreateCharacter(Transform transform)
        {
            if (allProfiles.Count == 0)
            {
                PopulateLists();
            }

            if (availableCharacterProfiles.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, availableCharacterProfiles.Count - 1);

                CharacterProfile profile = availableCharacterProfiles[index];
                availableCharacterProfiles.RemoveAt(index);
                activeCharacterProfiles.Add(profile);

                Character character = new Character(transform, lastCharacterId++, profile);
                characters.Add(character);

                return character;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Removes the character. Should only be used by the Character code it self
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns><c>true</c> if the character was removed successfully</returns>
        public static bool RemoveCharacter(Character character)
        {
            if (characters.Remove(character))
            {
                activeCharacterProfiles.Remove(character.Profile);

                availableCharacterProfiles.Add(character.Profile);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a random active character.
        /// </summary>
        /// <returns>A random active character</returns>
        public static Character GetRandomActiveCharacter()
        {
            return characters[UnityEngine.Random.Range(0, characters.Count - 1)];
        }

        /// <summary>
        /// Populates the lists.
        /// </summary>
        private static void PopulateLists()
        {
            UnityEngine.Object[] profiles = Resources.LoadAll("Characters");

            foreach (UnityEngine.Object obj in profiles)
            {
                CharacterProfile profile = obj as CharacterProfile;
                allProfiles.Add(profile);
                availableCharacterProfiles.Add(profile);
            }
        }

        #endregion Methods
    }
}