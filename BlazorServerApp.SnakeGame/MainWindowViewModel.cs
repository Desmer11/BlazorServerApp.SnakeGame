using BlazorServerApp.SnakeGame.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Snake.Core.DataModels;
using Snake.Core.Models;
using Snake.Core.ViewModels;
using Snake.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace Blazor_Snake;

/// <summary>
/// The main window view model for the main window
/// </summary>
public partial class MainWindowViewModel : ObservableObject
{
	#region Fields

	private Window _window;

	private Random _random = new Random();

	private const int MAX_GAME_GRID_SIZE = 400;
	private const int MAX_GAME_GRID_ROWS = 40;
	private const int MAX_GAME_GRID_COLUMNS = 40;
	private const string HIGH_SCORES_PATH = "Resources/HighScores.json";

	private Direction _currentDirection = Direction.DOWN;
	private Queue<NextMove> _nextMoves = new Queue<NextMove>();
	private object _lock = new object();
	private int _snakeSpeed = 200;

	#endregion

	#region Properties

	/// <summary>
	/// The size of the game grid
	/// </summary>
	public int GameGridSize => MAX_GAME_GRID_SIZE;

	/// <summary>
	/// The current score
	/// </summary>
	[ObservableProperty]
	private int score = 0;

	/// <summary>
	/// Flag to let us know if the game is over
	/// </summary>
	[ObservableProperty]
	private bool gameOver = true;

	/// <summary>
	/// Flag to let us know if the main menu is visible
	/// </summary>
	[ObservableProperty]
	private bool mainMenuVisible = true;

	/// <summary>
	/// Flag to let us know if the high scores are visible
	/// </summary>
	[ObservableProperty]
	private bool highScoresVisible;

	/// <summary>
	/// Fruit grows randomly and grows the snake
	/// </summary>
	[ObservableProperty]
	private CellViewModel fruit;

	/// <summary>
	/// The snake to draw in the game grid
	/// </summary>
	public ObservableCollection<CellViewModel> Snake { get; set; } = new ObservableCollection<CellViewModel>();

	/// <summary>
	/// The high scores
	/// </summary>
	[ObservableProperty]
	public ObservableCollection<HighScoreViewModel> highScores = new ObservableCollection<HighScoreViewModel>();



	public MainWindowViewModel(Window window)
	{
		_window = window;
		_window.KeyUp += _window_KeyUp;

		BindingOperations.EnableCollectionSynchronization(Snake, _lock);
		BindingOperations.EnableCollectionSynchronization(HighScores, _lock);
	}

	#endregion

{




	#endregion



		if (newX < 0)
		{
			newX = MAX_GAME_GRID_SIZE - 10;
		}
		else if (newX > MAX_GAME_GRID_SIZE - 10)
		{
			newX = 0;
		}


		Snake.Last().XPos = newX;
		Snake.Last().YPos = newY;
	}

	/// <summary>
	/// Grows the snake
	/// </summary>
	private void GrowSnake()
	{
		var snakeSection = new CellViewModel(Snake.First().XPos, Snake.First().YPos);
		if (Snake.First().Rgb.Equals(CellViewModel.SNAKE_HEAD_RGB))
		{
			snakeSection.Rgb = CellViewModel.SNAKE_BODY1_RGB;
		}
		else if (Snake.First().Rgb.Equals(CellViewModel.SNAKE_BODY1_RGB))
		{
			snakeSection.Rgb = CellViewModel.SNAKE_BODY2_RGB;
		}
		else if (Snake.First().Rgb.Equals(CellViewModel.SNAKE_BODY2_RGB))
		{
			snakeSection.Rgb = CellViewModel.SNAKE_BODY3_RGB;
		}
		else
		{
			snakeSection.Rgb = CellViewModel.SNAKE_BODY1_RGB;
		}

		Snake.Insert(0, snakeSection);
	}

	/// <summary>
	/// Generates a new fruit at a random location
	/// </summary>
	private void SpawnFruit()
	{
		bool foundSection = false;
		int xPos = 0;
		int yPos = 0;
		do
		{
			xPos = _random.Next(0, MAX_GAME_GRID_ROWS) * 10;
			yPos = _random.Next(0, MAX_GAME_GRID_COLUMNS) * 10;
			foundSection = Snake.FirstOrDefault(item => item.XPos.Equals(xPos) && item.YPos.Equals(yPos)) != null;
		} while (foundSection);

		Fruit = new CellViewModel(xPos, yPos);
	}

	#endregion
}