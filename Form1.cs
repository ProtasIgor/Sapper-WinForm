using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_2
{
	public partial class Form1 : Form
	{
		private static int row = 8;
		private static int columns = 8;

		private static int minCountBomb = 10;
		private static int maxCountBomb = 15;

		private int countBombs = 0;

		private Ceil[,] _arrayCeils;
		private List<Button> _arrayControlsForm;

		public Form1()
		{
			InitializeComponent();
			this._arrayControlsForm = new List<Button>();

			this.Text = "Сапер";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.WindowState = FormWindowState.Normal;

			this.Size = new Size(500, 500);
			this.MaximumSize = new Size(500, 500);
			this.MinimumSize = new Size(500, 500);
			this.BackColor = Color.DarkGray;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			CreateButtonStart();
		}

		private void CreateButtonStart()
		{
			CreateCeilMap();
			CreateEventCeil();
			CreateBombCeil();
			CreateNumberCeil();
			ChangeBgImage();

			Button button = new Button()
			{
				Left = 150,
				Top = 50,

				Width = 200,
				Height = 50,

				Name = "buttonStart",
				Text = "START",
				TabStop = false,
				BackColor = Color.CadetBlue,
			};

			button.Click += new System.EventHandler(Click);

			void Click(object sender, System.EventArgs e)
			{
				for (int i = 0; i < Form1.row; i++)
					for (int j = 0; j < Form1.columns; j++)
					{
						this._arrayCeils[i, j].Button.Visible = true;

						this._arrayCeils[i, j].Button.BackgroundImage = Properties.Resources._10;
					}

				foreach (var item in _arrayControlsForm)
				{
					if (item.Name == "buttonStart")
					{
						this.Controls.Remove(item);
					}
				}

				CreateButtonFinish();
			}

			_arrayControlsForm.Add(button);
			this.Controls.Add(button);
		}
		private void CreateButtonFinish()
		{
			Button button = new Button()
			{
				Left = 150,
				Top = 50,

				Width = 200,
				Height = 50,

				Name = "buttonFinish",
				Text = "QIUT",
				TabStop = false,
				BackColor = Color.CadetBlue,
			};

			button.Click += new System.EventHandler(Click);

			void Click(object sender, System.EventArgs e)
			{
				FinishGame("Игра окончена!");
			}

			_arrayControlsForm.Add(button);
			this.Controls.Add(button);
		}
		private void CreateCeilMap()
		{
			_arrayCeils = new Ceil[8, 8];

			for (int i = 0; i < row; i++)
				for (int j = 0; j < columns; j++)
					_arrayCeils[i, j] = new Ceil(i, j, row, columns);

			for (int i = 0; i < row; i++)
				for (int j = 0; j < columns; j++)
					this.Controls.Add(_arrayCeils[i, j].Button);
		}
		private void CreateBombCeil()
		{
			Random random = new Random();

			// количество бомб
            var count = random.Next(minCountBomb, maxCountBomb);

			// количество ячеек на карте, которые не являются бомбами
            countBombs = Form1.row * Form1.columns - count;

			while (count != 0)
			{
				int value1 = random.Next(0, row - 1);
				int value2 = random.Next(0, columns - 1);

				if (_arrayCeils[value1, value2].State == EnumStateCeil.Bomb) continue;

				_arrayCeils[value1, value2].State = EnumStateCeil.Bomb;

				count--;
			}
		}
		private void CreateNumberCeil()
		{
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					int count = 0;

					if (_arrayCeils[i, j].State == EnumStateCeil.Bomb) continue;

					// в углах матрицы
					if (i == 0 && j == 0 || i == 0 && j == columns - 1 ||
						i == row - 1 && j == 0 || i == row - 1 && j == columns - 1)
					{
						if (i == 0 && j == 0)
						{
							if (_arrayCeils[i, j + 1].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i + 1, j].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i + 1, j + 1].State == EnumStateCeil.Bomb) count++;
						}
						else if (i == 0 && j == columns - 1)
						{
							if (_arrayCeils[i, j - 1].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i + 1, j].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i + 1, j - 1].State == EnumStateCeil.Bomb) count++;
						}
						else if (i == row - 1 && j == 0)
						{
							if (_arrayCeils[i - 1, j].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i, j + 1].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i - 1, j + 1].State == EnumStateCeil.Bomb) count++;
						}
						else if (i == row - 1 && j == columns - 1)
						{
							if (_arrayCeils[i - 1, j - 1].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i, j - 1].State == EnumStateCeil.Bomb) count++;
							if (_arrayCeils[i - 1, j].State == EnumStateCeil.Bomb) count++;
						}
					}

					// по краям матрицы (вверх)
					else if (i == 0 && j != 0 && j != columns - 1)
					{
						if (_arrayCeils[i, j + 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j + 1].State == EnumStateCeil.Bomb) count++;
					}

					// по краям матрицы (низ)
					else if (i == row - 1 && j != 0 && j != columns - 1)
					{
						if (_arrayCeils[i, j + 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j + 1].State == EnumStateCeil.Bomb) count++;
					}

					// по краям матрицы (лево)
					else if (i != 0 && i != row - 1 && j == 0)
					{
						if (_arrayCeils[i - 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i, j + 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j + 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j + 1].State == EnumStateCeil.Bomb) count++;
					}

					// по краям матрицы (право)
					else if (i != 0 && i != row - 1 && j == columns - 1)
					{
						if (_arrayCeils[i - 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j - 1].State == EnumStateCeil.Bomb) count++;
					}

					// по центру
					else
					{
						if (_arrayCeils[i - 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i - 1, j + 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i, j + 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j - 1].State == EnumStateCeil.Bomb) count++;
						if (_arrayCeils[i + 1, j + 1].State == EnumStateCeil.Bomb) count++;
					}

					SetNumber(i, j, count);
					continue;
				}
			}

			void SetNumber(int row, int column, int value)
			{
				switch (value)
				{
					case 0:
						_arrayCeils[row, column].State = EnumStateCeil.None;
						break;
					case 1:
						_arrayCeils[row, column].State = EnumStateCeil.One;
						break;
					case 2:
						_arrayCeils[row, column].State = EnumStateCeil.Two;
						break;
					case 3:
						_arrayCeils[row, column].State = EnumStateCeil.Three;
						break;
					case 4:
						_arrayCeils[row, column].State = EnumStateCeil.Four;
						break;
					case 5:
						_arrayCeils[row, column].State = EnumStateCeil.Five;
						break;
					case 6:
						_arrayCeils[row, column].State = EnumStateCeil.Six;
						break;
					case 7:
						_arrayCeils[row, column].State = EnumStateCeil.Seven;
						break;
					case 8:
						_arrayCeils[row, column].State = EnumStateCeil.Eight;
						break;
					default:
						break;
				}
			}
		}
		private void CreateEventCeil()
		{
			for (int i = 0; i < row; i++)
				for (int j = 0; j < columns; j++)
				{
					_arrayCeils[i, j].Button.MouseDown += Button_Click;
				}
		}

		private void Button_Click(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			string value = ((Button)sender).Name;

			Ceil ceil = new Ceil();

			if (e.Button == MouseButtons.Right)
			{
				EditStateOnFlag(value);
			}
			else if (CheckFailGame(value))
			{
				Thread.Sleep(300);

				FinishGame("Игра окончена! Вы проиграли.");
			}
			else
			{
				// смена количества ячеек которые еще нужно разблокировать для победы 
				countBombs--;
				if (countBombs == 0)
				{
					FinishGame("Игра окончена! Вы победили.");
				}

				// открыть ячейку, не являющуюся бомбой, и освободить от Event 
				ceil.Button.MouseDown -= Button_Click;
				ceil.Button.Enabled = false;
				ceil.ChangeBgImage();
			}


			bool CheckFailGame(string name)
			{
				for (int i = 0; i < row; i++)
					for (int j = 0; j < columns; j++)
						if (_arrayCeils[i, j].Button.Name == name)
						{
							ceil = _arrayCeils[i, j];
							if (_arrayCeils[i, j].State == EnumStateCeil.Bomb)
							{
								_arrayCeils[i, j].Button.Enabled = false;
								_arrayCeils[i, j].ChangeBgImage();

								return true;
							}
						}

				return false;
			}

			void EditStateOnFlag(string name)
			{
				for (int i = 0; i < row; i++)
				{
					for (int j = 0; j < columns; j++)
					{
						if (_arrayCeils[i, j].Button.Name == name)
						{
							if (_arrayCeils[i, j].Button.BackgroundImage == Properties.Resources._10)
								_arrayCeils[i, j].Button.BackgroundImage = Properties.Resources._11;
							else
								_arrayCeils[i, j].Button.BackgroundImage = Properties.Resources._10;
							return;
						}
					}
				}
			}
		}

		private void ChangeBgImage()
		{
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					_arrayCeils[i, j].ChangeBgImage();
				}
			}
		}
		private void FinishGame(string value)
		{
			MessageBox.Show(value, "The End");

			this.Controls.Clear();
			this._arrayControlsForm.Clear();

			CreateButtonStart();
		}
	}
}
