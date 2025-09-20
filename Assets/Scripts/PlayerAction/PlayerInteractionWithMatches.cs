
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

        }
        _HUD.SetMacthesCountText(_amountOfMatches);

    }

    // Update is called once per frame
    void Update()
    {
        if (_throwMatchAction.WasPressedThisFrame() && _amountOfMatches != 0)
        {
            _amountOfMatches--;
            _HUD.SetMacthesCountText(_amountOfMatches);
            _positionToThrow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(_positionToThrow);
            Vector2 direction = _positionToThrow - new Vector2(transform.position.x, transform.position.y);
            Debug.Log(transform.position.x);
            Debug.Log(_positionToThrow);
            ThrowMatch(_positionToThrow,direction);
        }
    }

    private void ThrowMatch(Vector2 positionToThrow, Vector2 direction)
    {
        //TODO: spawn at hand position
        GameObject match = Instantiate(_matches, transform.position, Quaternion.identity);
        //TODO: how to add force right
        match.GetComponent<Rigidbody2D>().AddForce(direction.normalized * _throwForce);
        match.GetComponent<MatchesBehavior>().SetTargetPosition(positionToThrow);
    }

    public void PickUpMatches(int theNumberOfMatchesToPickUp)
    {
        _amountOfMatches += theNumberOfMatchesToPickUp;
        _HUD.SetMacthesCountText(_amountOfMatches);
    }

}
