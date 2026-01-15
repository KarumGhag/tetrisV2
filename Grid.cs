using Raylib_cs;

namespace GridClass;

public class GridPiece
{
    public Color colour;
}
public class Grid
{
    int numRows;
    int numCols;
    public List<List<GridPiece>> grid = new List<List<GridPiece>>();

    public Grid(int rows, int cols)
    {
        this.numRows = rows;
        this.numCols = cols;

        grid = new List<List<GridPiece>>();
    }

    public void MakeGrid()
    {
    }

}