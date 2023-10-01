using lab_2.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_2
{
	internal class Ceil : Control
	{
		private Form1 mainFrom;
		public Button Button { get; set; }
		public EnumStateCeil State { get; set; }

		public Ceil()
		{
			this.State = EnumStateCeil.None;
			InitButton();
		}
		public Ceil(int row, int col, int rows, int columns)
		{
			this.State = EnumStateCeil.None;
			InitButton(row, col, rows, columns);
		}

		public void InitButton()
		{
			this.Button = new Button()
			{
				Width = 36,
				Height = 36,
				Margin = new Padding(0),
				Name = $"button",
			};
		}
		public void InitButton(int rowItem, int colItem, int rows, int columns)
		{
			this.Button = new Button()
			{
				Top = 150 + 36 * rowItem,
				Left = 103 + 36 * colItem,

				Width = 36,
				Height = 36,

				Margin = new Padding(0),

				Name = $"button{rowItem * columns + colItem + 1}",
				TabStop = false,
				Visible = false,

				BackgroundImageLayout = ImageLayout.Stretch,
				//BackgroundImage = Properties.Resources._0,
			};

		}
		public void ChangeBgImage()
		{
			switch (this.State)
			{
				case EnumStateCeil.None:
					this.Button.BackgroundImage = Properties.Resources._0;
					break;
				case EnumStateCeil.One:
					this.Button.BackgroundImage = Properties.Resources._1;
					break;
				case EnumStateCeil.Two:
					this.Button.BackgroundImage = Properties.Resources._2;
					break;
				case EnumStateCeil.Three:
					this.Button.BackgroundImage = Properties.Resources._3;
					break;
				case EnumStateCeil.Four:
					this.Button.BackgroundImage = Properties.Resources._4;
					break;
				case EnumStateCeil.Five:
					this.Button.BackgroundImage = Properties.Resources._5;
					break;
				case EnumStateCeil.Six:
					this.Button.BackgroundImage = Properties.Resources._6;
					break;
				case EnumStateCeil.Seven:
					this.Button.BackgroundImage = Properties.Resources._7;
					break;
				case EnumStateCeil.Eight:
					this.Button.BackgroundImage = Properties.Resources._8;
					break;
				case EnumStateCeil.Bomb:
					this.Button.BackgroundImage = Properties.Resources._9;
					break;
				case EnumStateCeil.Base:
					this.Button.BackgroundImage = Properties.Resources._10;
					break;
				case EnumStateCeil.Flag:
					this.Button.BackgroundImage = Properties.Resources._11;
					break;
				default:
					break;
			}
		}
		
	}
}
