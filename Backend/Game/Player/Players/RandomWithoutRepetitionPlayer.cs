using System;
using System.Collections.Generic;

/**
	<summary>
		<c>RandomWithoutRepetitionPlayer</c> selects random cell to fire - does not repeat checked cells.
	</summary>
*/
public class RandomWithoutRepetitionPlayer : IPlayer
{
	private Random _random;

	private Board _board;
	private Board _firingBoard;
	private List<Ship> _shipList;

	public string Name { get => "Random Without Repetition Player"; }
	public Board Board { get => this._board; set => this._board = value; }
	public Board FiringBoard { get => this._firingBoard; set => this._firingBoard = value; }
	public List<Ship> Ships { get => this._shipList; set => this._shipList = value; }

	public void NewGame(BoardSize boardSize)
	{
		_random = new Random();
		this._board = new Board(boardSize);
		this._firingBoard = new Board(boardSize);
		this._shipList = new List<Ship>();
	}

	public BoardCell FireShot()
	{
		BoardCell temp;
		while (true)
		{
			int randomX = _random.Next(this._firingBoard.BoardSize.Width);
			int randomY = _random.Next(this._firingBoard.BoardSize.Height);
			if (_firingBoard.BoardCells[randomX, randomY] == CellStatus.Unchecked)
			{
				temp = new BoardCell(randomX, randomY);
				break;
			}
		}

		return temp;
	}

	public void ShotResult(BoardCell shotLocation, CellStatus shotResult)
	{
		_firingBoard.BoardCells[shotLocation.X, shotLocation.Y] = shotResult;
	}

	// Unused for this player
	public void OpponentShot(BoardCell shotLocation) { }
}