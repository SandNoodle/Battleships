using System;
using System.Collections.Generic;

/**
	<summary>
		<c>RandomPlayer</c> selects random cell to fire - even previously checked.
	</summary>
*/
public class RandomPlayer : IPlayer
{
	private Random random;
	private Board _board;
	private Board _firingBoard;
	private List<Ship> _shipList;

	public string Name { get => "Random Player"; }
	public Board Board { get => this._board; set => this._board = value; }
	public Board FiringBoard { get => this._firingBoard; set => this._firingBoard = value; }
	public List<Ship> Ships { get => this._shipList; set => this._shipList = value; }

	public void NewGame(BoardSize boardSize)
	{
		random = new Random();
		this._board = new Board(boardSize);
		this._firingBoard = new Board(boardSize);
		this._shipList = new List<Ship>();
	}

	public BoardCell FireShot()
	{
		return new BoardCell(random.Next(this._firingBoard.BoardSize.Width),
							random.Next(this._firingBoard.BoardSize.Height));
	}

	// Unused for this player
	public void ShotResult(BoardCell shotLocation, CellStatus shotResult) { }
	public void OpponentShot(BoardCell shotLocation) { }
}