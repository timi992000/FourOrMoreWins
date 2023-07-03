using System.Windows.Media;

namespace FourOrMoreWins.Core.Entities
{
  public class Player
  {
		public string PlayerName { get; set; }
    public Brush PlayerBrush { get; set; }
		public bool IsComputer { get; set; }
	}
}
