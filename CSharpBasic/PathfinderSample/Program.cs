namespace PathfinderSample {
    internal class Program {
        static void Main(string[] args) {
            Map map = SpawndMap();
            map.Display();
        }

        static Map SpawndMap() {
            Map map = Map.CreateDefault(30, 30);
            Coord[] shuffledEmptyCoords = map.GetShuffledEmptyCoords();
            int i = 0;

            // 갈수없는 길 생성(물)
            while (i < 100) {
                map.SetTile(new MapTile(shuffledEmptyCoords[i], FloorType.Water));
            }

            // 갈수없는 길 생성(돌)
            while (i < 150) {
                map.SetTile(new MapTile(shuffledEmptyCoords[i], FloorType.Stone));
            }

            // 플레이어 생성
            map.SetTile(new MapTile(shuffledEmptyCoords[i], FloorType.Grass, '㉾'));
            i++;

            map.SetTile(new MapTile(shuffledEmptyCoords[i], FloorType.Grass, '★'));
            i++;

            return map;
        }
    }
}
