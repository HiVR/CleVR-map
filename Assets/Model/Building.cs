// <copyright file="Building.cs" company="CleVR B.V.">
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
    /// Building model
    /// </summary>
    /// <seealso cref="Assets.Model.BaseModel" />
    public class Building : BaseModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        public Building(Transform transform)
            : base(transform, -1, "Building")
        {
        }

        #endregion Constructors
    }
}