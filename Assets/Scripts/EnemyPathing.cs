using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
    List<Transform> waypoints;
    int currentWaypointIndex = 0;

	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWaypoints();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (currentWaypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[currentWaypointIndex].transform.position;
            var deltaMoveSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, deltaMoveSpeed);

            if (transform.position == targetPos)
                currentWaypointIndex++;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
