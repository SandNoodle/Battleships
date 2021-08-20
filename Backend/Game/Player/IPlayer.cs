using System.Collections.Generic;

public interface IPlayer
{
	string Name { get; }
	Board Board { get; set; }
	Board FiringBoard { get; set; }
	List<Ship> Ships { get; set; }

	void NewGame(BoardSize boardSize);

	// Attack
	BoardCell FireShot();
	void ShotResult(BoardCell shotLocation, CellStatus shotResult);

	// Defence
	void OpponentShot(BoardCell shotLocation);
}