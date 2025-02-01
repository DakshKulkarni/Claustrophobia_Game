using UnityEngine;

public class DynamicRocksNarrowing : MonoBehaviour
{
    public Transform player; // Reference to the XR Origin or Camera
    public Transform[] rocks; // Array of all the rocks
    public float moveSpeed = 1f; // Speed at which rocks move inward
    public float minWidth = 1f; // Minimum allowable width between the rocks
    public float triggerDistance = 2f; // Z-axis distance for triggering narrowing effect

    private Vector3[] originalPositions; // Store original positions of rocks

    void Start()
    {
        // Store the original positions of all rocks
        originalPositions = new Vector3[rocks.Length];
        for (int i = 0; i < rocks.Length; i++)
        {
            originalPositions[i] = rocks[i].position;
        }
    }

    void Update()
    {
        // Check if the player is aligned with the rocks on the Z-axis
        if (IsPlayerInTriggerZone())
        {
            MoveRocksInward();
        }
        else
        {
            ResetRocksToOriginalPositions();
        }
    }

    private bool IsPlayerInTriggerZone()
    {
        foreach (Transform rock in rocks)
        {
            // If any rock is within the trigger distance along the Z-axis
            if (Mathf.Abs(player.position.z - rock.position.z) <= triggerDistance)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveRocksInward()
    {
        foreach (Transform rock in rocks)
        {
            float step = moveSpeed * Time.deltaTime;

            // Determine if the rock is on the left or right of the player
            if (rock.position.x < player.position.x) // Left side
            {
                // Move right towards the player
                rock.position = Vector3.MoveTowards(rock.position, new Vector3(player.position.x - minWidth / 2, rock.position.y, rock.position.z), step);
            }
            else // Right side
            {
                // Move left towards the player
                rock.position = Vector3.MoveTowards(rock.position, new Vector3(player.position.x + minWidth / 2, rock.position.y, rock.position.z), step);
            }
        }
    }

    private void ResetRocksToOriginalPositions()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            float step = moveSpeed * Time.deltaTime;
            rocks[i].position = Vector3.MoveTowards(rocks[i].position, originalPositions[i], step);
        }
    }
}
