
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HCC.GameObjects.Player
{  
  /* The PlayerMovement class is responsible for moving and animating the player character in a game
  using input from the Unity input system. */
    public class PlayerMovment : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _Speed;
        private Vector2 _PlayerMovmentInput;
        #endregion
        #region Properties
        private Animator m_Animator { get => GetComponent<Animator>();}
        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PlayerMove();
        }

        private void Animation() 
        { 
            bool moving = _PlayerMovmentInput != Vector2.zero;
            m_Animator.SetBool("IsMoving", moving);
        }

        // Get Input Value
        private void OnMovment(InputValue inputValue)
        {
            _PlayerMovmentInput = inputValue.Get<Vector2>();
        }

        // Move and Rotate Player using input system. Using Transform component
        private void PlayerMove() 
        {
            
            if(math.lengthsq(_PlayerMovmentInput.x) > float.Epsilon) 
            {
                transform.localRotation = 0f > _PlayerMovmentInput.x ? quaternion.RotateY(-3.14f) : quaternion.RotateY(0f);
            }
            Vector3 pos = _PlayerMovmentInput * Time.deltaTime * _Speed;
            transform.position += pos;
            Animation();
        }
    }

}
