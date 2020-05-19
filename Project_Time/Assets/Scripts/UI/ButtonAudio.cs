using System;
using System.Collections;
using System.Collections.Generic;
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
            audioSource = GameObject.FindGameObjectWithTag(UnityTags.MainUI.ToString()).GetComponent<AudioSource>();
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