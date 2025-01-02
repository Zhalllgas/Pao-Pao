using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyGame.Utils;

public class GameManager : MonoBehaviour
{    

    #region BASIC FIELDS

        [Space(20)] [Header("NOTES")] [TextArea(5, 10)]
        public string notes;

        [Space(20)] [Header("CUSTOM TITLE")]
        public bool isGamePlayed;

        [Space(20)] [Header("___")]
        public string emptySpace;

    #endregion

    #region LIFE CYCLE METHODS
    
        /// <summary>
        /// Called When The Script Instance Is Being Loaded.
        /// </summary>
        /// <seealso cref="Start"/>
        void Awake()
        {
            isGamePlayed = PlayerPrefs.GetInt("IsGamePlayed", 0) == 1; 

            if (isGamePlayed)
            {
                SystemComment("Not First Time Entering The Game!");
            }
            else
            {
                SystemComment("First Time Entering The Game!");
            }

            PlayerPrefs.SetInt("IsGamePlayed", 1);
        }

        /// <summary>
        /// Called Before The First Frame Update.
        /// </summary>
        void Start()
        {
            // Use This For Any Initialization That Needs To Happen Once The Game Starts
            // E.g., Initializing Game State, Loading Resources, Or Starting Coroutines
        }

        /// <summary>
        /// Called Once Per Frame.
        /// </summary>
        /// <seealso cref="FixedUpdate"/>
        void Update()
        {
            // Use This For Any Logic That Needs To Happen Every Frame
        }

        /// <summary>
        /// Called At Fixed Intervals, Useful For Physics Updates.
        /// </summary>
        void FixedUpdate()
        {
            // Use This For Physics-Related Updates Or Calculations
        }
        
        #endregion 

        #region CUSTOM METHODS
        
        /// <summary>
        /// Restarts the game by reloading the current scene.
        /// </summary>
        [ContextMenu("RestartTheGame")]
        public void RestartTheGame()
        {
            PlayerPrefs.DeleteAll();
        }

    #endregion

}