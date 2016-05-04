// <copyright file="TVViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.ViewModel
{
    using Assets.Model;
    using Assets.Model.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// TV View Model
    /// </summary>
    /// <seealso cref="Assets.ViewModel.StaticObjectViewModel{Assets.Model.TV}" />
    public class TVViewModel : StaticObjectViewModel<TV>
    {
        #region Methods

        /// <summary>
        /// Creates the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>
        /// Created model
        /// </returns>
        protected override TV CreateModel(Transform transform)
        {
            return TVFactory.CreateTV(transform);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            this.Observed.Update();
        }

        #endregion Methods
    }
}