public struct TournamentRules
{
	private readonly BoardSize _boardSize;
	private readonly int[] _shipLengths;
	private readonly int _roundsPerMatch;

	public BoardSize BoardSize { get => this._boardSize; }
	public int[] ShipLengths { get => this._shipLengths; }
	public int RoundsPerMatch { get => this._roundsPerMatch; }

	public TournamentRules(BoardSize boardSize, int[] shipLengths, int roundsPerMatch)
	{
		this._boardSize = boardSize;
		this._shipLengths = shipLengths;
		this._roundsPerMatch = roundsPerMatch;
	}
}