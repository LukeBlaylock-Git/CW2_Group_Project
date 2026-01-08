using UnityEngine;
using static GameManager;
public class TowerPlacer : MonoBehaviour
{
    public TowerData SelectedTower;
    public LayerMask GroundMask; //The Layer mask we will be using to say "yes you can place a tower here"

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //CHecking for a LMB input.
        {
            TryPlaceTower();
        }
    }

    void TryPlaceTower()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Build) //If the game phase is NOT build, do nothing
            return;

        if (SelectedTower == null) return; //if selected tower is null do nothing (To be configured and added on later.)

        if (!GameManager.Instance.CanAfford(SelectedTower.Cost)) //Check if the player has enough money to place this tower.
        {
            Debug.Log("Not enough money");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Create the ray cast which comes from the camera

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, GroundMask)) //If we hit a appropriate Groundmask, LMB is clicked, we instantiate the currently selected tower.
        {
            Instantiate( SelectedTower.TowerPrefab,hit.point, Quaternion.identity);

            GameManager.Instance.SpendMoney(SelectedTower.Cost);
        }
    }
 

}
