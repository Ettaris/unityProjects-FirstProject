
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionWithMatches : MonoBehaviour
{

    [SerializeField] private int _amountOfMatches = 3;
    [SerializeField] private GameObject _matches;
    [SerializeField] private float _throwForce = 1;

    private InputAction _throwMatchAction;
    private Vector2 _positionToThrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _throwMatchAction = InputSystem.actions.FindAction("ThrowMatch");
    }

    // Update is called once per frame
    void Update()
    {
        if (_throwMatchAction.WasPressedThisFrame() && _amountOfMatches != 0)
        {
            _amountOfMatches--;
            _positionToThrow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _positionToThrow = _positionToThrow - new Vector2(transform.position.x, transform.position.y);
            ThrowMatch(_positionToThrow);

            //debug block; TODO: clean it
            Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition).normalized);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized);
        }
    }

    private void ThrowMatch(Vector2 positionToThrow)
    {
        GameObject match = Instantiate(_matches, transform.position, _matches.transform.rotation);
        match.GetComponent<Rigidbody2D>().AddForce(positionToThrow * _throwForce);
    }

    public void PickUpMatches(int theNumberOfMatchesToPickUp)
    {
        _amountOfMatches += theNumberOfMatchesToPickUp;
        //TODO: don't forget to clear it
        Debug.Log("the number of matches player has: " + _amountOfMatches);
    }

}
