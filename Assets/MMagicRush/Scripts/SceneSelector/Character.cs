using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 3f;
    public bool IsMoving { get; private set; }

    public Pin CurrentPin { get; private set; }
    private Pin _targetPin;
    private MapManager _mapManager;


    public void Initialise(MapManager mapManager, Pin startPin)
    {
        _mapManager = mapManager;
        SetCurrentPin(startPin);
    }
    
    
    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void Update()
    {
        if (_targetPin == null) return;

        // Get the characters current position and the targets position
        var currentPosition = transform.position;
        var targetPosition = _targetPin.transform.position;

        // If the character isn't that close to the target move closer
        if (Vector3.Distance(currentPosition, targetPosition) > .02f)
        {
            transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                Time.deltaTime * Speed
            );
        }
        else
        {
            if (_targetPin.IsAutomatic)
            {
                // Get a direction to keep moving in
                var pin = _targetPin.GetNextPin(CurrentPin);
                MoveToPin(pin);
            }
            else
            {
                SetCurrentPin(_targetPin);
            }
        }

	
    }

    
    /// <summary>
    /// Check the if the current pin has a reference to another in a direction
    /// If it does the move there
    /// </summary>
    /// <param name="direction"></param>
    public void TrySetDirection(Direction direction)
    {
        // Try get the next pin
        var pin = CurrentPin.GetPinInDirection(direction);

//		if (direction == Direction.Down) {
//			Debug.Log ("moving down");
//		} else if (direction == Direction.Up) {
//			Debug.Log ("moving up");
//		} else {
//			Debug.Log ("moving elsewere");
//		}
        
        // If there is a pin then move to it
        if (pin == null) return;
        MoveToPin(pin);
    }

	public Pin NextPin()
	{
		// Try get the next pin
		if (CurrentPin.GetPinInDirectionUnhiden (Direction.Up) != null) {
			return CurrentPin.GetPinInDirectionUnhiden (Direction.Up);
		} else {
			return null;
		}

	}

	public Pin PreviousPin()
	{
		// Try get the next pin
		if (CurrentPin.GetPinInDirectionUnhiden (Direction.Down) != null) {
			return CurrentPin.GetPinInDirectionUnhiden (Direction.Down);
		} else {
			return null;
		}
	}


    /// <summary>
    /// Move to a new pin
    /// </summary>
    /// <param name="pin"></param>
    private void MoveToPin(Pin pin)
    {
        _targetPin = pin;
        IsMoving = true;
    }

    
    /// <summary>
    /// Set the current pin
    /// </summary>
    /// <param name="pin"></param>
    public void SetCurrentPin(Pin pin)
    {
        CurrentPin = pin;
        _targetPin = null;
        transform.position = pin.transform.position;
        IsMoving = false;
        
        // Tell the map manager that
        // the current pin has changed
        _mapManager.UpdateGui();
    }
}