using System.Numerics;
using ToyEngine.Renderer.Interfaces;

namespace ToyEditor;

public class EditorCamera : ICamera
{
	public Matrix4x4 GetViewMatrix() => Matrix4x4.CreateLookAt(_position, _position + _front, _up);

	public Matrix4x4 GetProjectionMatrix()
		=> Matrix4x4.CreatePerspectiveFieldOfView(DegreesToRadians(_zoom),
												  _aspectRatio,
												  _nearPlaneDistance,
												  _farPlaneDistance);

	public void Dispose() { }

	private float _nearPlaneDistance = 0.1f;
	private float _farPlaneDistance = 100.0f;


	private Vector3 _position = new Vector3(0.0f, 0.0f, 5.0f);
	private Vector3 _front = new Vector3(0.0f, 0.0f, -1.0f);
	private Vector3 _up = Vector3.UnitY;
	private Vector3 _direction = Vector3.Zero;

	private float _aspectRatio = 1.0f;
	public void SetAspectRatio(float aspectRatio) => _aspectRatio = aspectRatio;

	// euler Angles
	private float _yaw = -90f;
	private float _pitch = 0f;

	// Options
	float _zoom = 45f;

	private float DegreesToRadians(float degrees) => MathF.PI / 180f * degrees;
}