using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour
{
    public bool FacingRight { get; set; }
    public bool Walking { get; set; }
    public bool Jumping { get; set; }
    public bool DoubleJumping { get; set; }
    public bool WallSlide { get; set; }
    public bool WallJumping { get; set; }
    public bool RecoilingX { get; set; }
    public bool RecoilingY { get; set; }
}
