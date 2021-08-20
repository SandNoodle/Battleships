using System;

public struct BoardSize
{
	private readonly int _width;
	private readonly int _height;

	public BoardSize(int width, int height)
	{
		if (width <= 0 || height <= 0)
		{
			throw new ArgumentOutOfRangeException("Board size dimension cannot be less or equal to 0!");
		}

		this._width = width;
		this._height = height;
	}

	public int Width { get => this._width; }
	public int Height { get => this._height; }
}