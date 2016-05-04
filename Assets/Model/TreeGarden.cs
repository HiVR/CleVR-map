// <copyright file="TreeGarden.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using Assets.Model.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Three Garden representing a nice big tree and some seats around it
    /// </summary>
    /// <seealso cref="Assets.Model.BaseModel" />
    public class TreeGarden : BaseModel
    {
        #region Fields

        /// <summary>
        /// The seats
        /// </summary>
        private List<Seat> seats = new List<Seat>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeGarden" /> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="id">The unique identifier.</param>
        public TreeGarden(Transform transform, int id)
            : base(transform, id, "TreeGarden")
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the seats.
        /// </summary>
        /// <value>
        /// The seats.
        /// </value>
        public IEnumerable<Seat> Seats
        {
            get
            {
                return this.seats;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds the seat.
        /// </summary>
        /// <param name="transform">The transform.</param>
        public void AddSeat(Transform transform)
        {
            this.seats.Add(SeatFactory.CreateSeat(transform));
        }

        #endregion Methods
    }
}