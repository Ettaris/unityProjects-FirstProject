using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInteractionWithMatches _playerMatches;
    private CheckpointInteract _currentInteraction;
    private PlayerBasicMovement _playerMovement;
    private bool _isInteracting;

    private void Start()
    {
        _playerMatches = GetComponent<PlayerInteractionWithMatches>();
        _playerMovement = GetComponent<PlayerBasicMovement>();

    }

    private void Update()
    {
        if (_isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                _currentInteraction.Interact();
                _currentInteraction.EndInteraction();
                GetComponent<Animator>().SetTrigger("InteractOff");
                _playerMovement.SetMoveAbilityOn();
                _isInteracting = false;
            }
        }
    }

    public void SetCurrentInteraction(CheckpointInteract interaction) => _currentInteraction = interaction;

    public void StartInteraction()
    {
        _currentInteraction.Interact();
        _playerMovement.SetMoveAbilityOff();
        _isInteracting = true;
    }


    public void PickUpMatchesFromTable()
    {
        if (!_currentInteraction.IsActivated())
        {
            _playerMatches.PickUpMatches(_currentInteraction.amountOfMatchesToPickUp);
            _currentInteraction.PickUpMatchesFromTable();
        }
    }

    public bool IsInteracting()
    {
        if (_isInteracting)
            return true;
        return false;

    }
}