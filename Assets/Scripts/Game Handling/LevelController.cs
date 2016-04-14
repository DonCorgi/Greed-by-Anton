﻿using UnityEngine;

public enum LevelState {
	Playing,
	Completed,
    Secret,
	Failed
}

public class LevelController : MonoBehaviour {
	//TODO should really this be done like this?
	//Maybe gamemanager can pass the prefabs to every levelmanager instead.
	[Header("Setup")]
	public GameObject player1Prefab;
	public GameObject player2Prefab;
    
	[Header("Level setup")]
	public GoalController Goal1;
	public GoalController Goal2;
    public float SpawnDelay = 0.0f;
    public bool isSecretLevel = false;
	public Vector3[] spawnPoints;
    [Header("Optional level setup")]
    public SecretTrigger secretTrigger;

	[HideInInspector]
	public LevelState state = LevelState.Playing;

	GameObject player1Object;
	GameObject player2Object;
	
	void Start () {
        Invoke("SpawnPlayers", SpawnDelay);
	}

	void Update () {
        if (player1Object == null || player2Object == null)
            return;
        
		if (!player1Object.activeSelf || !player2Object.activeSelf) {            
			state = LevelState.Failed;
		}

		if (Goal1.playerIsInGoal && Goal2.playerIsInGoal) {
			state = LevelState.Completed;
		}
        
        if (secretTrigger != null && secretTrigger.collected) {
            state = LevelState.Secret;
        }
	}

    void SpawnPlayers() {
        player1Object = Instantiate(player1Prefab, spawnPoints[0], Quaternion.identity) as GameObject;
		player2Object = Instantiate(player2Prefab, spawnPoints[1], Quaternion.identity) as GameObject;
    }

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		float size = 0.5f;

		for (int i = 0; i < spawnPoints.Length; i++) {
			Gizmos.DrawLine(spawnPoints[i] - Vector3.up * size, spawnPoints[i] + Vector3.up * size);
			Gizmos.DrawLine(spawnPoints[i] - Vector3.left * size, spawnPoints[i] + Vector3.left * size);
		}
	}
}
