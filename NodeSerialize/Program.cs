using System;
using System.IO;

namespace NodeSerialize
{
	class Program
	{
		static void Main(string[] args)
		{
      ListRandom testList = new ListRandom();
      Random r = new Random();

      for (int i = 0; i < 700; i++)
        testList.Add(Convert.ToString(r.Next(0, 103)));

      testList.Serialize(new FileStream("Test.txt", FileMode.Create));
      testList.Deserialize(new FileStream("Test.txt", FileMode.Open));

      ListRandom newTestList = testList.DeserializeInList(new FileStream("Test.txt", FileMode.Open));

      bool f = testList.Equals(newTestList);

      Console.WriteLine(f);
      Console.ReadKey();
    }
	}
}
