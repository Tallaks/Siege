using System.Text;

namespace Kulinaria.Siege.Runtime.Gameplay.Battle.Characters.Enemies
{
	public class BaseEnemy : BaseCharacter
	{
		public override string ToString()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"Enemy {Name}");
			stringBuilder.AppendLine($"MaxHP {MaxHP}");
			stringBuilder.AppendLine($"MaxAP {MaxAP}");
			return stringBuilder.ToString();
		}
	}
}