
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionWithMatches : MonoBehaviour
{

    [SerializeField] private int _amountOfMatches = 3;
    [SerializeField] private GameObject _matches;
    [SerializeField] private float _throwForce = 1;
    [SerializeField] private HUD _HUD;
    [SerializeField] private Transform _rightHand;

    private InputAction _throwMatchAction;
    private Vector2 _positionToThrow;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _throwMatchAction = InputSystem.actions.FindAction("ThrowMatch");
        if (_HUD == null)
        {
            _HUD = FindFirstObjectByType<Canvas>().GetComponent<HUD>();
            _HUD.SetMacthesCountText(_amountOfMatches);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_throwMatchAction.WasPressedThisFrame() && _amountOfMatches != 0)
        {
            _amountOfMatches--;
            _HUD.SetMacthesCountText(_amountOfMatches);
            _positionToThrow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _positionToThrow = _positionToThrow - new Vector2(_rightHand.position.x, _rightHand.position.y);
            ThrowMatch(_positionToThrow, Vector2.Distance(_positionToThrow, transform.position));
        }
    }

    private void ThrowMatch(Vector2 positionToThrow, float distance)
    {
        //TODO: spawn at hand position
        GameObject match = Instantiate(_matches, _rightHand.position, Quaternion.identity);
        //TODO: how to add force right
        match.GetComponent<Rigidbody2D>().AddForce(positionToThrow.normalized * _throwForce);
        match.GetComponent<MatchesBehavior>().SetTargetPosition(positionToThrow + new Vector2(transform.position.x, transform.position.y));
    }

    public void PickUpMatches(int theNumberOfMatchesToPickUp)
    {
        _amountOfMatches += theNumberOfMatchesToPickUp;
        _HUD.SetMacthesCountText(_amountOfMatches);
    }

}
