using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelState
{
    Intro,
    Playing,
    Paused,
    Defeat, 
    Victory
}

public class LevelManager : MonoBehaviour
{

    #region BASIC FIELDS

        [Header("NOTES")] [TextArea(5, 10)]
        public string notes;

        [Space(20)] [Header("BASIC VARIABLES")]
            
            [Space(10)] [Header("Basic Variables")]
            [Tooltip("The time before the timer starts")] public int introLength;
            [Tooltip("The additional time")] public int bufferTime;
            [Tooltip("The duration of frozen time after unpausing")] public int pausedLength;
            [Tooltip("The maximum allowed amount of time")] public int maxTime;
            [Tooltip("The current time")] public int currentTime;
            public bool isPaused;
            public Slider timeSlider;
            public LevelState currentLevelState;
            public static LevelManager instance;
            public GameObject pausedScreen;
            public GameObject victoryScreen;
            public GameObject defeatScreen;

    #endregion

    #region LIFE CYCLE METHODS

        /// <summary>
        /// Called when the script instance is being loaded.
        /// Useful for initialization before the game starts.
        /// </summary>
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            LeanTween.init(800); 
            LeanTween.cancelAll();
        }

        /// <summary>
        /// Called before the first frame update.
        /// Useful for initialization once the game starts.
        /// </summary>
        void Start()
        {
            currentLevelState = LevelState.Playing;

            pausedScreen.SetActive(false);
            victoryScreen.SetActive(false);
            defeatScreen.SetActive(false);
            
            ResetTimer();
            Invoke("StartTimer", introLength);
        }

        /// <summary>
        /// Called once per frame.
        /// Use for logic that needs to run every frame, such as user input or animations.
        /// </summary>
        void Update()
        {
            if (currentTime <= 0) Defeat();
        }

        /// <summary>
        /// Called at fixed intervals, ideal for physics updates.
        /// Use this for physics-related updates like applying forces or handling Rigidbody physics.
        /// </summary>
        void FixedUpdate()
        {
            // Add physics-related logic here.
            // Example: Rigidbody movement, applying forces, or collision detection.
        }

    #endregion

    #region CUSTOM METHODS

        /// <summary>
        /// A method used to restart the timer.
        /// </summary>
        public void ResetTimer()
        {
            maxTime = SpawningManager.instance.x * SpawningManager.instance.y + bufferTime;
            currentTime = maxTime;
            timeSlider.value = 1f;
            isPaused = false;   
        }
    
        /// <summary>
        /// A method used to call the timer.
        /// </summary>
        void StartTimer()
        {
            StartCoroutine(Timer());   
        }

        /// <summary>
        /// Timer system.
        /// </summary>
        IEnumerator Timer()
        {
            while ((currentTime > 0) && (currentLevelState == LevelState.Playing))
            {
                currentTime--;

                timeSlider.value = (float)(currentTime) / (float)(maxTime);

                yield return new WaitForSeconds(1f);    
            }
        }        
        
        /// <summary>
        /// Method used to show victory screen.
        /// Triggered when the user matches all cards.
        /// </summary>
        public void Victory()
        {
            currentLevelState = LevelState.Victory;
            victoryScreen.SetActive(true);
            //StopAllCoroutines();

            CanvasGroup canvasGroup = victoryScreen.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f; 
            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeOutQuad);
            
            victoryScreen.transform.position = new Vector3 (0, 10, 0);
            LeanTween.move(victoryScreen, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeOutQuad);
        }

        /// <summary>
        /// Method used to show defeat screen.
        /// Triggered when the user runs out of time.
        /// </summary>
        public void Defeat()
        {
            currentLevelState = LevelState.Defeat;
            defeatScreen.SetActive(true);
            //StopAllCoroutines();

            CanvasGroup canvasGroup = defeatScreen.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f; 
            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeOutQuad);
            
            defeatScreen.transform.position = new Vector3 (0, 10, 0);
            LeanTween.move(defeatScreen, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeOutQuad);
        }

        /// <summary>
        /// Pausing the game.
        /// </summary>
        public void Pause()
        {
            currentLevelState = LevelState.Paused;
            pausedScreen.SetActive(true);
            
            CanvasGroup canvasGroup = pausedScreen.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f; 
            LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setEase(LeanTweenType.easeOutQuad);
            
            pausedScreen.transform.position = new Vector3 (0, 10, 0);
            LeanTween.move(pausedScreen, new Vector3(0, 0, 0), 1f).setEase(LeanTweenType.easeOutQuad);

            isPaused = true;
        }
    
        /// <summary>
        /// Unpausing the game.
        /// </summary>
        public void Unpause()
        {
            currentLevelState = LevelState.Playing;
        
            CanvasGroup canvasGroup = pausedScreen.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f; 
            LeanTween.alphaCanvas(canvasGroup, 0f, 1f).setEase(LeanTweenType.easeOutQuad);
            
            LeanTween.move(pausedScreen, new Vector3(0, 10, 0), 1f).setEase(LeanTweenType.easeOutQuad);

            Invoke("StartTimer", pausedLength);
            isPaused = false;
            StartCoroutine(Timer());   
        }

    #endregion

}