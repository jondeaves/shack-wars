using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    running,
    complete
}

public class GameManager : MonoBehaviour
{
    public int WinScore = 10;
    public int PointsPerBlock = 1;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> dropoffs = new List<GameObject>();
    private GameState _state;
    public GameObject Winner { get; private set; }
    public List<AudioClip> SoundEffects = new List<AudioClip>();
    public GameObject PickupPrefab;

    private float _spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = Random.Range(3, 5);
        _state = GameState.running;
        GameObject.FindGameObjectWithTag("Delay").GetComponent<AudioSource>().PlayDelayed(1f);

        if (GameSetup.PlayerCount == 1)
        {
            GameSetup.PlayerCount = 2;
        }

        if (GameSetup.PlayerCount < 4)
        {
            for (int iDisabled = 3; iDisabled >= GameSetup.PlayerCount; iDisabled -= 1)
            {
                GameObject playerObj = players[iDisabled];
                GameObject shackObj = dropoffs[iDisabled];

                playerObj.SetActive(false);
                shackObj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == GameState.complete)
        {
            return;
        }


        AudioSource fadeInSource = GameObject.FindGameObjectWithTag("FadeIn").GetComponent<AudioSource>();
        if (fadeInSource.volume < 0.1f)
        {
            fadeInSource.volume += 0.01f * Time.deltaTime;
        }

        for (int iPlayer = 0; iPlayer < players.Count; iPlayer += 1)
        {
            GameObject player = players[iPlayer];
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController.Score >= WinScore)
            {
                Winner = player;
                _state = GameState.complete;
                SetEndData();
                SceneManager.LoadSceneAsync("EndScene", LoadSceneMode.Single);
            }

        }

        // Every 3-5 seconds spawn a new pickup, if we aren't at maximum capacity
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            SpawnPickup();
            _spawnTimer = Random.Range(3, 5);
        }
    }

    private void SetEndData()
    {

        for (int iPlayer = 0; iPlayer < GameSetup.PlayerCount; iPlayer += 1)
        {
            PlayerController playerController = players[iPlayer].GetComponent<PlayerController>();

            GameStats.WinScore = WinScore;
            GameStats.playerScores.Add(playerController.Score);

            if (playerController.Score >= WinScore)
            {
                // Set static stats
                GameStats.Winner = playerController.PlayerNumber;
            }

        }
    }

    public void PlaySfx(int idx)
    {
        GameObject.FindGameObjectWithTag("SfxAudioSource").GetComponent<AudioSource>().PlayOneShot(
            FindObjectOfType<GameManager>().SoundEffects[idx]
        );
    }

    private void SpawnPickup()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        GameObject[] currentPickups = GameObject.FindGameObjectsWithTag("Pickup");

        if (currentPickups.Length < spawnPoints.Length)
        {
            // Further filter spawn points by which are 'occupied'
            List<GameObject> emptySpawnPoints = new List<GameObject>();

            if (currentPickups.Length > 0)
            {
                foreach (GameObject pickup in currentPickups)
                {
                    foreach (GameObject spawnPoint in spawnPoints)
                    {
                        if (pickup.transform.position.x != spawnPoint.transform.position.x &&
                            pickup.transform.position.z != spawnPoint.transform.position.z)
                        {
                            emptySpawnPoints.Add(spawnPoint);
                        }
                    }
                }
            }
            else
            {
                emptySpawnPoints = spawnPoints.OfType<GameObject>().ToList();
            }

            if (emptySpawnPoints.Count > 0)
            {
                GameObject chosenSpawnPoint = emptySpawnPoints[Random.Range(0, emptySpawnPoints.Count - 1)];
                Instantiate(PickupPrefab, chosenSpawnPoint.transform.position, Quaternion.identity);

                //Debug.Log(string.Format("Spawning new pickup at {0}:{1}", chosenSpawnPoint.transform.position.x, chosenSpawnPoint.transform.position.z));
            }
        }
    }
}
