using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerEconomy : MonoBehaviour
{
    public int Money = 10;
    public Camera Camera; 
    public GameObject TowerPlaced;
    private bool Activated = false; //Prevents double clicks
    private float PlacementCooldown = 1f;

    public void Start()
    {
        Money = 10;
    }
    private void FixedUpdate()
    {
        Activated = false;
        if (Input.GetMouseButtonDown(1) && !Activated)//Have they right clicked and the cooldown is done
        {
            Activated = true;
            PlaceTower();
            StartCoroutine(PlacementDelay()); //Starts the cooldown
        }
    }
    void PlaceTower()
    {
        Ray CameraRay = Camera.ScreenPointToRay(Input.mousePosition);//Where is the mouse

        if (Physics.Raycast(CameraRay, out RaycastHit hit) && Money >= 0 )
        {

            Money = Money - 5; //How much a tower costs
            Debug.Log("You placed a tower you have" + Money + "Left");//Testing Purposes
            Instantiate(TowerPlaced, hit.point, Quaternion.identity);//Crete the tower
            
        }
    }

    IEnumerator PlacementDelay()
    {
        yield return new WaitForSeconds(PlacementCooldown); // Wait for the cooldown
        Activated = false;
        
    }
    
    public void AddMoney(int AmountAdded)
    {
        Money += AmountAdded;
    }
    
    public int ReturnMoney()
    {
        return Money;
    }
}
