
using Com.DarkLynxDEV.Player;
using UnityEngine;

public class TowerSegment : MonoBehaviour
{
    #region Variables

    private TowerSpawner towerSpawner;
    private BoxCollider boxCollider;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start() {
        towerSpawner = FindObjectOfType<TowerSpawner>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check that the collider that triggers is a player
        if(other.gameObject.CompareTag("Player")) {

            //Create the next tower segment and disable the trigger collider
            towerSpawner.SpawnTowerSegment();
            boxCollider.enabled = false;
        }
    }

    #endregion
}
