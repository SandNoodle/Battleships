public struct BoardCell
{
	private readonly int _x;
	private readonly int _y;

	public BoardCell(int x, int y)
	{
		this._x = x;
		this._y = y;
	}

	public int X { get => this._x; }
	public int Y { get => this._y; }

	public override string ToString()
	{
		return "(" + this._x + ", " + this._y + ")";
	}

	public override bool Equals(object obj)
	{
		if (!(obj is BoardCell))
		{
			return false;
		}

		BoardCell cell = (BoardCell)obj;
		return (cell._x == this._x) && (cell._y == this._y);
	}

	public override int GetHashCode() => base.GetHashCode();
}