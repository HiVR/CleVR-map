// <copyright file="CarView.cs" company="CleVR B.V.">
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
    /// Car editor view
    /// </summary>
    [CustomEditor(typeof(CarViewModel), editorForChildClasses: true)]
    public sealed class CarView : Editor
    {
        #region Methods

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CarViewModel car = this.target as CarViewModel;

            if (car != null && car.Observed != null)
            {
                GUILayout.Label(string.Format("Type: {0}", car.Observed.Type.ToString()));

                if (car.Observed.HasSiren)
                {
                    GUILayout.Label(string.Format("Siren: {0}", car.Observed.SirenActive ? "On" : " Off"));
                }
            }
        }

        #endregion Methods
    }
}