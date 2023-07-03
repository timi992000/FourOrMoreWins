using FourOrMoreWins.Core.BaseClasses;
using FourOrMoreWins.Core.Entities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FourOrMoreWins.Client.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly MetroWindow _MetroWindow;
		private Player[] _Players;
		private Queue<Player> _TurnQueue;
		private List<GameCell> _GameCells;
		public MainWindowViewModel(MainWindow mainWindow)
		{
			_MetroWindow = mainWindow;
			NeedsToWinCount = 4;
			RowCount = 6;
			ColumnCount = 8;
			SelectedBackgroundColor = Brushes.Blue.Color;
			SelectedPlayer1Color = Brushes.Yellow.Color;
			SelectedPlayer2Color = Brushes.Red.Color;
			WinColor = Brushes.Green.Color;
			EmptyEllipseColor = Brushes.White.Color;
		}

		public Player CurrentPlayer
		{
			get => Get<Player>();
			set => Set(value);
		}

		public int NeedsToWinCount
		{
			get => Get<int>();
			set => Set(value);
		}

		public int RowCount
		{
			get => Get<int>();
			set => Set(value);
		}

		public int ColumnCount
		{
			get => Get<int>();
			set => Set(value);
		}

		public Color? SelectedBackgroundColor
		{
			get => Get<Color?>();
			set
			{
				Set(value);
				OnPropertyChanged(nameof(BackgroundBrush));
			}
		}

		public Brush BackgroundBrush => new SolidColorBrush(SelectedBackgroundColor.Value);

		public Color? SelectedPlayer1Color
		{
			get => Get<Color?>();
			set => Set(value);
		}

		public Color? SelectedPlayer2Color
		{
			get => Get<Color?>();
			set => Set(value);
		}

		public Color? WinColor
		{
			get => Get<Color?>();
			set => Set(value);
		}

		public Color? EmptyEllipseColor
		{
			get => Get<Color?>();
			set => Set(value);
		}

		public object GameField
		{
			get => Get<object>();
			set => Set(value);
		}

		public bool IsRunningGame
		{
			get => Get<bool>();
			set
			{
				Set(value);
				OnPropertyChanged(nameof(LeftText));
				OnPropertyChanged(nameof(RightText));
			}
		}

		public string LeftText => IsRunningGame ? "ITS" : "Player";

		public string RightText => IsRunningGame ? "TURN" : "Won";

		public void DrawGameField()
		{
			try
			{
				__DoDraw();
				__Enqueue();
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex);
			}
		}

		private void __Enqueue()
		{
			_TurnQueue = new Queue<Player>();
			_Players.ToList().ForEach(x => _TurnQueue.Enqueue(x));
			__NextPlayer();
		}

		private void __NextPlayer()
		{
			CurrentPlayer = _TurnQueue.Dequeue();
			_TurnQueue.Enqueue(CurrentPlayer);
			OnPropertyChanged(nameof(Player.PlayerBrush));
		}

		private void __DoDraw()
		{
			_Players = new Player[]
			{
				new Player{ PlayerBrush =  new SolidColorBrush(SelectedPlayer1Color.Value), PlayerName = "Player 1"},
				new Player{ PlayerBrush = new SolidColorBrush(SelectedPlayer2Color.Value), PlayerName = "Player 2"},
			};
			_GameCells = new List<GameCell>();
			int size = 50;
			int margin = 10;

			var grid = new Grid
			{
				Background = new SolidColorBrush(SelectedBackgroundColor.Value)
			};

			for (int rowCounter = 0; rowCounter < RowCount; rowCounter++)
			{
				grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
				for (int columnCounter = 0; columnCounter < ColumnCount; columnCounter++)
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
					GameCell cell = __GetNewGameCell(size, margin, rowCounter, columnCounter);
					cell.SetBackground(new SolidColorBrush(EmptyEllipseColor.Value));
					Grid.SetColumn(cell.Element, columnCounter);
					Grid.SetRow(cell.Element, rowCounter);
					grid.Children.Add(cell.Element);
				}
			}
			GameField = grid;
			IsRunningGame = false;
			IsRunningGame = true;
		}

		private GameCell __GetNewGameCell(int size, int margin, int rowCounter, int columnCounter)
		{
			var cell = new GameCell(rowCounter, columnCounter, size, margin);
			cell.Clicked += __CellClicked;
			_GameCells.Add(cell);
			return cell;
		}

		private void __CellClicked(object? sender, EventArgs e)
		{
			if (sender is GameCell cell && !cell.Locked)
			{
				cell = __GetBottomCellToSet(cell);
				if (cell == null)
					return;
				cell.SetPlayer(CurrentPlayer);
				if (__CheckAndHandleWin(cell))
					return;
				__NextPlayer();
			}
		}

		private bool __CheckAndHandleWin(GameCell cell)
		{
			bool win = false;
			//Check simple row
			win = __CheckRowWin(cell);

			//Check simple column
			if (!win)
				win = __CheckColumnWin(cell);

			//Check X diagonal
			if (!win)
				win = __CheckDiagonalWin(cell);

			if (win)
				__DoWinAction();

			if (_GameCells.All(c => c.Locked))
			{
				win = true;
				__DoDrawAction();
			}

			if (win)
			{
				IsRunningGame = false;
				_GameCells.Where(c => c.WinnerCell).ToList().ForEach(wc => wc.SetBackground(new SolidColorBrush(WinColor.Value)));
			}

			return win;
		}

		private void __DoDrawAction()
		{
			ShowMessage($"Game ended, no player has won", _MetroWindow);
		}

		private void __DoWinAction()
		{
			var dlgSettings = new MetroDialogSettings();
			dlgSettings.DialogMessageFontSize = 30;
			ShowMessage($"{CurrentPlayer.PlayerName} won", _MetroWindow, dlgSettings);
		}

		private bool __CheckRowWin(GameCell cell)
		{
			var rowCells = _GameCells.Where(c => c.Row == cell.Row && c.Player != null && c.Player == cell.Player);

			//if the matching player cells in row x is smaller than the count to win, the current player cant have a win in this row
			if (rowCells.Count() < NeedsToWinCount)
				return false;

			foreach (var rowCell in rowCells)
			{
				bool isContinuous = true;
				for (int i = 0; i < NeedsToWinCount; i++)
				{
					var columnCell = rowCells.FirstOrDefault(c => c.Column == rowCell.Column + i);
					if (columnCell == null)
					{
						isContinuous = false;
						break;
					}
					columnCell.WinnerCell = true;
				}
				if (isContinuous)
					return true;
				_GameCells.ForEach(c => c.WinnerCell = false);
			}
			return false;
		}

		private bool __CheckColumnWin(GameCell cell)
		{
			var columnCells = _GameCells.Where(c => c.Column == cell.Column && c.Player != null && c.Player == cell.Player);

			//if the matching player cells in column x is smaller than the count to win, the current player cant have a win in this column
			if (columnCells.Count() < NeedsToWinCount)
				return false;

			foreach (var ColumnCell in columnCells)
			{
				bool isContinuous = true;
				for (int i = 0; i < NeedsToWinCount; i++)
				{
					var rowCell = columnCells.FirstOrDefault(c => c.Row == ColumnCell.Row + i);
					if (rowCell == null)
					{
						isContinuous = false;
						break;
					}
					rowCell.WinnerCell = true;
				}
				if (isContinuous)
					return true;
				_GameCells.ForEach(c => c.WinnerCell = false);
			}
			return false;
		}

		private bool __CheckDiagonalWin(GameCell cell)
		{
			var slashWin = __CheckDiagonalSlash(cell);
			if (slashWin)
				return true;
			var backSlashWin = __CheckDiagonalBackSlash(cell);
			if (backSlashWin)
				return true;
			return false;
		}

		private bool __CheckDiagonalSlash(GameCell cell)
		{
			GameCell currentCellToCheck = cell;
			//Go down and left to first cell in the diagonal
			while (currentCellToCheck != null)
			{
				var foundCell = _GameCells.FirstOrDefault(c => c.Row == currentCellToCheck.Row + 1 && c.Column == currentCellToCheck.Column - 1);
				if (foundCell != null)
					currentCellToCheck = foundCell;
				else
					break;
			}

			//Go up and right and check for continuous
			while (currentCellToCheck != null)
			{
				bool isContinuous = true;
				for (int i = 0; i < NeedsToWinCount; i++)
				{
					var matchingCell = _GameCells.FirstOrDefault(c => c.Row == currentCellToCheck.Row - i && c.Column == currentCellToCheck.Column + i && c.Player == cell.Player && c.Locked);
					if (matchingCell == null)
						isContinuous = false;
					else
						matchingCell.WinnerCell = true;
				}
				if (isContinuous)
					return true;
				_GameCells.ForEach(c => c.WinnerCell = false);
				var foundCell = _GameCells.FirstOrDefault(c => c.Row == currentCellToCheck.Row - 1 && c.Column == currentCellToCheck.Column + 1);
				if (foundCell != null)
					currentCellToCheck = foundCell;
				else
					break;
			}

			return false;
		}

		private bool __CheckDiagonalBackSlash(GameCell cell)
		{
			GameCell currentCellToCheck = cell;
			//Go up and left to first cell in the diagonal
			while (currentCellToCheck != null)
			{
				var foundCell = _GameCells.FirstOrDefault(c => c.Row == currentCellToCheck.Row - 1 && c.Column == currentCellToCheck.Column - 1);
				if (foundCell != null)
					currentCellToCheck = foundCell;
				else
					break;
			}

			//Go up and right and check for continuous
			while (currentCellToCheck != null)
			{
				bool isContinuous = true;
				for (int i = 0; i < NeedsToWinCount; i++)
				{
					var matchingCell = _GameCells.FirstOrDefault(c => c.Row == currentCellToCheck.Row + i && c.Column == currentCellToCheck.Column + i && c.Player == cell.Player && c.Locked);
					if (matchingCell == null)
						isContinuous = false;
					else
						matchingCell.WinnerCell = true;
				}
				if (isContinuous)
					return true;
				_GameCells.ForEach(c => c.WinnerCell = false);
				var foundCell = _GameCells.FirstOrDefault(c => c.Row == currentCellToCheck.Row + 1 && c.Column == currentCellToCheck.Column + 1);
				if (foundCell != null)
					currentCellToCheck = foundCell;
				else
					break;
			}

			return false;
		}

		private GameCell __GetBottomCellToSet(GameCell cell)
		{
			//Get all gamecells from clicked column which are not locked
			var gameCellsFromColumn = _GameCells.Where(c => c.Column == cell.Column && !c.Locked).ToList();

			//All gamecells have player assignment - do nothing on click
			if (!gameCellsFromColumn.Any())
				return null;

			return gameCellsFromColumn.OrderByDescending(c => c.Row).FirstOrDefault();
		}

		private GameCell __GetCell(int Row, int Column)
		{
			return _GameCells.FirstOrDefault(c => c.Row == Row && c.Column == Column);
		}
	}
}