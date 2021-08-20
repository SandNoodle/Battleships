using System;
using System.Collections.Generic;

public class Ship
{
	private readonly BoardCell _anchorLocation;
	private readonly ShipDirection _direction;
	private readonly int _length;

	private List<BoardCell> _shipCells;

	public BoardCell AnchorLocation { get => this._anchorLocation; }
	public ShipDirection ShipDirection { get => this._direction; }
	public int Length { get => this._length; }
	public List<BoardCell> ShipCells { get => this._shipCells; }

	public Ship(BoardCell anchorLocation, ShipDirection direction, int length)
	{
		if (length <= 0)
		{
			Console.WriteLine("Ship's length specified as less or equal 0! Correcting to 1.");
			this._length = 1;
		}

		this._anchorLocation = anchorLocation;
		this._direction = direction;
		this._length = length;

		GenerateShipCells();
	}

	public void HitCell(BoardCell location)
	{
		if (this._shipCells.Contains(location))
		{
			this._shipCells.Remove(location);
		}
	}

	public bool ContainsCell(BoardCell location)
	{
		return this._shipCells.Contains(location);
	}

	public bool IsSunk()
	{
		return this._shipCells.Count == 0;
	}
	
	private void GenerateShipCells()
	{
		this._shipCells = new List<BoardCell>();
		for (int i = 0; i < this._length; i++)
		{
			if (this._direction == ShipDirection.Horizontal)
			{
				this._shipCells.Add(new BoardCell(_anchorLocation.X + i, _anchorLocation.Y));
			}
			else
			{
				this._shipCells.Add(new BoardCell(_anchorLocation.X, _anchorLocation.Y + i));
			}
		}
	}
}