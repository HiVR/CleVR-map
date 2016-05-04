// <copyright file="TV.cs" company="CleVR B.V.">
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
    /// There is nothing interesting on TV ever
    /// </summary>
    /// <seealso cref="Assets.Model.BaseModel" />
    public class TV : BaseModel
    {
        #region Fields

        /// <summary>
        /// The show text until moment in time
        /// </summary>
        private float showTextUntil;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TV" /> class.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="id">The unique identifier.</param>
        public TV(Transform transform, int id)
            : base(transform, id, "TV")
        {
            this.State = TVState.Off;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public TVState State { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [show text].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show text]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowingText { get; private set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the movie.
        /// </summary>
        /// <value>
        /// The movie.
        /// </value>
        public string Movie { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Turns the TV off.
        /// </summary>
        public void TurnOff()
        {
            this.State = TVState.Off;
        }

        /// <summary>
        /// Turns the TV on.
        /// </summary>
        public void TurnOn()
        {
            this.State = TVState.On;
        }

        /// <summary>
        /// Plays the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        public void PlayMovie(string movie)
        {
            this.Movie = movie;
            this.ShowingText = false;
        }

        /// <summary>
        /// Shows the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="duration">The duration.</param>
        public void ShowText(string text, float duration)
        {
            this.showTextUntil = Time.time + duration;
            this.Text = text;
            this.ShowingText = true;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (this.ShowingText && Time.time > this.showTextUntil)
            {
                this.ShowingText = false;
            }
        }

        #endregion Methods
    }
}