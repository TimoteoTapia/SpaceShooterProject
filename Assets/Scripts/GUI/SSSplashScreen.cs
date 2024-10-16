using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for Coroutines

public class SSSplashScreen : MonoBehaviour
{
    [SerializeField] BlinkingText startMessage;
    [SerializeField] AudioClip confirmSound; // The sound you want to play
    [SerializeField] [Range(0f, 1f)] float volume = 0.2f; // Volume control
    private AudioSource audioSource;

    private bool gameStarted = false; // To prevent multiple inputs

    void Start()
    {
        // Set the start message
        startMessage.SetMessage(true, "Press any key to <color=#ff0000<ALPHA>>start</color>");

        // Add an AudioSource component if the object doesn't already have one
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Ensure it doesn't play automatically
    }

    void Update()
    {
        StartGame();
    }

    void StartGame()
    {
        // Check if any key or joystick button is pressed and if the game hasn't started yet
        if (!gameStarted && (Input.anyKeyDown || AnyButtonDown()))
        {
            gameStarted = true; // Prevent further inputs

            // Adjust the blinking speed to be faster when key is pressed
            startMessage.SetBlinkSpeed(20f); // Set it to blink faster (increase this value as needed)

            // Play the sound with extended duration
            audioSource.PlayOneShot(confirmSound, volume);

            // Start the coroutine for a delay and then load the scene
            StartCoroutine(WaitAndLoadScene(1f)); // Wait 2 seconds before loading the scene
        }
    }

    bool AnyButtonDown()
    {
        // Check if any of the specific joystick buttons are pressed
        bool anyButtonDown = false;
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            anyButtonDown = true;
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            anyButtonDown = true;
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            anyButtonDown = true;
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
            anyButtonDown = true;
        return anyButtonDown;
    }

    // Coroutine to wait for the specified delay (in seconds) before loading the next scene
    IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        SceneManager.LoadScene("Game Play"); // Load the main menu scene
    }
}
