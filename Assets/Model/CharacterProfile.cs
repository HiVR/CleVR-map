// <copyright file="CharacterProfile.cs" company="CleVR B.V.">
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
    /// Character profile contains profile of a specific character
    /// </summary>
    public sealed class CharacterProfile : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// The gender
        /// </summary>
        [SerializeField]
        private Gender gender = Gender.Male;

        /// <summary>
        /// The ethnicity
        /// </summary>
        [SerializeField]
        private Ethnicity ethnicity = Ethnicity.AmericanIndian;

        /// <summary>
        /// The head gear
        /// </summary>
        [SerializeField]
        private HeadGear headGear = HeadGear.None;

        /// <summary>
        /// Are you a Tim?
        /// </summary>
        [SerializeField]
        private string characterName = "Tim";

        /// <summary>
        /// The character render color
        /// </summary>
        [SerializeField]
        private Color color = Color.black;

        /// <summary>
        /// The walking speed in m/s
        /// </summary>
        [SerializeField]
        private float walkingSpeed = 2.0f;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string CharacterName
        {
            get
            {
                return this.characterName;
            }

            private set
            {
                this.characterName = value;
            }
        }

        /// <summary>
        /// Gets the character render color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get
            {
                return this.color;
            }

            private set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets the character head gear.
        /// </summary>
        /// <value>
        /// The head gear.
        /// </value>
        public HeadGear HeadGear
        {
            get
            {
                return this.headGear;
            }

            private set
            {
                this.headGear = value;
            }
        }

        /// <summary>
        /// Gets the character ethnicity.
        /// </summary>
        /// <value>
        /// The ethnicity.
        /// </value>
        public Ethnicity Ethnicity
        {
            get
            {
                return this.ethnicity;
            }

            private set
            {
                this.ethnicity = value;
            }
        }

        /// <summary>
        /// Gets the walking speed in m/s.
        /// </summary>
        /// <value>
        /// The walking speed.
        /// </value>
        public float WalkingSpeed
        {
            get
            {
                return this.walkingSpeed;
            }

            private set
            {
                this.walkingSpeed = value;
            }
        }

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the character gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            private set
            {
                this.gender = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes the specified character profile.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <param name="ethnicity">The ethnicity.</param>
        /// <param name="headGear">The head gear.</param>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        /// <param name="walkingSpeed">The walking speed.</param>
        public void Initialize(Gender gender, Ethnicity ethnicity, HeadGear headGear, string name, Color color, float walkingSpeed)
        {
            this.Gender = gender;
            this.Ethnicity = ethnicity;
            this.HeadGear = headGear;
            this.CharacterName = name;
            this.Color = color;
            this.WalkingSpeed = walkingSpeed;
        }

        #endregion Methods
    }
}