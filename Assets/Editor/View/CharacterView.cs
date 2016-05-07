// <copyright file="CharacterView.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.View
{
    using Assets.Model;
    using Assets.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Character view
    /// </summary>
    [CustomEditor(typeof(CharacterViewModel), editorForChildClasses: true)]
    public sealed class CharacterView : Editor
    {
        #region Methods

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CharacterViewModel character = this.target as CharacterViewModel;

            if (character != null && character.Observed != null)
            {
                CharacterProfile profile = character.Observed.Profile;

                GUILayout.Label(string.Format("Name: {0}", profile.CharacterName));
                GUILayout.Label(string.Format("Gender: {0}", profile.Gender));
                GUILayout.Label(string.Format("Ethnicity: {0}", profile.Ethnicity));
                GUILayout.Label(string.Format("Head Gear: {0}", profile.HeadGear));
                GUILayout.Label(string.Format("Walking Speed: {0}", profile.WalkingSpeed));
                GUILayout.Label(string.Format("Reached Goal: {0}", character.Observed.HasReachedGoal));
                GUILayout.Label(string.Format("Emotion: {0}", character.Observed.Emotion.ToString()));
                GUILayout.Label(string.Format("Action: {0}", character.Observed.Action.ToString()));
                GUILayout.Label(string.Format("Is Sitting: {0}", character.Observed.IsSitting));
            }
        }

        #endregion Methods
    }
}