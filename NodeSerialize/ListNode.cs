using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeSerialize
{
  class ListNode : IEquatable<ListNode>
  {
    public ListNode Previous;
    public ListNode Next;
    public ListNode Random;
    public string Data;

    public ListNode() { }
    public ListNode(string Data)
    {
      this.Data = Data;
    }

    public bool Equals(ListNode other)
    {
      if (this == other) return true;
      if (other == null) return false;
      if (this.Data != other.Data) return false;
      if (this.Previous != null && other.Previous != null)
        if (this.Previous.Data != other.Previous.Data) return false;
      if (this.Next != null && other.Next != null)
        if (this.Next.Data != other.Next.Data) return false;
      if (this.Random != null && other.Random != null)
        if (this.Random.Data != other.Random.Data) return false;

      return true;
    }
  }
}
