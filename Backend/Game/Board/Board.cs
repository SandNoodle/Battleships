public class Board
{
	private BoardSize _boardSize;
	private CellStatus[,] _boardCells;

	public BoardSize BoardSize { get => this._boardSize; }
	public CellStatus[,] BoardCells { get => this._boardCells; set => this._boardCells = value; }

	public Board(BoardSize boardSize)
	{
		this._boardSize = boardSize;
		this._boardCells = new CellStatus[boardSize.Width, boardSize.Height];
	}
}