using UnityEngine;
using System.Collections;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bridgePanelPrefab;
    [SerializeField] private int panelCount = 8;
    [SerializeField] private float panelWidth = 1f;
    [SerializeField] private Vector3 bridgeStartPosition = Vector3.zero;
    [SerializeField] private Vector3 bridgeEndPosition = new Vector3(10, -2, 0);

    private void Start()
    {
        StartCoroutine(SpawnBridgeDelayed());
    }

    private IEnumerator SpawnBridgeDelayed()
    {
        GameObject[] panels = new GameObject[panelCount];
        
        // Calculate direction and spacing to fit exactly between start and end
        Vector3 direction = (bridgeEndPosition - bridgeStartPosition).normalized;
        float totalDistance = Vector3.Distance(bridgeStartPosition, bridgeEndPosition);
        float spacing = totalDistance / (panelCount - 1);

        // Step 1: Instantiate all panels along the line from start to end
        for (int i = 0; i < panelCount; i++)
        {
            Vector3 panelPos = bridgeStartPosition + direction * (i * spacing);
            
            // Rotate panels to match the bridge angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            panels[i] = Instantiate(bridgePanelPrefab, panelPos, rotation, transform);
            panels[i].name = $"BridgePanel_{i}";
            
            Rigidbody2D rb = panels[i].GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            
            // Freeze end caps
            if (i == 0 || i == panelCount - 1)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
                                 RigidbodyConstraints2D.FreezePositionY;
            }
        }

        yield return null;

        // Step 2: Connect joints
        for (int i = 0; i < panelCount - 1; i++)
        {
            HingeJoint2D hinge = panels[i].AddComponent<HingeJoint2D>();
            hinge.connectedBody = panels[i + 1].GetComponent<Rigidbody2D>();
            hinge.autoConfigureConnectedAnchor = false;
            
            hinge.anchor = new Vector2(panelWidth / 2f, 0);
            hinge.connectedAnchor = new Vector2(-panelWidth / 2f, 0);
            hinge.useLimits = false;
        }

        yield return null;

        // Step 3: Enable gravity on middle panels
        for (int i = 1; i < panelCount - 1; i++)
        {
            panels[i].GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}