// <copyright file="CharacterGenerator.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.ViewModel
{
    using Assets.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Temp Character Generator, creates random character profiles
    /// </summary>
    [ExecuteInEditMode]
    public sealed class CharacterGenerator : MonoBehaviour
    {
        #region Methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            if (!Application.isPlaying)
            {
                if (Resources.LoadAll("Characters").Length == 0)
                {
                    this.GenerateCharacterProfiles();
                }
            }
        }

        /// <summary>
        /// Generates the character profiles.
        /// </summary>
        private void GenerateCharacterProfiles()
        {
            HeadGear[] headGears = Enum.GetValues(typeof(HeadGear)) as HeadGear[];
            Ethnicity[] ethnicities = Enum.GetValues(typeof(Ethnicity)) as Ethnicity[];

            StreamReader reader = File.OpenText("MaleNames.txt");

            string name = reader.ReadLine();

            while (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                CharacterProfile profile = ScriptableObject.CreateInstance<CharacterProfile>();
                profile.Initialize(
                    Gender.Male,
                    this.GenerateEthnicity(ethnicities),
                    this.GenerateHeadGear(headGears),
                    name,
                    new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value),
                    UnityEngine.Random.Range(1.0f, 3.0f));

                AssetDatabase.CreateAsset(profile, string.Format("Assets/Resources/Characters/{0}.asset", name));

                name = reader.ReadLine();
            }

            reader.Dispose();

            reader = File.OpenText("FemaleNames.txt");

            name = reader.ReadLine();

            while (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                CharacterProfile profile = ScriptableObject.CreateInstance<CharacterProfile>();
                profile.Initialize(
                    Gender.Female,
                    this.GenerateEthnicity(ethnicities),
                    this.GenerateHeadGear(headGears),
                    name,
                    new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value),
                    UnityEngine.Random.Range(1.0f, 3.0f));

                AssetDatabase.CreateAsset(profile, string.Format("Assets/Resources/Characters/{0}.asset", name));

                name = reader.ReadLine();
            }

            reader.Dispose();
        }

        /// <summary>
        /// Generates the head gear.
        /// </summary>
        /// <param name="headGears">The head gears.</param>
        /// <returns>random headgear</returns>
        private HeadGear GenerateHeadGear(HeadGear[] headGears)
        {
            int items = 0;
            HeadGear headGear = HeadGear.None;
            while (UnityEngine.Random.value < 0.25f && items < 3)
            {
                if (headGear == HeadGear.None)
                {
                    headGear = headGears[UnityEngine.Random.Range(1, headGears.Length - 1)];
                }
                else
                {
                    headGear |= headGears[UnityEngine.Random.Range(1, headGears.Length - 1)];
                }

                items++;
            }

            return headGear;
        }

        /// <summary>
        /// Generates the ethnicity.
        /// </summary>
        /// <param name="ethnicities">The available ethnicities.</param>
        /// <returns>
        /// random ethnicity
        /// </returns>
        private Ethnicity GenerateEthnicity(Ethnicity[] ethnicities)
        {
            int items = 0;
            Ethnicity ethnicity = ethnicities[UnityEngine.Random.Range(0, ethnicities.Length - 1)];

            while (UnityEngine.Random.value < 0.05 && items < 2)
            {
                ethnicity |= ethnicities[UnityEngine.Random.Range(0, ethnicities.Length - 1)];
                items++;
            }

            return ethnicity;
        }

        #endregion Methods
    }
}