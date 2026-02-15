using System.Collections.Generic;
using UnityEngine;

public class ParryController
{
    private Queue<ParryInfo> queue = new Queue<ParryInfo>(30);

    public void StackParry()
    {
        queue.Enqueue(new ParryInfo(Time.time));
    }

    public bool CanParry()
    {
        return 0 < queue.Count;
    }

    public void Parry()
    {
        queue.Dequeue();
    }

    public void RemoveTooEarlyParries()
    {
        while (0 < queue.Count)
        {
            ParryInfo info = queue.Peek();
            if (info.time + 0.5f < Time.time)
            {
                queue.Dequeue();
            }
            else
            {
                break;
            }
        }
    }
}