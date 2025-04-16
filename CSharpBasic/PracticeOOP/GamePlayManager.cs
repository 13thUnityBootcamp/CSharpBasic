using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeOOP {
    /// <summary>
    /// 게임 시작과 로직 관리
    /// </summary>
    class GamePlayManager {
        private Map _map; // 현재 플레이하려는 레벨의 맵 
        private PC _player; // 현재 조작하려는 플레이어의 캐릭터
        private Coord _playerCoord; // <= 플레이어 좌표 저장해놓고 활용해도됨

        /// <summary>
        /// 게임 첫 시작시 호출
        /// 게임 로직 시작 전 레벨 초기화 진행함
        /// </summary>
        public void PlayGame() {
            InitializeLevel();
            GameWorkflow();
        }

        /// <summary>
        /// 맵 만들고 맵에 게임오브젝트들 배치
        /// </summary>
        private void InitializeLevel() {
            CreateMap();
            SpawnGameObjects();
        }

        private void CreateMap() {
            _map = Map.CreateDefault(20, 20); // 맵을 기본값으로 생성
        }

        private void SpawnGameObjects() {
            MapTile mapTile; // 게임 오브젝트 배치하려는 타일값을 처리하기위한 변수
            GameObject spawnedObject; // 생성한 게임 오브젝트를 처리하기위한 변수
            Coord[] coords = _map.GetShuffledEmptyCoords(); // 맵 전체에서 게임오브젝트가 존재하지않는 타일들을 랜덤하게 섞어서 받아옴
            int coordIndex = 0; // 순차적으로 빈 타일을 검색하기위한 인덱스 (iteration)

            // 이장님 소환 및 배치
            spawnedObject = new TownNPC_VillageChief("마을이장", int.MaxValue);
            mapTile = _map.GetTile(coords[coordIndex]);
            mapTile.GameObject = spawnedObject;
            _map.SetTile(mapTile);
            coordIndex++;

            // 슬라임 10마리 생성
            for (int i = 0; i < 10; i++) {
                spawnedObject = new Slime("슬라임", 50);
                mapTile = _map.GetTile(coords[coordIndex]);
                mapTile.GameObject = spawnedObject;
                _map.SetTile(mapTile);
                coordIndex++;
            }

            // 버섯 5마리 생성
            for (int i = 0; i < 5; i++) {
                spawnedObject = new Mushroom("버섯", 100, 5);
                mapTile = _map.GetTile(coords[coordIndex]);
                mapTile.GameObject = spawnedObject;
                _map.SetTile(mapTile);
                coordIndex++;
            }

            // 플레이어 캐릭터 생성 (전사)
            spawnedObject = _player = new Warrior("나는전사", 200, 20);
            mapTile = _map.GetTile(coords[coordIndex]);
            mapTile.GameObject = spawnedObject;
            _map.SetTile(mapTile);
            coordIndex++;
            _playerCoord = mapTile.Coord;
        }

        private void GameWorkflow() {
            while (IsGameOver() == false &&
                   IsGameClear() == false) {
                Console.Clear();
                _map.Display();
                HandleInput();
            }
        }

        private bool IsGameOver() {
            return _player.Hp <= 0;
        }

        private bool IsGameClear() {
            // TODO : 소환된 적이 더이상 남아있지 않은지
            return false;
        }

        private void HandleInput() {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;

            Coord direction = default;

            switch (key) {
                case ConsoleKey.UpArrow:
                    direction = Coord.Up;
                    break;
                case ConsoleKey.DownArrow:
                    direction = Coord.Down;
                    break;
                case ConsoleKey.RightArrow:
                    direction = Coord.Right;
                    break;
                case ConsoleKey.LeftArrow:
                    direction = Coord.Left;
                    break;
                default:
                    break;
            }

            Coord targetCoord = _playerCoord + direction;

            if (_map.IsValid(targetCoord) &&
                _map.IsEmpty(targetCoord)) {
                MapTile origin = _map.GetTile(_playerCoord);
                origin.GameObject = null;
                _map.SetTile(origin);

                MapTile target = _map.GetTile(targetCoord);
                target.GameObject = _player;
                _map.SetTile(target);

                _playerCoord = targetCoord;
            }
        }
    }
}