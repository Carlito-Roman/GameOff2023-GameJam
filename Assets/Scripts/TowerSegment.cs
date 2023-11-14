
using UnityEngine;

public class TowerSegment : MonoBehaviour
{
    #region Variables

    private TowerSpawner towerSpawner;
    private BoxCollider boxCollider;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        towerSpawner = FindObjectOfType<TowerSpawner>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        towerSpawner.SpawnTowerSegment();
        boxCollider.enabled = false;

    }

    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    #endregion
}
