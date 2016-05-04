// <copyright file="Seat.cs" company="CleVR B.V.">
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
    /// Nice comfy seat with absolutely no whoopee cushion on it
    /// </summary>
    /// <seealso cref="Assets.Model.BaseModel" />
    public class Seat : BaseModel
    {
        #region Fields

        /// <summary>
        /// The assigned to
        /// </summary>
        private Character assignedTo = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Seat"/> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="id">The unique identifier.</param>
        public Seat(Transform transform, int id)
            : base(transform, id, "Seat")
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the character assigned to the seat.
        /// </summary>
        /// <value>
        /// The assigned to.
        /// </value>
        public Character AssignedTo
        {
            get
            {
                return this.assignedTo;
            }

            private set
            {
                this.assignedTo = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Seat"/> is reserved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reserved; otherwise, <c>false</c>.
        /// </value>
        public bool Reserved
        {
            get
            {
                return this.AssignedTo != null;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Assigns the seat.
        /// </summary>
        /// <param name="character">The character.</param>
        public void AssignSeat(Character character)
        {
            this.AssignedTo = character;
        }

        /// <summary>
        /// Stands up.
        /// </summary>
        public void StandUp()
        {
            if (this.AssignedTo != null)
            {
                this.AssignedTo.StandUp();
            }

            this.AssignedTo = null;
        }

        #endregion Methods
    }
}