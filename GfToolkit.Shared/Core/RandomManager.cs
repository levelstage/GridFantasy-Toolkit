using System;
using System.Collections.Generic;
using System.Linq;
using GfToolkit.Shared.Models.Actors; // Trait 클래스를 쓰기 위해 추가

namespace GfToolkit.Shared.Core
{
    public static class RandomManager
    {
        // 게임 전체에서 사용할 단 하나의 난수 생성기
        private static Random _rng;

        // 게임 시작 시, PlayerState의 시드로 단 한 번 초기화하는 함수
        public static void InitializeRandom(int seed)
        {
            _rng = new Random(seed);
        }

        // 게임의 모든 랜덤 숫자는 이제 이 함수를 통해 얻는다.
        public static int GetRandomInt(int min, int max)
        {
            // _rng가 초기화되지 않았다면 비상용으로 새로 생성
            if (_rng == null)
            {
                _rng = new Random();
            }
            return _rng.Next(min, max);
        }

        
    }
}