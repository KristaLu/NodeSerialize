using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeSerialize
{
  class ListRandom : IEquatable<ListRandom>
  {
    public ListNode Head;
    public ListNode Tail;
    public int Count;

    public void Add(string Data)
    {
      ListNode Node = new ListNode(Data);

      if (Head == null)
        Head = Node;
      else
      {
        Tail.Next = Node;
        Node.Previous = Tail;
      }

      Tail = Node;

      Count++;

      if (Count > 1)
      {
        Random r = new Random();
        int ri = r.Next(1, Count - 1);
        int i = 1;
        ListNode tmp = Head;

        while (tmp != null && i != ri)
        {
          tmp = tmp.Next;
          i++;
        }

        Node.Random = tmp.Previous;
      }
      else
        Node.Random = null;
    }

    public void Add(string Data, string Random, string RandomPrevious, string RandomNext)
    {
      ListNode Node = new ListNode(Data);

      if (Head == null)
        Head = Node;
      else
      {
        Tail.Next = Node;
        Node.Previous = Tail;
      }

      Tail = Node;

      Count++;

      ListNode tmp = Head;

      while (tmp != null)
      {
        if (tmp.Data == Random && tmp.Previous.Data == RandomPrevious && tmp.Next.Data == RandomNext)
          Node.Random = tmp;
        else
          Node.Random = null;

        tmp = tmp.Next;
      }
    }

    public void Serialize(Stream s)
    {
      ListNode Current = Head;

      using (StreamWriter sw = new StreamWriter(s))
      {
        while (Current != null)
        {
          string NodeLine = "Node:" + Current.Data;

          if (Current.Random != null)
          {
            NodeLine += "Random:" + Current.Random.Data;

            if (Current.Random.Previous != null)
              NodeLine += "RandomPrevious:" + Current.Random.Previous.Data;
            else
              NodeLine += "RandomPrevious:NULL";

            if (Current.Random.Next != null)
              NodeLine += "RandomNext:" + Current.Random.Next.Data;
            else
              NodeLine += "RandomNext:NULL";
          }
          else
          {
            NodeLine += "Random:NULL";
            NodeLine += "RandomPrevious:NULL";
            NodeLine += "RandomNext:NULL";
          }

          sw.WriteLine(NodeLine);
          Current = Current.Next;
        }
      }
    }

    public void Deserialize(Stream s)
    {
      ListRandom newTestList = new ListRandom();

      using (StreamReader sr = new StreamReader(s))
      {
        String line = "";
        while ((line = sr.ReadLine()) != null)
        {
          string Node = line.Substring(line.IndexOf("Node:") + 5, line.IndexOf("Random:") - 5);
          string Random = line.Substring(line.IndexOf("Random:") + 7, line.IndexOf("RandomPrevious:") - (line.IndexOf("Random:") + 6));
          string RandomPrevious = line.Substring(line.IndexOf("RandomPrevious:") + 15, line.IndexOf("RandomNext:") - (line.IndexOf("RandomPrevious:") + 15));
          string RandomNext = line.Substring(line.IndexOf("RandomNext:") + 11);

          newTestList.Add(Node, Random, RandomPrevious, RandomNext);
        }
      }

      Console.WriteLine(this.Equals(newTestList));
    }

    public ListRandom DeserializeInList(Stream s)
    {
      ListRandom newTestList = new ListRandom();

      using (StreamReader sr = new StreamReader(s))
      {
        String line = "";
        while ((line = sr.ReadLine()) != null)
        {
          string Node = line.Substring(line.IndexOf("Node:") + 5, line.IndexOf("Random:") - 5);
          string Random = line.Substring(line.IndexOf("Random:") + 7, line.IndexOf("RandomPrevious:") - (line.IndexOf("Random:") + 6));
          string RandomPrevious = line.Substring(line.IndexOf("RandomPrevious:") + 15, line.IndexOf("RandomNext:") - (line.IndexOf("RandomPrevious:") + 15));
          string RandomNext = line.Substring(line.IndexOf("RandomNext:") + 11);

          newTestList.Add(Node, Random, RandomPrevious, RandomNext);
        }
      }

      return newTestList;
    }

    public bool Equals(ListRandom other)
    {
      if (this == other) return true;
      if (other == null) return false;
      if (this.Count != other.Count) return false;
      if (!this.Head.Equals(other.Head)) return false;
      if (!this.Tail.Equals(other.Tail)) return false;

      ListNode thisCurrent = this.Head;
      ListNode otherCurrent = other.Head;

      while (thisCurrent != null)
      {
        if (thisCurrent.Equals(otherCurrent))
        {
          thisCurrent = thisCurrent.Next;
          otherCurrent = otherCurrent.Next;
        }
        else
          return false;
      }

      return true;
    }

    public void Print()
    {
      if (Head == null)
        return;

      ListNode Current = Head;

      Console.WriteLine(Count);

      while (Current != null)
      {
        if (Current.Previous == null)
          Console.WriteLine("Previous NULL");
        else
          Console.WriteLine("Previous " + Current.Previous.Data);

        if (Current.Random == null)
          Console.WriteLine("Random NULL");
        else
          Console.WriteLine("Random " + Current.Random.Data);

        Console.WriteLine("Current " + Current.Data);

        if (Current.Next == null)
          Console.WriteLine("Next NULL");
        else
          Console.WriteLine("Next " + Current.Next.Data);

        Console.WriteLine("_____________________________________________");
        Current = Current.Next;
      }
    }
  }
}
