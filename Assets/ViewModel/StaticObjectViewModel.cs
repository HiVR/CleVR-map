// <copyright file="StaticObjectViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.ViewModel
{
    using Assets.Logic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Static object view model for object that will never move during runtime.
    /// </summary>
    /// <typeparam name="T">Type of the model layer</typeparam>
    /// <seealso cref="Assets.ViewModel.BaseViewModel{T}" />
    public abstract class StaticObjectViewModel<T> : BaseViewModel<T>
    {
        #region Methods

        /// <summary>
        /// Adds the model to a tracking list.
        /// </summary>
        protected override void AddModelToTracking()
        {
            ObjectTracker.AddStaticObject(this);
        }

        /// <summary>
        /// Removes the model from a tracking list.
        /// </summary>
        protected override void RemoveModelFromTracking()
        {
            ObjectTracker.RemoveStaticObject(this);
        }

        #endregion Methods
    }
}