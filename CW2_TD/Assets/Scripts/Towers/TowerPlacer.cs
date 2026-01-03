using UnityEngine;
using static GameManager;
public class TowerPlacer : MonoBehaviour
{
    public TowerData SelectedTower;
    public LayerMask GroundMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    void TryPlaceTower()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Build)
            return;

        if (SelectedTower == null) return;

        if (!GameManager.Instance.CanAfford(SelectedTower.Cost))
        {
            Debug.Log("Not enough money");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, GroundMask))
        {
            Instantiate( SelectedTower.TowerPrefab,hit.point, Quaternion.identity);

            GameManager.Instance.SpendMoney(SelectedTower.Cost);
        }
    }
 

}
