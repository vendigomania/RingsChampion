using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Game;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private GameObject hud;

        [SerializeField] private UI.PauseScreen pauseScreen;
        [SerializeField] private ResultScreen resultScreen;
        [SerializeField] private RectTransform backgroundTransform;

        [Header("Ingame")]
        [SerializeField] private PinkRing[] rings;
        [SerializeField] private Transform platform;
        [SerializeField] private Problem[] problems;

        [SerializeField] private Animator characterJump;
        [SerializeField] private Rigidbody2D characterRgb;

        private Transform lastRing;

        // Start is called before the first frame update
        void Start()
        {
            for(int i = 1; i < rings.Length; i++)
            {
                rings[i] = Instantiate(rings[0], rings[0].transform.parent);
            }

            for(int i = 1; i < problems.Length; i++)
            {
                problems[i] = Instantiate(problems[0], problems[0].transform.parent);
            }

            WinTrigger.OnTrigger += Win;
            LoseTrigger.OnLose += Lose;
            JumpTrgger.OnTrigger += Connect;

            TouchControlScreen.OnDragAction += SetTarget;
            TouchControlScreen.OnDragEndAction += Jump;

            ResetGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (characterRgb.velocity.y < -5f)
            {
                Lose();
            }

            backgroundTransform.anchoredPosition = Vector2.up * Mathf.Clamp((characterRgb.transform.position.y) * -60, -1200f, 0f);

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, new Vector3(0f, characterRgb.transform.position.y, -10f), Time.deltaTime * 2f);
        }

        public void StartGame()
        {
            hud.SetActive(true);
        }

        public void ResetGame()
        {
            hud.SetActive(false);
            resultScreen.Hide();
            pauseScreen.Hide();

            rings[0].transform.localPosition = new Vector2(Random.Range(0, 2) == 1? -1.5f : 1.5f, 0f);
            for (int i = 1; i < rings.Length; i++)
            {
                rings[i].transform.localPosition = new Vector2(Random.Range(-1.8f, 1.8f), 3 * i);
                rings[i].Init(i % 3 == 0);

                if(i % 4 == 0)
                {
                    int index = i / 4 - 1;
                    problems[index].transform.localPosition = Vector2.up * rings[i].transform.localPosition.y;
                    problems[index].transform.localScale = new Vector2(Mathf.Sign(rings[i].transform.localPosition.x), 1f);
                    problems[index].transform.rotation = Quaternion.Euler(0f, 0f, -15f + 15f * Random.Range(0, 3));
                    problems[index].SetRandom();
                }
            }

            lastRing = null;
            Connect(rings[0].transform);
        }

        public void Pause()
        {
            pauseScreen.Show("Pause");
        }


        private void Connect(Transform _ringTransform)
        {
            if (lastRing == _ringTransform) return;

            characterRgb.transform.parent = _ringTransform;
            characterRgb.simulated = false;
            characterRgb.velocity = Vector3.zero;
            characterRgb.transform.localPosition = Vector3.up * -0.3f;
            characterRgb.transform.rotation = Quaternion.identity;

            characterJump.SetFloat("Power", 0f);

            lastRing = _ringTransform;
        }

        private void SetTarget(Quaternion _quaternion, float _power)
        {
            characterRgb.transform.rotation = _quaternion;
            characterJump.SetFloat("Power", Mathf.Min(_power / 400f, 0.95f));
            Debug.Log(_power);
        }

        private void Jump(Vector2 _delta) //Shot
        {
            Debug.Log("Jump");

            characterRgb.transform.parent = characterRgb.transform.parent.parent;
            characterJump.SetFloat("Power", 0f);
            characterJump.Play("Jump");
            characterRgb.simulated = true;
            characterRgb.AddForce(_delta);

            GameAudio.Instance.Jump();
        }

        private void Win()
        {
            PlayerData.Instance.Money += 10;
            PlayerData.Instance.Score += 18;

            ResetGame();
            resultScreen.Show(true);
        }

        private void Lose()
        {
            ResetGame();
            resultScreen.Show(false);

            PlayerData.Instance.Score = 0;
        }
    }
}
