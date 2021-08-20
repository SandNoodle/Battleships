using System;
using System.Linq;
using System.Collections.Generic;

public class Game
{

	public void PlayTournament(List<IPlayer> playersList, TournamentRules tournamentRules)
	{
		// Create score table
		Dictionary<IPlayer, int> playerScoreMap = new Dictionary<IPlayer, int>();
		foreach (IPlayer player in playersList)
		{
			playerScoreMap.Add(player, 0);
		}

		// Display information about tournament
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Starting tournament!");
		Console.ResetColor();
		Console.WriteLine("List of players:");
		foreach (IPlayer player in playersList)
		{
			Console.WriteLine("- {0}", player.Name);
		}
		Console.WriteLine();

		// Tournament matches people based on Double Round Robin rules.
		for (int i = 0; i < playersList.Count; i++)
		{
			for (int j = 0; j < playersList.Count; j++)
			{
				if (!playersList[i].Equals(playersList[j]))
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("{0} vs {1}", playersList[i], playersList[j]);
					Console.ResetColor();

					// Update scores for each player
					Tuple<int, int> matchScores = PlayMatch(playersList[i], playersList[j], tournamentRules);
					playerScoreMap[playersList[i]] += matchScores.Item1;
					playerScoreMap[playersList[j]] += matchScores.Item2;
				}
			}
		}

		// Display post-tournament scores descending
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Post Tournament standings:");
		Console.ResetColor();

		foreach (KeyValuePair<IPlayer, int> entry in playerScoreMap.OrderByDescending(key => key.Value))
		{
			float winPercentage = (entry.Value * 100) / playerScoreMap.Sum(x => x.Value);
			Console.WriteLine("- {0} scored {1} with {2} % wins!", entry.Key.Name, entry.Value, winPercentage);
		}

	}

	public Tuple<int, int> PlayMatch(IPlayer playerOne, IPlayer playerTwo, TournamentRules tournamentRules)
	{
		int playerOneScore = 0;
		int playerTwoScore = 0;

		for (int i = 0; i < tournamentRules.RoundsPerMatch; i++)
		{
			IPlayer roundWinnder = PlayRound(playerOne, playerTwo, tournamentRules);
			if (roundWinnder == playerOne)
			{
				playerOneScore++;
			}
			else
			{
				playerTwoScore++;
			}
		}

		// Post match information
		Console.WriteLine("{0} scored {1}!", playerOne.Name, playerOneScore);
		Console.WriteLine("{0} scored {1}!", playerTwo.Name, playerTwoScore);
		Console.WriteLine();

		return new Tuple<int, int>(playerOneScore, playerTwoScore);
	}

	public IPlayer PlayRound(IPlayer playerOne, IPlayer playerTwo, TournamentRules tournamentRules)
	{
		if (playerOne == null || playerTwo == null)
		{
			throw new ArgumentNullException("Match cannot be played when one or more players are null!");
		}

		// Prepare match
		ref IPlayer attackingPlayer = ref playerOne;
		ref IPlayer defendingPlayer = ref playerTwo;

		attackingPlayer.NewGame(tournamentRules.BoardSize);
		defendingPlayer.NewGame(tournamentRules.BoardSize);

		PlaceShipsForPlayer(attackingPlayer, tournamentRules.BoardSize, tournamentRules.ShipLengths);
		PlaceShipsForPlayer(defendingPlayer, tournamentRules.BoardSize, tournamentRules.ShipLengths);

		// NOTE: Player One will always be the first one to move.
		// Loop until one of the players win
		while (true)
		{
			// Attacking player fires a shot
			BoardCell shotLocation = attackingPlayer.FireShot();
			CellStatus shotResult = CellStatus.Miss;

			defendingPlayer.OpponentShot(shotLocation);

			// Defending player has to check if any of the ships were hit
			foreach (Ship ship in defendingPlayer.Ships)
			{
				if (ship.ContainsCell(shotLocation))
				{
					// Hit success
					shotResult = CellStatus.Hit;
					ship.HitCell(shotLocation);

					//  Ship is sunk when no cells remain in List<BoardCell>
					if (ship.IsSunk())
					{
						shotResult = CellStatus.Sunk;
						defendingPlayer.Ships.Remove(ship);
					}

					// No need to check further because ships cannot overlap.
					break;
				}

			}

			// Pass firing result back to attacking player
			attackingPlayer.ShotResult(shotLocation, shotResult);

			// Check if defending player lost
			if (defendingPlayer.Ships.Count == 0)
			{
				break;
			}

			// Prepare them for next turn - deffending player now attacks
			SwapPlayers(ref attackingPlayer, ref defendingPlayer);
		}

		// Return winning player
		return attackingPlayer;
	}

	private void SwapPlayers(ref IPlayer attackingPlayer, ref IPlayer defendingPlayer)
	{
		IPlayer temp = attackingPlayer;
		attackingPlayer = defendingPlayer;
		defendingPlayer = temp;
	}

	// Due to assigment's rules the arbiter will place ships for each player at random
	private void PlaceShipsForPlayer(IPlayer player, BoardSize boardSize, int[] shipLengths)
	{
		Random random = new Random();
		List<Ship> placedShips = new List<Ship>();
		List<BoardCell> placedCells = new List<BoardCell>();

		for (int i = 0; i < shipLengths.Length; i++)
		{
			while (true)
			{
				// Chose a random point and direction inside the board
				BoardCell anchorLocation = anchorLocation = new BoardCell(random.Next(boardSize.Width), random.Next(boardSize.Height));
				ShipDirection randomShipDirection = (ShipDirection)random.Next(2);
				// Check if every cell of the ship is inside the board
				if (IsShipInbounds(boardSize, anchorLocation, randomShipDirection, shipLengths[i]))
				{
					// Create and generate new ship
					Ship ship = new Ship(anchorLocation, randomShipDirection, shipLengths[i]);
					// Update ship lists if none of the cells overlap
					if (!IsShipOverlapping(ship.ShipCells, placedCells))
					{
						placedCells.AddRange(ship.ShipCells);
						placedShips.Add(ship);
						break;
					}
				}
			}
		}

		player.Ships.AddRange(placedShips);
	}

	private bool IsCellInbounds(BoardSize boardSize, BoardCell boardCell)
	{
		if (boardCell.X < 0 || boardCell.Y < 0 || boardCell.X >= boardSize.Width || boardCell.Y >= boardSize.Height)
		{
			return false;
		}

		return true;
	}

	private bool IsShipInbounds(BoardSize boardSize, BoardCell anchorCell, ShipDirection shipDirection, int shipLength)
	{
		if (shipDirection == ShipDirection.Horizontal)
		{
			return IsCellInbounds(boardSize, anchorCell) && IsCellInbounds(boardSize, new BoardCell(anchorCell.X + shipLength, anchorCell.Y));
		}
		else
		{
			return IsCellInbounds(boardSize, anchorCell) && IsCellInbounds(boardSize, new BoardCell(anchorCell.X, anchorCell.Y + shipLength));
		}
	}

	private bool IsShipOverlapping(List<BoardCell> shipCells, List<BoardCell> placedCells)
	{
		foreach (BoardCell shipCell in shipCells)
		{
			if (placedCells.Contains(shipCell))
			{
				return true;
			}
		}

		return false;
	}
}