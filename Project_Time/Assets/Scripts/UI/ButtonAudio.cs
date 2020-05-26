using System;
using System.Collections;
using System.Collections.Generic;
using ProjectTime.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectTime.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
    {
        // Declarations
        #region Declarations
        [SerializeField] AudioClip hoverSound;
        [SerializeField] AudioClip clickSound;
        AudioSource audioSource;
        #endregion

        // Unity Methods
        #region Unity Methods
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClicked);
            var player = GameObject.FindObjectOfType<Player>();
            if (player == null)
                audioSource = GameObject.FindGameObjectWithTag(UnityTags.MainUI.ToString()).GetComponent<AudioSource>();
            else
                audioSource = player.GetComponent<AudioSource>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            audioSource.PlayOneShot(hoverSound);
        }
        #endregion

        // Public Methods
        #region Public Methods

        #endregion

        // Private Methods
        #region Private Methods
        private void OnClicked()
        {
            audioSource.PlayOneShot(clickSound);
        }
        #endregion
    }
}