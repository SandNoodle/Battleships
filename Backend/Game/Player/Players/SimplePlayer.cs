using System;
using System.Collections.Generic;

/**
	<summary>
		<c>SimplePlayer</c> - fires cells without repetition at random.
		Checks neighbouring cells (top, down, left, right) when hit status equals to HIT or SUNK.
	</summary>
*/
public class SimplePlayer : IPlayer
{
	private Random _random;
	private List<BoardCell> _priorityCells;

	private Board _board;
	private Board _firingBoard;
	private List<Ship> _shipList;

	public string Name { get => "Simple Player"; }
	public Board Board { get => this._board; set => this._board = value; }
	public Board FiringBoard { get => this._firingBoard; set => this._firingBoard = value; }
	public List<Ship> Ships { get => this._shipList; set => this._shipList = value; }

	public void NewGame(BoardSize boardSize)
	{
		_random = new Random();
		this._board = new Board(boardSize);
		this._firingBoard = new Board(boardSize);
		this._shipList = new List<Ship>();
		this._priorityCells = new List<BoardCell>();
	}

	public BoardCell FireShot()
	{
		// Fire at random cell from priority list
		if (this._priorityCells.Count > 0)
		{
			int randomCell = this._random.Next(this._priorityCells.Count);
			BoardCell priorityCell = this._priorityCells[randomCell];
			this._priorityCells.Remove(priorityCell);

			return priorityCell;
		}

		// Default behaviour: Fire at random unchecked cell
		while (true)
		{
			int randomX = _random.Next(this._firingBoard.BoardSize.Width);
			int randomY = _random.Next(this._firingBoard.BoardSize.Height);
			if (_firingBoard.BoardCells[randomX, randomY] == CellStatus.Unchecked)
			{
				return new BoardCell(randomX, randomY);
			}
		}
	}

	public void ShotResult(BoardCell shotLocation, CellStatus shotResult)
	{
		_firingBoard.BoardCells[shotLocation.X, shotLocation.Y] = shotResult;

		// Add horizontal and vertical neighbours to priority list
		if (shotResult == CellStatus.Hit || shotResult == CellStatus.Sunk)
		{
			AddToPriorityListIfValid(shotLocation.X + 1, shotLocation.Y);
			AddToPriorityListIfValid(shotLocation.X - 1, shotLocation.Y);
			AddToPriorityListIfValid(shotLocation.X, shotLocation.Y + 1);
			AddToPriorityListIfValid(shotLocation.X, shotLocation.Y - 1);
		}
	}

	// Unused for this player
	public void OpponentShot(BoardCell shotLocation) { }

	// Helper methods
	private bool IsCellInbounds(int cellX, int cellY)
	{
		if (cellX < 0 ||
			cellY < 0 ||
			cellX >= _firingBoard.BoardSize.Width ||
			cellY >= _firingBoard.BoardSize.Height)
		{
			return false;
		}

		return true;
	}

	private void AddToPriorityListIfValid(int cellX, int cellY)
	{
		if (IsCellInbounds(cellX, cellY) && this._firingBoard.BoardCells[cellX, cellY] == CellStatus.Unchecked)
		{
			this._priorityCells.Add(new BoardCell(cellX, cellY));
		}
	}
}