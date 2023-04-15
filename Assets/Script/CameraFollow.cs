using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float damping = 1.5f;    // The damping coefficient
    public float updateInterval = 0.5f;    // How often to update the target object
    public Transform target;        // The target to follow

    private Vector3 offset;         // The initial offset between the camera and target
    private float timeSinceUpdate;  // How much time has passed since the last update
    private ButtonControll bvalue;
    void Start()
    {
        // Calculate the initial offset between the camera and target
        offset = transform.position - target.position;
        bvalue = GameObject.FindObjectOfType<ButtonControll>();
    }

    void LateUpdate()
    {
        float moveHorizontal = bvalue.horizontalInput;
        // Update the target object periodically
        timeSinceUpdate += Time.deltaTime;
        if (timeSinceUpdate >= updateInterval)
        {
            UpdateTarget();
            timeSinceUpdate = 0f;
        }

        if (target)
        {
            // Calculate the desired camera position with some damping
            Vector3 desiredPosition = target.position + offset;
            Vector3 currentPosition = Vector3.Lerp(transform.position, desiredPosition, damping * Time.deltaTime);

            // Update the camera position
            transform.position = currentPosition;
        }
    }

    void UpdateTarget()
    {
        // Find the game object with the "Player" tag that has the appropriate x-transform and y-transform based on moveHorizontal
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform targetTransform = null;

        if (players.Length > 0)
        {
            if (bvalue.horizontalInput == 1)
            {
                float maxX = float.MinValue;
                float maxY = float.MinValue;
                foreach (GameObject player in players)
                {
                    Vector3 playerPosition = player.transform.position;
                    if (playerPosition.x > maxX && playerPosition.y > maxY)
                    {
                        maxX = playerPosition.x;
                        maxY = playerPosition.y;
                        targetTransform = player.transform;
                    }
                }
            }
            else if (bvalue.horizontalInput == -1)
            {
                float minX = float.MaxValue;
                float maxY = float.MinValue;
                foreach (GameObject player in players)
                {
                    Vector3 playerPosition = player.transform.position;
                    if (playerPosition.x < minX && playerPosition.y > maxY)
                    {
                        minX = playerPosition.x;
                        maxY = playerPosition.y;
                        targetTransform = player.transform;
                    }
                }
            }
          /*  else // moveHorizontal == 0
            {
                float avgX = 0f;
                float avgY = 0f;
                foreach (GameObject player in players)
                {
                    Vector3 playerPosition = player.transform.position;
                    avgX += playerPosition.x;
                    avgY += playerPosition.y;
                }
                avgX /= players.Length;
                avgY /= players.Length;

                float minDistance = float.MaxValue;
                foreach (GameObject player in players)
                {
                    Vector3 playerPosition = player.transform.position;
                    float distance = Vector2.Distance(new Vector2(avgX, avgY), new Vector2(playerPosition.x, playerPosition.y));
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetTransform = player.transform;
                    }
                }
            }*/
        }

        // Set the target to follow to the appropriate player object
        if (targetTransform != null)
        {
            target = targetTransform;
        }
    }
}