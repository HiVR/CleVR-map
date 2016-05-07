// <copyright file="CharacterAction.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>
namespace Assets.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Character actions
    /// </summary>
    public enum CharacterAction
    {
        /// <summary>
        /// No action
        /// </summary>
        Nothing = 1,

        /// <summary>
        /// The calling phone
        /// </summary>
        CallingPhone = 2,

        /// <summary>
        /// The taking picture
        /// </summary>
        TakingPicture = 3,

        /// <summary>
        /// The looking at time
        /// </summary>
        LookingAtTime = 4,

        /// <summary>
        /// The follow user
        /// </summary>
        FollowUser = 5,

        /// <summary>
        /// The looking at user
        /// </summary>
        LookingAtUser = 6,
    }
}