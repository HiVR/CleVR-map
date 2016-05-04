// <copyright file="Controller.cs" company="CleVR B.V.">
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
    /// The main world controller
    /// </summary>
    public class Controller
    {
        #region Fields

        /// <summary>
        /// The next seat update
        /// </summary>
        private float nextSeatUpdate = 0.0f;

        /// <summary>
        /// The assigned seats
        /// </summary>
        private List<Seat> assignedSeats = new List<Seat>();

        /// <summary>
        /// The next TV update
        /// </summary>
        private float nextTVUpdate = 0.0f;

        /// <summary>
        /// The next siren update
        /// </summary>
        private float nextSirenUpdate = 0.0f;

        /// <summary>
        /// The next character action update
        /// </summary>
        private float nextCharacterActionUpdate = 0.0f;

        /// <summary>
        /// The next character emotion update
        /// </summary>
        private float nextCharacterEmotionUpdate = 0.0f;

        /// <summary>
        /// The TV texts
        /// </summary>
        private List<string> tvTexts = new List<string>();

        /// <summary>
        /// The TV movies
        /// </summary>
        private List<string> tvMovies = new List<string>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            this.tvMovies.Add("Scary Movie");
            this.tvMovies.Add("Journaal");
            this.tvMovies.Add("Reclame");
            this.tvMovies.Add("Funniest Home Video");

            this.tvTexts.Add("Dreigings niveua verhoogt");
            this.tvTexts.Add("Kijk jij ooit achterom");
            this.tvTexts.Add("Meer inbraken gebleegt");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (CharacterFactory.NumberOfActiveCharacters > 10)
            {
                if (this.nextSeatUpdate < Time.time)
                {
                    this.nextSeatUpdate = Time.time + UnityEngine.Random.Range(1f, 5f);

                    if (UnityEngine.Random.value < 0.25f)
                    {
                        Character character = CharacterFactory.GetRandomActiveCharacter();
                        if (!character.GoingToSeat)
                        {
                            Seat seat = SeatFactory.GetRandomEmptySeat();
                            if (seat != null)
                            {
                                seat.AssignSeat(character);
                                character.SetGoal(seat.Transform.position, isSeat: true);
                                this.assignedSeats.Add(seat);
                            }
                        }
                    }
                    else if (UnityEngine.Random.value < 0.25f)
                    {
                        if (this.assignedSeats.Count > 0)
                        {
                            Seat seat = this.assignedSeats[UnityEngine.Random.Range(0, this.assignedSeats.Count - 1)];

                            if (seat.AssignedTo.IsSitting)
                            {
                                seat.StandUp();
                            }

                            this.assignedSeats.Remove(seat);
                        }
                    }
                }

                if (this.nextCharacterActionUpdate < Time.time)
                {
                    this.nextCharacterActionUpdate = Time.time + UnityEngine.Random.Range(0.1f, 2.0f);

                    Character character = CharacterFactory.GetRandomActiveCharacter();
                    character.SetAction((CharacterAction)UnityEngine.Random.Range(1, 6));
                }

                if (this.nextCharacterEmotionUpdate < Time.time)
                {
                    this.nextCharacterEmotionUpdate = Time.time + UnityEngine.Random.Range(0.1f, 2.0f);
                    Character character = CharacterFactory.GetRandomActiveCharacter();
                    character.SetEmotion((CharacterEmotion)UnityEngine.Random.Range(1, 7));
                }
            }

            if (this.nextSirenUpdate < Time.time)
            {
                this.nextSirenUpdate = Time.time + UnityEngine.Random.Range(5.0f, 15.0f);

                if (UnityEngine.Random.value < 0.35f)
                {
                    List<Car> cars = CarFactory.AllCars.Where(x => x.HasSiren && !x.SirenActive).ToList();

                    if (cars.Count > 0)
                    {
                        Car car = cars[UnityEngine.Random.Range(0, cars.Count - 1)];
                        car.ActivateSiren(UnityEngine.Random.Range(4.0f, 10f));
                    }
                }
            }

            if (this.nextTVUpdate < Time.time)
            {
                this.nextTVUpdate = Time.time + UnityEngine.Random.Range(3.0f, 10.0f);

                if (UnityEngine.Random.value < 0.5f)
                {
                    TV tv = TVFactory.GetRandomTV();

                    tv.TurnOn();

                    if (tv.ShowingText)
                    {
                        if (UnityEngine.Random.value < 0.75f)
                        {
                            tv.PlayMovie(this.GetRandomMovie());
                        }
                        else
                        {
                            tv.ShowText(this.GetRandomText(), UnityEngine.Random.Range(5.0f, 15.0f));
                        }
                    }
                    else
                    {
                        if (UnityEngine.Random.value < 0.6f)
                        {
                            tv.ShowText(this.GetRandomText(), UnityEngine.Random.Range(5.0f, 15.0f));
                        }
                        else
                        {
                            tv.PlayMovie(this.GetRandomMovie());
                        }
                    }
                }

                if (UnityEngine.Random.value < 0.05)
                {
                    TV tv = TVFactory.GetRandomTV();

                    tv.TurnOff();
                }
            }
        }

        /// <summary>
        /// Gets a random TV text.
        /// </summary>
        /// <returns>Random TV text</returns>
        private string GetRandomText()
        {
            return this.tvTexts[UnityEngine.Random.Range(0, this.tvTexts.Count - 1)];
        }

        /// <summary>
        /// Gets a random movie.
        /// </summary>
        /// <returns>Random movie</returns>
        private string GetRandomMovie()
        {
            return this.tvMovies[UnityEngine.Random.Range(0, this.tvMovies.Count - 1)];
        }

        #endregion Methods
    }
}