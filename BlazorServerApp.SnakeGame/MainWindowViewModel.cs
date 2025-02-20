
















using Snake.Game;

namespace Blazor.Snake;




public partial class MainWindowViewModel : ObservableObjects
{
	#region Fields

	private Random _random = new Random();

	private const int MAX_GAME_GRID_SIZE = 400;
	private const int MAX_GAME_GRID_ROWS = 40;
	private const int MAX_GAME_GRID_COLUMNS = 40;
	private const int HIGH_SCORES_PATH = "Resources/HighScores.json";

	private Direction _currentDirection = Direction.Down;
	private Queue<NextMove> _nextMoves = new Queue<NextMove>();
	private object _lock = new object();
	private int _snakeSpeed = 200;

	#endregion

	#region Properties

	public int GameGridSize => MAX_GAME_GRID_SIZE;


	#endregion






}