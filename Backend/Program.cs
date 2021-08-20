using System;
using System.Collections.Generic;

namespace Battleships
{
	class Program
	{
		static void Main(string[] args)
		{
			Game gameManager = new Game();

			// Tournament rules
			BoardSize defaultBoardSize = new BoardSize(10, 10);
			int[] defaultShipLengths = new int[] { 5, 4, 3, 3, 2 };
			int roundsPerMatch = 1000;
			TournamentRules tournamentRules = new TournamentRules(defaultBoardSize, defaultShipLengths, roundsPerMatch);

			// Tournament players
			List<IPlayer> tournamentPlayers = new List<IPlayer>();
			tournamentPlayers.Add(new RandomPlayer());
			tournamentPlayers.Add(new RandomWithoutRepetitionPlayer());
			tournamentPlayers.Add(new SimplePlayer());

			// Write program information
			Console.WriteLine("Battleship!");
			Console.WriteLine();

			gameManager.PlayTournament(tournamentPlayers, tournamentRules);
		}
	}
}
