using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField] private RotateMode rotateMode = RotateMode.Y;
	[SerializeField] private bool isLefToRightIfRotateModeIsZ;
	[SerializeField] private float speed = 100;

	private Quaternion _startRotate;
	private Vector3 _angle;


	private void OnEnable() => StartRotate();

	private void OnDisable() => StopRotate();

	public void StartRotate()
	{
		_angle = transform.eulerAngles;
		_startRotate = gameObject.transform.rotation;
		
		if (IsInvoking(nameof(RotateGameObject)) is false) 
			InvokeRepeating(nameof(RotateGameObject), default, Time.fixedDeltaTime);
	}

	public void StopRotate()
	{
		gameObject.transform.rotation = _startRotate;
		
		if (IsInvoking(nameof(RotateGameObject)))
			CancelInvoke(nameof(RotateGameObject));
	}

	private void RotateGameObject()
	{
		switch (rotateMode)
		{
			case RotateMode.X:
				_angle.x += Time.fixedDeltaTime * speed;
				break;
			case RotateMode.Y:
				_angle.y += Time.fixedDeltaTime * speed;
				break;
			case RotateMode.Z:
				_angle.z += GetZAngle();
				break;
		}
		
		transform.eulerAngles = _angle;
	}

	private float GetZAngle()
	{
		return isLefToRightIfRotateModeIsZ ? -1 : 1 * Time.fixedDeltaTime * speed;
	}
}

[System.Serializable]
public enum RotateMode
{
	X = 0,
	Y = 1,
	Z = 2,
}
