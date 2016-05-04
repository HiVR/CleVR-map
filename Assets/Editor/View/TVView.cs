// <copyright file="TVView.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.View
{
    using Assets.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// TV View with editor TV controls
    /// </summary>
    /// <seealso cref="UnityEditor.Editor" />
    [CustomEditor(typeof(TVViewModel), editorForChildClasses: true)]
    public class TVView : Editor
    {
        #region Methods

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TVViewModel tv = this.target as TVViewModel;

            if (tv != null && tv.Observed != null)
            {
                if (tv.Observed.State == Model.TVState.On)
                {
                    if (GUILayout.Button("Turn Off"))
                    {
                        tv.Observed.TurnOff();
                    }
                }
                else
                {
                    if (GUILayout.Button("Turn On"))
                    {
                        tv.Observed.TurnOn();
                    }
                }

                if (tv.Observed.ShowingText)
                {
                    GUILayout.Label(string.Format("Text: {0}", tv.Observed.Text));
                }
                else
                {
                    GUILayout.Label(string.Format("Movie: {0}", tv.Observed.Movie));
                }
            }
        }

        #endregion Methods
    }
}