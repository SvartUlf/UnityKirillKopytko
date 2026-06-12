using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputComponent _input;
    [SerializeField] private GameObject _inventoryRoot;

    private bool _isInventoryOpen = false;
    void Update()
    {
        HandleInventoryToggle();
    }
    private void HandleInventoryToggle()
    {
        if (!_input.GetInventoryOpen())
        {
            return;
        }

        _isInventoryOpen = !_isInventoryOpen;
        _inventoryRoot.SetActive(_isInventoryOpen);
    }
}
