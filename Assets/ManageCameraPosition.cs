using UnityEngine;

public class ManageCameraPosition : MonoBehaviour
{
    [SerializeField] private Transform camPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = camPos.position;
    }
}
