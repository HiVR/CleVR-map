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
        }

        /// <summary>
        /// Creates the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>Created model</returns>
        protected abstract T CreateModel(Transform transform);

        #endregion Methods
    }
}