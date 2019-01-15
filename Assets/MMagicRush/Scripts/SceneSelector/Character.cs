using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 3f;
    public bool IsMoving { get; private set; }

    public Pin CurrentPin { get; private set; }
	public Pin _targetPin;
    private MapManager _mapManager;

	public GameObject[] CharacterAnimators;
	public GameObject ActualAnimator;
	public Pin NextPinTest;

	public Pin[] GenericPins;
	public Pin[] ForestPins;
	public Pin[] FrozenPins;
	public Pin[] DungeonPins;
	public int atualpin;
	public Pin targetpin;
	public bool reached;
	public bool increase;

    public void Initialise(MapManager mapManager, Pin startPin)
    {
        _mapManager = mapManager;
        SetCurrentPin(startPin);
    }
    
	public void Start(){
		reached = true;
		
		if (PlayerPrefs.GetInt ("Character") == null) {
			PlayerPrefs.SetInt ("Character", 1);
		}
		this.GenericPins = FrozenPins;
		ActualAnimator = CharacterAnimators [PlayerPrefs.GetInt ("Character")];
		ActualAnimator.GetComponent<SpriteRenderer> ().enabled = true;

	}
    
    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void Update()
	{

//		NextPinTest = NextPin ();
//        if (_targetPin == null) return;
//
//        // Get the characters current position and the targets position
//        var currentPosition = transform.position;
//        var targetPosition = _targetPin.transform.position;
//
//        // If the character isn't that close to the target move closer
//        if (Vector3.Distance(currentPosition, targetPosition) > .02f)
//        {
//            transform.position = Vector3.MoveTowards(
//                currentPosition,
//                targetPosition,
//                Time.deltaTime * Speed
//            );
//        }
//        else
//        {
//            if (_targetPin.IsAutomatic)
//            {
//                // Get a direction to keep moving in
//                var pin = _targetPin.GetNextPin(CurrentPin);
//                MoveToPin(pin);
//            }
//            else
//            {
//                SetCurrentPin(_targetPin);
//            }
//        }

		if (targetpin != null && reached == true){
			
		if (this.atualpin > targetpin.GetComponent<Pin> ().PinID) {
			reached = false;
				increase = false;	
			
		} else if (this.atualpin < targetpin.GetComponent<Pin> ().PinID) {
			reached = false;
				increase = true;

		} else {
			reached = true;
			IsMoving = false;
//				Debug.Log ("AA");
		}
		}

		if (reached == false && increase == true && Vector2.Distance (this.transform.position, GenericPins [atualpin + 1].transform.position) > 0.2f) {
			if (GenericPins [atualpin + 1].transform.position.x < this.transform.position.x) {
				ActualAnimator.GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				ActualAnimator.GetComponent<SpriteRenderer> ().flipX = false;
			}

			transform.position = Vector3.MoveTowards(
				this.transform.position,
				GenericPins [atualpin + 1].transform.position,
				Time.deltaTime * Speed
			);
			IsMoving = true;

		} else if(reached == false && increase == true && IsMoving == true){
			IsMoving = false;
			reached = true;
			atualpin += 1;
		}

		if (reached == false && increase == false && Vector2.Distance (this.transform.position, GenericPins [atualpin - 1].transform.position) > 0.2f) {
			if (GenericPins [atualpin - 1].transform.position.x < this.transform.position.x) {
				ActualAnimator.GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				ActualAnimator.GetComponent<SpriteRenderer> ().flipX = false;
			}

			transform.position = Vector3.MoveTowards (
				this.transform.position,
				GenericPins [atualpin - 1].transform.position,
				Time.deltaTime * Speed
			);
			IsMoving = true;
		} else if(reached == false && increase == false && IsMoving == true){
			IsMoving = false;
			reached = true;
			atualpin -= 1;
		}

		if (IsMoving) {
			ActualAnimator.GetComponent<Animator> ().SetBool ("Idle", false);
			ActualAnimator.GetComponent<Animator> ().SetBool ("Walk", true);

//			if (_targetPin.transform.position.x < this.transform.position.x) {
//				ActualAnimator.GetComponent<SpriteRenderer> ().flipX = true;
//			} else {
//				ActualAnimator.GetComponent<SpriteRenderer> ().flipX = false;
//			}
		} else {
			ActualAnimator.GetComponent<Animator> ().SetBool ("Walk", false);
			ActualAnimator.GetComponent<Animator> ().SetBool ("Idle", true);

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

	private void MoveToGenericPin(Pin pin)
	{

		if (pin.transform.position.x < this.transform.position.x) {
			ActualAnimator.GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			ActualAnimator.GetComponent<SpriteRenderer> ().flipX = false;
		}



		transform.position = Vector3.MoveTowards(
			this.transform.position,
			pin.transform.position,
			Time.deltaTime * 1000
		);
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