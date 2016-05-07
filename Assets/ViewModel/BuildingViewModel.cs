// <copyright file="BuildingViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.ViewModel
{
    using Assets.Model;
    using Assets.ViewModel;
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Building ViewModel
    /// </summary>
    /// <seealso cref="Assets.ViewModel.StaticObjectViewModel{Assets.Model.Building}" />
    public class BuildingViewModel : StaticObjectViewModel<Building>
    {
        #region Methods

        /// <summary>
        /// Creates a new instance of the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>
        /// Created Model
        /// </returns>
        protected override Building CreateModel(Transform transform)
        {
            return new Building(transform);
        }

        #endregion Methods
    }
}