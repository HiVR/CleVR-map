// <copyright file="BaseViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Abstract base class with basic ViewModel functionality such as the link to the Model layer
    /// </summary>
    /// <typeparam name="T">Model layer type</typeparam>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public abstract class BaseViewModel<T> : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Gets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public T Observed { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Called when GameObject is enabled.
        /// </summary>
        protected void OnEnable()
        {
            this.Observed = this.CreateModel(this.transform);

            this.AddModelToTracking();
        }

        /// <summary>
        /// Called when GameObject is disabled.
        /// </summary>
        protected void OnDisable()
        {
            this.RemoveModelFromTracking();
        }

        /// <summary>
        /// Creates the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>Created model</returns>
        protected abstract T CreateModel(Transform transform);


        /// <summary>
        /// Adds the model to a tracking list.
        /// </summary>
        protected abstract void AddModelToTracking();

        /// <summary>
        /// Removes the model from a tracking list.
        /// </summary>
        protected abstract void RemoveModelFromTracking();


        #endregion Methods
    }
}