using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float FollowSpeed = 2f;
	private Transform _target;
	private Vector3 _cameraOffset = Vector3.zero;

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform _camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.1f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;
	private bool _isFollow;
	private float height = 1.0f;
	private float distance = 10.0f;

	void Awake()
	{
		_camTransform = Camera.main.transform;
		Cursor.visible = false;
	}

	void OnEnable()
	{
		originalPos = _camTransform.localPosition;
	}

	private void LateUpdate()
	{
		if(!_isFollow)
			return;

		Follow();
	}

	private void Follow()
	{
		Vector3 newPosition = _target.position;
		newPosition.z = - distance;
		_camTransform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);

		if (shakeDuration > 0)
		{
			_camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		Cut();
	}

	public void ShakeCamera()
	{
		originalPos = _camTransform.localPosition;
		shakeDuration = 0.2f;
	}

	public void OnStartFollowing()
	{
		_target = this.gameObject.transform;
		_isFollow = true;
	}
	
	void Cut()
	{
		_cameraOffset.z = -distance;
		_cameraOffset.y = height;

		_camTransform.position = this.transform.position + this.transform.TransformVector(_cameraOffset);

		//_camTransform.LookAt(this.transform.position + centerOffset);
	}
}
