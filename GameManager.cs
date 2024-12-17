using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class  GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Camera_Controller mainCamera;

    public Player_Controller player;

    public SpawnPoint[] spawnPoints;

    public int lives = 3;
        
    public int currentWave;

    public int enemiesRemaining = 0;

    public GameObject prefabPlayer_Controller;
    public GameObject prefabAI_Controller;
    public GameObject prefabPlayerPawn;
    public GameObject prefabAIPawn;

    public List<WaveData> waves;

    public List<AI_Controller> enemies;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FindCamera()
    {
        // Find and store the camera controller
        mainCamera = FindObjectOfType<Camera_Controller>();
    }

    private void LoadSpawnPoints()
    {
        // Load all spawn points
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public Transform GetRandomSpawnPoint()
    {
        // If we have spawn points
        if (spawnPoints.Length > 0)
        {
            // Return a random player spawn point
            return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        }
        // Otherwise, return null
        return null;
    }

   public void SpawnPlayer()
    {
        // Spawn the Player Controller at 0,0,0, and save it in our player variable
        GameObject playerObject = Instantiate(prefabPlayer_Controller, Vector3.zero, Quaternion.identity);
        player = playerObject.GetComponent<Player_Controller>();


        // Ensure pawn is assigned after calling PossessPawn
        Pawn newPawn = SpawnPawn();
        if (newPawn != null)
        {
        player.PossessPawn(newPawn);

        // Connect the camera controller to the pawn
        mainCamera.target = player.pawn.transform;

        // Subscribe to the player death event once, when player is spawned
        Health playerHealth = player.pawn.GetComponent<Health>();
            if (playerHealth != null)
            {
            playerHealth.OnDeath.AddListener(OnPlayerDeath);
            }
        }
        else
        {
            Debug.LogError("Failed to spawn player pawn.");
        }
    }


    public Pawn SpawnPawn()
    {
        return SpawnPawn(prefabPlayerPawn.GetComponent<Pawn>());
    }


    public Pawn SpawnPawn(Pawn pawnToSpawn)
    {
        // Spawn the Player at a random spawn point
        Transform randomSpawnPoint = GetRandomSpawnPoint();
        if (randomSpawnPoint != null)
        {
            GameObject tempPawnObject = Instantiate(prefabPlayerPawn, randomSpawnPoint.position, randomSpawnPoint.rotation);
            Pawn tempPawn = tempPawnObject.GetComponent<Pawn>();

            return tempPawn;
        }
        else
        {
            Debug.LogWarning("No spawn points available for player.");
            return null;
        }
    }
    
    public void SpawnEnemy()
    {
        // Instantiate the pawn prefab and retrieve its Pawn component
        Pawn newPawn = Instantiate(prefabAIPawn).GetComponent<Pawn>();
        SpawnEnemy(newPawn);

        
    }


    public void SpawnEnemy(Pawn pawnToSpawn)
    {
        // Ensure prefabAI_Controller is a GameObject with an AI_Controller component
        GameObject aiObject = Instantiate(prefabAI_Controller, Vector3.zero, Quaternion.identity);
        AI_Controller newAI = aiObject.GetComponent<AI_Controller>();

        // Add it to the list of enemies
        enemies.Add(newAI);

        // Make sure SpawnPawn() returns the correct type
        Pawn spawnedPawn = SpawnPawn(); // Assume Pawn is the correct type for PossessPawn
        newAI.PossessPawn(spawnedPawn);
        
        // Subscribe to the new enemy's OnDeath event
        Health newAIHealth = newAI.pawn.GetComponent<Health>();
        if (newAIHealth != null)
        {
            newAIHealth.OnDeath.AddListener(OnEnemyDeath);
        }
    }

    [System.Serializable]
    public class WaveData
    {
        public List<Pawn> pawns;
    }

    public void SpawnWave(int waveNumber)
    {
        // Spawn the wave for that wave number using our overloaded function!
        SpawnWave(waves[waveNumber]);
    }

    public void SpawnWave(WaveData wave)
    {
        if (wave.pawns == null || wave.pawns.Count == 0)
        {
            Debug.LogWarning("No pawns to spawn in the wave.");
            return;
        }

        foreach (Pawn enemyToSpawn in wave.pawns)
        {
            SpawnEnemy(enemyToSpawn);
        }

        enemiesRemaining = wave.pawns.Count;
    }

    public void OnEnemyDeath()
    {
        enemiesRemaining = Mathf.Max(0, enemiesRemaining - 1); // Ensure enemiesRemaining doesn't go below 0

        if (enemiesRemaining <= 0)
        {
            currentWave++;

            if (currentWave < waves.Count)
            {
                SpawnWave(waves[currentWave]);
            }
            else
            {
                DoVictory();
            }
        }
    }

    public void RespawnPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is missing. Cannot respawn.");
            return;
        }

        if (player.pawn == null)
        {
            Debug.LogWarning("Player Pawn is missing. Cannot respawn.");
            return;
        }

        if (player.lives > 0)
        {
            Destroy(player.pawn.gameObject);
            player.UnpossessPawn();
            player.PossessPawn(SpawnPawn());
            mainCamera.target = player.pawn.transform;
            player.lives--;
        }
        else
        {
            DoGameOver();
        }
    }

    public void StartGame()
    {
        FindCamera();
        LoadSpawnPoints();
        SpawnPlayer();

        if (waves != null && waves.Count > 0)
        {
            currentWave = 0;
            SpawnWave(waves[currentWave]);
        }
        else
        {
            Debug.LogError("No waves to spawn.");
        }
    }


    public void DoVictory()
    {
        Debug.Log("---------------VICTORY---------------");
        // Add victory logic (UI, restart, etc.)
    }

    public void DoGameOver()
    {
        Debug.Log("Game Over");
        // Add game over logic (UI, restart, etc.)
    }

    public void OnPlayerDeath()
    {
        // Respawn the player
        RespawnPlayer();

        // TODO: Add any additional logic for player death (e.g., display death animation, etc.)
    }

    void Start()
    {
        StartGame(); // Automatically start the game when the script runs
    }

}
