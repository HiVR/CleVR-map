// <copyright file="CharacterViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.ViewModel
{
    using Assets.Model;
    using Assets.Model.Factories;
    using Assets.Logic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Character view Model
    /// </summary>
    public sealed class CharacterViewModel : BaseViewModel<Character>
    {
        #region Methods

        /// <summary>
        /// Creates the model layer.
        /// </summary>
        /// <param name="transform">The transform of the GameObject.</param>
        /// <returns>
        /// Created model
        /// </returns>
        protected override Character CreateModel(Transform transform)
        {
            return CharacterFactory.CreateCharacter(transform);
        }

        /// <summary>
        /// Adds the model to a tracking list.
        /// </summary>
        protected override void AddModelToTracking()
        {
            ObjectTracker.AddDynamicObject(this);
        }

        /// <summary>
        /// Removes the model from a tracking list.
        /// </summary>
        protected override void RemoveModelFromTracking()
        {
            ObjectTracker.RemoveDynamicObject(this);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            Transform model = this.transform.FindChild("Model");

            if (model != null)
            {
                Renderer renderer = model.gameObject.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.material.color = this.Observed.Profile.Color;
                }
            }

            this.Observed.Transform.name = this.Observed.Profile.CharacterName;

            NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

            if (agent != null)
            {
                this.Observed.SetNavMeshAgent(agent);
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        private void Update()
        {
            if (!this.Observed.IsActive)
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        #endregion Methods
    }
}