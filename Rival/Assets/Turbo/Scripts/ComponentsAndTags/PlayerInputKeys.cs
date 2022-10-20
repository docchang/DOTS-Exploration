using Unity.Entities;
using UnityEngine;

namespace TMG.SpaceShooter
{
    [GenerateAuthoringComponent]
    public struct PlayerInputKeys : IComponentData
    {
        public KeyCode UpKey;
        public KeyCode DownKey;
        public KeyCode LeftKey; 
        public KeyCode RightKey;
        public KeyCode PrimaryWeaponKey;
    }
}