namespace GenericSample
{
	internal class Program {
		static void Main(string[] args)
		{
			int a = 3;
			int b = 5;
			Swap (ref a, ref b);

			Console.WriteLine ($"{a} {b}");

			Vector3 pos1 = new Vector3 { X = 4f, Y = 2f, Z = 3.5f };
			Vector3 pos2 = new Vector3 { X = 2f, Y = 6f, Z = 4f };
			Swap<Vector3>(ref pos1, ref pos2); // �Ķ���Ϳ� Vector3 Ÿ���� �־��� ������ �����Ϸ��� ���׸� ���Ŀ� Vector3 Ÿ���� ����Ѵٴ� ���� �˼� �ִ�

			Box<int> boxOfInt = new Box<int>();
			boxOfInt.Item = 4;

			Box<Vector3> boxOfVector3 = new Box<Vector3>();
			boxOfVector3.Item = pos1;

			Box<int, string> boxOfIntAndString = new Box<int, string>();
			boxOfIntAndString.PrintItems();

			boxOfIntAndString.Dummy<int[], Vector3>();

			object obj1 = 1;
			object obj2 = 2;
		}

		static void Swap(ref int a, ref int b)
		{
			int temp = a;
			a= b;
			b= temp;
		}

		static void Swap(ref float a, ref float b)
		{
			float temp = a;
			a= b;
			b= temp;
		}

		static void Swap(ref double a, ref double b)
		{
			double temp = a;
			a= b;
			b= temp;
		}

		static void Swap<T>(ref T a, ref T b)
		{
			T temp = a;
			a= b;
			a= temp;
		}
	}

	internal struct Vector3
	{
		public float X, Y, Z;

		public override string ToString()
		{
			return $"X:{X}, Y:{Y}, Z:{Z}";
		}
	}
}