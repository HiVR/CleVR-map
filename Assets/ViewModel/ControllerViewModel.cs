// <copyright file="ControllerViewModel.cs" company="CleVR B.V.">
//     CleVR B.V. All rights reserved.
// </copyright>

namespace Assets.ViewModel
{
    using Assets.Model;
    using Assets.Model.Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// The main world controller ViewModel updates the underlying model layer
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class ControllerViewModel : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The spawn more over time
        /// </summary>
        [SerializeField]
        private bool spawnMoreOverTime = true;

        /// <summary>
        /// The maximum number of character after seconds
        /// </summary>
        [SerializeField]
        private float maximumNumberOfCharacterAfter = 60f * 150f;

        #endregion Fields

        #region Properties

        /// <summary>
        /// The character destinations
        /// </summary>
        [NonSerialized]
        private List<Vector3> characterDestinations = new List<Vector3>();

        /// <summary>
        /// The character pre fab
        /// </summary>
        [SerializeField]
        private GameObject characterPreFab = null;

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public Controller Controller { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        public void Awake()
        {
            for (int index = 0; index < this.transform.childCount; index++)
            {
                this.characterDestinations.Add(this.transform.GetChild(index).position);
            }

            this.Controller = new Controller();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            float percentage = Time.time / this.maximumNumberOfCharacterAfter;

            int targetCharacterCount = Math.Min((int)Math.Ceiling(CharacterFactory.TotalNumberOfCharacters * percentage), CharacterFactory.TotalNumberOfCharacters);

            if (CharacterFactory.NumberOfActiveCharacters < targetCharacterCount && this.spawnMoreOverTime)
            {
                GameObject go = GameObject.Instantiate(this.characterPreFab);

                go.transform.position = this.GetRandomCharacterDestination();
                CharacterViewModel vm = go.GetComponent<CharacterViewModel>();
                vm.Observed.SetGoal(go.transform.position);
            }

            List<Character> removeCharacters = new List<Character>();

            foreach (Character character in CharacterFactory.AllCharacters)
            {
                if (character.HasReachedGoal && !character.IsSitting)
                {
                    if (UnityEngine.Random.value < 0.05f)
                    {
                        removeCharacters.Add(character);
                    }
                    else
                    {
                        character.SetGoal(this.GetRandomCharacterDestination());
                    }
                }
            }

            foreach (Character character in removeCharacters)
            {
                character.Remove();
            }

            this.Controller.Update();
        }

        /// <summary>
        /// Gets the random character destination.
        /// </summary>
        /// <returns>the random destination</returns>
        private Vector3 GetRandomCharacterDestination()
        {
            return this.characterDestinations[UnityEngine.Random.Range(0, this.characterDestinations.Count)];
        }

        #endregion Methods
    }
}