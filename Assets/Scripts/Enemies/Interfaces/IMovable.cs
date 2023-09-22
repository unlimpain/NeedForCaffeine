using UnityEngine;

namespace Enemies
{
    public interface IMovable
    {
        public Rigidbody2D Rigidbody2D { get; set; }
        bool IsFacedRight { get; set; }
        void Move();
        
    }
}