namespace PracticeOOP {
    struct Coord {
        public Coord(int x, int y) {
            X = x;
            Y = y;
        }

        public static Coord Up => new Coord(0, -1);
        public static Coord Down => new Coord(0, 1);
        public static Coord Right => new Coord(1, 0);
        public static Coord Left => new Coord(-1, 0);


        public int X, Y;

        public static bool operator ==(Coord op1, Coord op2)
            => (op1.X == op2.X) && (op1.Y == op2.Y);

        public static bool operator !=(Coord op1, Coord op2)
            => !(op1 == op2);

        public static Coord operator +(Coord op1, Coord op2)
            => new Coord(op1.X + op2.X, op1.Y + op2.Y);

        public static Coord operator -(Coord op1, Coord op2)
            => new Coord(op1.X - op2.X, op1.Y - op2.Y);
    }

    enum FloorType {
        None,
        Grass,
        Stone,
        Water
    }

    struct MapTile {
        public MapTile(Coord coord, FloorType floorType, GameObject gameObject) {
            Coord = coord;
            FloorType = floorType;
            GameObject = gameObject;
        }


        public static MapTile Invalid => new MapTile(new Coord(-1, -1), FloorType.None, null);


        public Coord Coord;
        public FloorType FloorType;
        public GameObject GameObject;
    }
}