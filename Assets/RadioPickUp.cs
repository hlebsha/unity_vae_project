using UnityEngine;

public class RadioPickUp : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isHeld = !isHeld;

            if (isHeld)
            {
                rb.isKinematic = true;  // Disables physics simulation
                transform.SetParent(Camera.main.transform);
                transform.localPosition = new Vector3(0, -0.5f, 1); // Position offset
            }
            else
            {
                transform.SetParent(null);
                rb.isKinematic = false;  // Enables physics simulation
            }
        }
    }
}