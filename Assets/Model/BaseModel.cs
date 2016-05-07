// <copyright file="BaseModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Abstract base model providing base functionality
    /// </summary>
    public abstract class BaseModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModel"/> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public BaseModel(Transform transform, int id, string name)
        {
            this.Transform = transform;
            this.Id = id;
            this.Name = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the transform.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        public Transform Transform { get; private set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        #endregion Properties
    }
}