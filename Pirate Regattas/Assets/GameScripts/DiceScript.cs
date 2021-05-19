using UnityEngine;

namespace GameScripts
{
    public class DiceScript : MonoBehaviour
    {
        private static Rigidbody rb;
        public GameObject rollDiceButton;

        public static Vector3 diceVelocity;
        public static Vector3 startingposition;

        public static bool roolingDice;
    
    
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            startingposition = transform.position;
            roolingDice = false;

        }

        // Update is called once per frame
        void Update()
        {
            diceVelocity = rb.velocity;
        }

        public void RollTheDice()
        {
            rollDiceButton.SetActive(false);
            roolingDice = true;
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = startingposition;
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
    }
}
