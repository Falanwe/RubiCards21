using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NavalBattle.Services
{
	public interface INavalBattleService
	{
		Task<NavalBattle> GetOrSet(Func<NavalBattle> factory);
		Task Empty();
    }
}
