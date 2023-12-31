
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{

    #region Variables

    [SerializeField] private GameObject towerSegmentPrefab;
    private Vector3 nextSpawnPoint;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        //Test
        for (int i = 0; i < 3; i++) {
            SpawnTowerSegment();
        }
    }

    #endregion

    #region Private Methods


    #endregion

    #region Public Methods

    public void SpawnTowerSegment()
    {

        //Spawn a new segment and set its empty gameObject to the next spawn point
        GameObject tempTower = Instantiate(towerSegmentPrefab, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = tempTower.transform.GetChild(1).transform.position;
    }

    #endregion

}
