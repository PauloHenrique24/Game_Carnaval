using UnityEngine;

namespace Entities{
    [System.Serializable]
    class Movimento{
        public float Speed { get; set; }

        public Movimento(){}

        public Movimento(float speed){
            Speed = speed;
        }

        public Vector3 Movimentar(){
            float x = Input.GetAxisRaw("Horizontal");
            Vector3 mov = new(x,0);

            return mov * Speed * Time.deltaTime;
        }
    }
}