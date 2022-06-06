using System;
using System.Collections;
using Events.ScriptableObjects;
using InputSystem;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace RaceManagement
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private GameplayInputReader inputReader;
        [SerializeField] private VoidEventChannelSO onRaceStarted;
        
        private bool _coroutineFinished;
        private void OnEnable()
        {
            inputReader.SetInput();
            inputReader.GameplayInputEnabled(false);
            
            GetComponent<PhotonView>().RPC("StartRace", RpcTarget.AllBuffered, null);
            //StartCoroutine(RaceStart());
        }

        private IEnumerator RaceStart()
        {
            textMesh.enabled = true;
            yield return new WaitForSeconds(1f);
            StartCoroutine(DisplayText("Ready?"));
            yield return new WaitUntil(() => _coroutineFinished);
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(DisplayText("3"));
            yield return new WaitUntil(() => _coroutineFinished);
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(DisplayText("2"));
            yield return new WaitUntil(() => _coroutineFinished);
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(DisplayText("1"));
            yield return new WaitUntil(() => _coroutineFinished);
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(DisplayText("GO!"));
            yield return new WaitUntil(() => _coroutineFinished);
            inputReader.GameplayInputEnabled(true);
            textMesh.enabled = false;
            
            onRaceStarted.RaiseEvent();
        }

        private IEnumerator DisplayText(string text)
        {
            _coroutineFinished = false;
            textMesh.text = text;
            textMesh.fontSize = 380;
            textMesh.color = new Color(1,1,1,1);

            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                textMesh.fontSize -= 2.5f;
                textMesh.color -= new Color(0,0,0,0.1f);

                yield return new WaitForSeconds(0.00000001f);

                if (textMesh.color.a <= 0.1f)
                {
                    textMesh.color = new Color(1,1,1,0);
                    break;
                }
            }
            _coroutineFinished = true;
        }
        
        [PunRPC]
        public void StartRace()
        {
            StartCoroutine(RaceStart());
        }
    }
}