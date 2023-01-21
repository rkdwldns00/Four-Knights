using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Mover
{
    public void Walk(Vector2 vector);
    public void Run(Vector2 vector);
}
