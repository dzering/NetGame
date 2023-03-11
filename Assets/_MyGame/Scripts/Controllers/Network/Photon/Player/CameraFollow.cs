using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float FollowSpeed = 2f;
	private Transform _target;
	private Vector3 _cameraOffset = Vector3.zero;

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform _cameraTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.1f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;
	private bool _isFollowing;
	private float height = 1.0f;
	private float distance = 10.0f;

	void Awake()
	{
		Cursor.visible = false;
	}

	void OnEnable()
	{
		//originalPos = _cameraTransform.localPosition;
	}

	private void LateUpdate()
	{
		if (_cameraTransform == null && _isFollowing)
		{
			OnStartFollowing();
		}
		if(_isFollowing)
			Follow();
	}

	private void Follow()
	{
		Vector3 newPosition = this.transform.position;
		newPosition.z = - distance;
		_cameraTransform.position = Vector3.Slerp(_cameraTransform.position, newPosition, FollowSpeed * Time.deltaTime);

		if (shakeDuration > 0)
		{
			_cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		Cut();
	}

	public void ShakeCamera()
	{
		originalPos = _cameraTransform.localPosition;
		shakeDuration = 0.2f;
	}

	public void OnStartFollowing()
	{
		_cameraTransform = Camera.main.transform;
		_isFollowing = true;
		originalPos = _cameraTransform.localPosition;
	}
	
	void Cut()
	{
		_cameraOffset.z = -distance;
		_cameraOffset.y = height;

		_cameraTransform.position = this.transform.position + this.transform.TransformVector(_cameraOffset);

		//_camTransform.LookAt(this.transform.position + centerOffset);
	}
}
