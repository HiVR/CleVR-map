// <copyright file="BuildingView.cs" company="CleVR B.V.">
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
    /// Building editor view (example)
    /// </summary>
    /// <seealso cref="UnityEditor.Editor" />
    [CustomEditor(typeof(BuildingViewModel), editorForChildClasses: true)]
    public class BuildingView : Editor
    {
        #region Methods

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        #endregion Methods
    }
}