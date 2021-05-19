using UnityEngine;

namespace GameScripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        public float CamMoveSpeed;

        private Vector3 _cam;

        public Vector3 diceCamera;
        public Vector3 boardCamera;
        public Transform diceTarget;
        public Transform boardTarget;
        private Transform target;
 
        private void Start()
        {
            _cam = transform.position;
            diceCamera = new Vector3(-18, 0, -11);
            boardCamera = _cam;

            GameController.Instance.AddOnStateChanged(OnGameChangeState);
        }

        void OnGameChangeState()
        {
            print("State of the game changed");
            if (GameController.Instance.currentGameState == GameState.PlayerRoleDice || GameController.Instance.currentGameState == GameState.EnemyRollingDice)
            {
                target = diceTarget;
                _cam = diceCamera;
            }
            else
            {
                if (GameController.Instance.previousGameState == GameState.PlayerRoleDice)
                {
                
                }
                target = boardTarget;
                _cam = boardCamera;
            }
        
        }
 
        private void FixedUpdate()
        {

            transform.position = Vector3.Lerp(transform.position, _cam, CamMoveSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
