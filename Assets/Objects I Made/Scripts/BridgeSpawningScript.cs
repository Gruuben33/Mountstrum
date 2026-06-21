using UnityEngine;
using System.Collections;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bridgePanelPrefab;
    [SerializeField] private int panelCount = 8;
    [SerializeField] private float panelWidth = 1f;
    [SerializeField] private Vector2 bridgeStartPosition = Vector2.zero;
    [SerializeField] private Vector2 bridgeEndPosition = new Vector2(10, -2);
    [SerializeField, Tooltip("If true, start/end are treated as local offsets from this spawner.")]
    private bool positionsAreLocal = true;

    private void Start()
    {
        StartCoroutine(SpawnBridgeDelayed());
    }

    private IEnumerator SpawnBridgeDelayed()
    {
        if (bridgePanelPrefab == null)
        {
            Debug.LogError("BridgeSpawner: bridgePanelPrefab is not assigned.");
            yield break;
        }

        if (panelCount < 2)
        {
            Debug.LogWarning("BridgeSpawner requires at least 2 panels.");
            yield break;
        }

        GameObject[] panels = new GameObject[panelCount];

        // Work purely in 2D for calculations
        Vector2 spawnerPos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 worldStart = positionsAreLocal ? spawnerPos2D + bridgeStartPosition : bridgeStartPosition;
        Vector2 worldEnd = positionsAreLocal ? spawnerPos2D + bridgeEndPosition : bridgeEndPosition;

        // Calculate direction and spacing to fit exactly between start and end (center-to-center)
        Vector2 direction = (worldEnd - worldStart).normalized;
        float totalDistance = Vector2.Distance(worldStart, worldEnd);
        float spacing = totalDistance / (panelCount - 1);

        // Preserve the spawner's Z so panels stay on the same plane
        float z = transform.position.z;

        // Determine prefab's local sprite width (in local units) so we can scale to match `panelWidth`.
        float prefabSpriteLocalWidth = 1f;
        SpriteRenderer prefabSR = bridgePanelPrefab.GetComponentInChildren<SpriteRenderer>();
        if (prefabSR != null && prefabSR.sprite != null)
        {
            prefabSpriteLocalWidth = prefabSR.sprite.bounds.size.x;
        }
        else
        {
            Collider2D prefabCol = bridgePanelPrefab.GetComponentInChildren<Collider2D>();
            if (prefabCol != null)
            {
                float lossyScaleX = bridgePanelPrefab.transform.lossyScale.x;
                if (lossyScaleX != 0f)
                    prefabSpriteLocalWidth = prefabCol.bounds.size.x / lossyScaleX;
            }
            else
            {
                Debug.LogWarning("BridgeSpawner: couldn't determine prefab visual width; using 1 unit as base.");
            }
        }

        // base world width (before we apply our scale) = local sprite width * prefab local scale
        float prefabBaseLocalScaleX = bridgePanelPrefab.transform.localScale.x;
        float prefabBaseWorldWidth = prefabSpriteLocalWidth * prefabBaseLocalScaleX;
        float scaleFactor = 1f;
        if (prefabBaseWorldWidth > 0f)
            scaleFactor = panelWidth / prefabBaseWorldWidth;

        // Step 1: Instantiate all panels along the line from start to end
        for (int i = 0; i < panelCount; i++)
        {
            Vector2 panelPos2D = worldStart + direction * (i * spacing);

            // Rotate panels to match the bridge angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            // Instantiate as child, apply scale to achieve requested `panelWidth`, then set position/rotation
            panels[i] = Instantiate(bridgePanelPrefab, transform);
            panels[i].name = $"BridgePanel_{i}";
            panels[i].transform.localScale = bridgePanelPrefab.transform.localScale * scaleFactor;
            panels[i].transform.position = new Vector3(panelPos2D.x, panelPos2D.y, z);
            panels[i].transform.rotation = rotation;

            Rigidbody2D rb = panels[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f;
                rb.linearVelocity = Vector2.zero;

                // Freeze end caps completely so bridge anchors stay fixed
                if (i == 0 || i == panelCount - 1)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
            else
            {
                Debug.LogWarning($"BridgeSpawner: panel {i} has no Rigidbody2D.");
            }
        }

        yield return null;

        // Step 2: Connect joints
        for (int i = 0; i < panelCount - 1; i++)
        {
            Rigidbody2D rbA = panels[i].GetComponent<Rigidbody2D>();
            Rigidbody2D rbB = panels[i + 1].GetComponent<Rigidbody2D>();
            if (rbA == null || rbB == null) continue;

            HingeJoint2D hinge = panels[i].AddComponent<HingeJoint2D>();
            hinge.connectedBody = rbB;
            hinge.autoConfigureConnectedAnchor = false;

            // anchors are in the panel's local units. Use half the sprite's local width so anchors match visual edges.
            hinge.anchor = new Vector2(prefabSpriteLocalWidth * 0.5f, 0f);
            hinge.connectedAnchor = new Vector2(-prefabSpriteLocalWidth * 0.5f, 0f);
            hinge.useLimits = false;
        }

        yield return null;

        // Step 3: Enable gravity on middle panels
        for (int i = 1; i < panelCount - 1; i++)
        {
            Rigidbody2D rb = panels[i].GetComponent<Rigidbody2D>();
            if (rb != null) rb.gravityScale = 1f;
        }
    }
}