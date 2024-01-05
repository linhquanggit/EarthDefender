using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthDenfender
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController instance;
        public static PlayerController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<PlayerController>();
                return instance;
            }
        }
        public Action<int, int> onHpChanged;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int playerHp;
        private int playerCurrentHp;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireCoolDown;
        [SerializeField] private bool usedInputSystem;
        private float tempCoolDown;
        private bool isDestroyed = false;
        private PlayerInput playerInput;
        private Vector2 movementInputValue;
        private bool attackInputValue;

        private void OnEnable()
        {
            if (playerInput == null)
            {
                playerInput = new PlayerInput();
                playerInput.Player.Movement.started += OnMovement;
                playerInput.Player.Movement.performed += OnMovement;
                playerInput.Player.Movement.canceled += OnMovement;

                playerInput.Player.Attack.started += OnAttack;
                playerInput.Player.Attack.performed += OnAttack;
                playerInput.Player.Attack.canceled += OnAttack;
                playerInput.Enable();
            }
        }
        private void OnDisable()
        {
            playerInput.Disable();
        }


        private void Start()
        {
            playerCurrentHp = playerHp;
            if (onHpChanged != null)
                onHpChanged(playerCurrentHp, playerHp);
        }
        // Update is called once per frame
        void Update()
        {
            if (!GameController.Instance.IsActive())
                return;
            Vector2 direction = Vector2.zero;
            if (!usedInputSystem)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                direction = new Vector2(horizontal, vertical);
                if (Input.GetKey(KeyCode.Space))
                {
                    if (tempCoolDown <= 0)
                    {
                        Fire();
                        tempCoolDown = fireCoolDown;
                    }
                }
            }
            else
            {
                direction = movementInputValue;

                if (attackInputValue)
                {
                    if (tempCoolDown <= 0)
                    {
                        Fire();
                        tempCoolDown = fireCoolDown;
                    }
                }
            }


            // Di chuyển người chơi
            Vector3 newPosition = transform.position + new Vector3(direction.x * Time.deltaTime * moveSpeed, direction.y * Time.deltaTime * moveSpeed, 0);

            // Lấy tọa độ mép trên cùng và dưới cùng của màn hình
            Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 bottomLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

            // Giới hạn tọa độ x và y trong khoảng đã chỉ định
            newPosition.x = Mathf.Clamp(newPosition.x, bottomLeftCorner.x, topRightCorner.x);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomLeftCorner.y, topRightCorner.y);

            // Áp dụng tọa độ mới
            transform.position = newPosition;


            tempCoolDown -= Time.deltaTime;

        }
        protected virtual void Fire()
        {
            ProjectileController projectileCtrl = SpawnManager.Instance.SpawnPlayerProjectile(firePoint.position);
            projectileCtrl.Fire(1);
            SpawnManager.Instance.SpawnShootingFX(firePoint.position);
            AudioManager.Instance.PlayRocketSFXClip();
        }
        public void GetHit(int enemyDamage)
        {
            playerCurrentHp -= enemyDamage;
            Vector3 curPos = transform.position;
            curPos.y += 1f;
            Vector3 textPos = new Vector3(curPos.x, curPos.y, curPos.z);
            FloatingTextController dameText = SpawnManager.Instance.SpawnFloatingText(textPos);
            dameText.transform.GetChild(0).GetComponent<TextMesh>().text = enemyDamage.ToString();
            if (onHpChanged != null)
                onHpChanged(playerCurrentHp, playerHp);
            if (playerCurrentHp <= 0)
            {
                SpawnManager.Instance.SpawnDestroyPlayerFX(transform.position);
                Destroy(gameObject);
                GameController.Instance.GameOver(false);
                Invoke("ClearGameOver", 2f);
                AudioManager.Instance.PlayDestroySFXClip();
                AudioManager.Instance.PlayGameOverSFXClip();
            }
            AudioManager.Instance.PlayHitSFXClip();
        }

        public void ClearGameOver()
        {
            SpawnManager.Instance.Clear();
        }
        public void DestroyPlayer()
        {
            if (!isDestroyed)
            {
                Destroy(gameObject);
                isDestroyed = true;
            }
            else
            {
                isDestroyed = false;
            }

        }
        private void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (obj.started)
            {
                attackInputValue = true;
            }
            else if (obj.performed)
            {
                attackInputValue = true;
            }
            else if (obj.canceled)
            {
                attackInputValue = false;
            }
        }

        private void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (obj.started)
            {
                movementInputValue = obj.ReadValue<Vector2>();
            }
            else if (obj.performed)
            {
                movementInputValue = obj.ReadValue<Vector2>();
            }
            else if (obj.canceled)
            {
                movementInputValue = Vector2.zero;
            }
        }

        public Vector3 PlayerPos()
        {
            return transform.position;
        }    

    }
}