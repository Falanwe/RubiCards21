using System;
using System.Threading.Tasks;

namespace NavalBattle.Services
{
	internal class NavalBattleCacheService : INavalBattleService
    {
        private NavalBattle _cachedValue;
        private readonly object _syncRoot = new object();
        public Task<NavalBattle> GetOrSet(Func<NavalBattle> factory)
        {
            NavalBattle cachedValue = _cachedValue;
            if (cachedValue == null)
            {
                lock (_syncRoot)
                {
                    if (_cachedValue == null)
                    {
                        _cachedValue = factory();
                    }
                    cachedValue = _cachedValue;
                }
            }

            return Task.FromResult(cachedValue);
        }

        public Task Empty()
        {
            lock (_syncRoot)
            {
                _cachedValue = null;
            }
            return Task.CompletedTask;
        }
    }
}
