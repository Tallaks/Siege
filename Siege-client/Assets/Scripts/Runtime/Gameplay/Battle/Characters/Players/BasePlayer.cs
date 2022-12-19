using System.Text;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Players
{
	public class BasePlayer : BaseCharacter
	{
		public override string ToString()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"Player {Name}");
			stringBuilder.AppendLine($"MaxHP {MaxHP}");
			stringBuilder.AppendLine($"MaxAP {MaxAP}");
			return stringBuilder.ToString();
		}
	}
}