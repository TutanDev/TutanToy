
using Silk.NET.Input;
using Silk.NET.Maths;
using System.Numerics;
using ToyEngine.Math;

namespace ToyEngine.Render;

public class Camera
{
	public Matrix4x4 View => Matrix4x4.CreateLookAt(_position, _position + _front, _up);

	private float _nearPlaneDistance = 0.1f;
	private float _farPlaneDistance = 100.0f;


	private Vector3 _position = new Vector3(0.0f, 0.0f, 3.0f);
	private Vector3 _front = new Vector3(0.0f, 0.0f, -1.0f);
	private Vector3 _up = Vector3.UnitY;
	private Vector3 _direction = Vector3.Zero;

	// euler Angles
	private float _yaw = -90f;
	private float _pitch = 0f;

	// Options
	float _movementSpeed;
	float _mouseSensitivity;
	float _zoom = 45f;

	private Vector2 _lastMousePosition;

	private IInputContext _input;
	private IKeyboard _primaryKeyboard;

	public Camera(IInputContext input)
	{
		_input = input;
		_primaryKeyboard = input.Keyboards.FirstOrDefault();

		for (int i = 0; i < input.Mice.Count; i++)
		{
			//input.Mice[i].Cursor.CursorMode = CursorMode.Raw;
			input.Mice[i].MouseMove += OnMouseMove;
			input.Mice[i].Scroll += OnMouseWheel;
		}
	}

	public Matrix4x4 GetProjectionMatrix(Vector2D<int> size)
		=> Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(_zoom),
												  (float)size.X / size.Y,
												  _nearPlaneDistance,
												  _farPlaneDistance);

	public void Update(float dt)
	{
		var moveSpeed = 2.5f * dt;

		if (_primaryKeyboard.IsKeyPressed(Key.W))
		{
			//Move forwards
			_position += moveSpeed * _front;
		}
		if (_primaryKeyboard.IsKeyPressed(Key.S))
		{
			//Move backwards
			_position -= moveSpeed * _front;
		}
		if (_primaryKeyboard.IsKeyPressed(Key.A))
		{
			//Move left
			_position -= Vector3.Normalize(Vector3.Cross(_front, _up)) * moveSpeed;
		}
		if (_primaryKeyboard.IsKeyPressed(Key.D))
		{
			//Move right
			_position += Vector3.Normalize(Vector3.Cross(_front, _up)) * moveSpeed;
		}
	}

	private void OnMouseMove(IMouse mouse, Vector2 position)
	{
		var lookSensitivity = 0.1f;
		if (_lastMousePosition == default) { _lastMousePosition = position; }
		else
		{
			var xOffset = (position.X - _lastMousePosition.X) * lookSensitivity;
			var yOffset = (position.Y - _lastMousePosition.Y) * lookSensitivity;
			_lastMousePosition = position;

			_yaw += xOffset;
			_pitch -= yOffset;

			//We don't want to be able to look behind us by going over our head or under our feet so make sure it stays within these bounds
			_pitch = System.Math.Clamp(_pitch, -89.0f, 89.0f);

			_direction.X = MathF.Cos(MathHelper.DegreesToRadians(_yaw)) * MathF.Cos(MathHelper.DegreesToRadians(_pitch));
			_direction.Y = MathF.Sin(MathHelper.DegreesToRadians(_pitch));
			_direction.Z = MathF.Sin(MathHelper.DegreesToRadians(_yaw)) * MathF.Cos(MathHelper.DegreesToRadians(_pitch));
			_front = Vector3.Normalize(_direction);
		}
	}

	private void OnMouseWheel(IMouse mouse, ScrollWheel wheel)
	{
		//We don't want to be able to zoom in too close or too far away so clamp to these values
		_zoom = System.Math.Clamp(_zoom - wheel.Y, 1.0f, 45f);
	}
}