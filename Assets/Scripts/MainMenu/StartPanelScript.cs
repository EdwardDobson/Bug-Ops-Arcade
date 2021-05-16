using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WowieJam2.MainMenu
{
    public class StartPanelScript : MonoBehaviour
    {
        public GameObject MainPanel;
        public AudioSource Click;
        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                // TODO fade into
                MainPanel.SetActive(true);
                gameObject.SetActive(false);
                Click.Play();
            }
        }
    }

}