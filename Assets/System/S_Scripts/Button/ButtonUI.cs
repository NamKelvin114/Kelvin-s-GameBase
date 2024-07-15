using System.Collections;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kelvin.ButtonUI
{
    public class ButtonUI : Button
    {
        /// <summary>
        ///     Custom Properties
        /// </summary>
        [SerializeField] private bool isPlaySound;

        [SerializeField] private bool isVibrate;

        [SerializeField] [Range(0, 1)] private float sizePressed = 0.9f;

        [SerializeField] private float timeToHold = 1;

        /// <summary>
        ///     Button Event
        /// </summary>
        public ButtonClickedEvent OnBUttonUp;

        public ButtonClickedEvent OnButtonDown;

        public ButtonClickedEvent OnButtonHold;

        public ButtonClickedEvent OnButtonUpdate;

        private Tween _buttonTween;

        private bool _isButtonDown;

        /// <summary>
        ///     Local Properties
        /// </summary>
        private Vector3 _originSize;

        private Coroutine holdCoroutine;

        private void Update()
        {
            if (OnButtonUpdate != null && _isButtonDown) OnButtonUpdate?.Invoke();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _originSize = transform.localScale;
        }


        protected override void OnDisable()
        {
            base.OnDisable();
            StopUse();
        }

        private void StopUse()
        {
            transform.localScale = _originSize;
            _buttonTween.Stop();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _buttonTween = Tween.Scale(transform, _originSize * sizePressed, 0.15f, Ease.OutCubic);
            _isButtonDown = true;
            OnButtonDown?.Invoke();
            if (OnButtonHold != null) holdCoroutine = StartCoroutine(HandleHolding());
            if (isVibrate)
            {
                //PLay Vibrate
            }

            if (isPlaySound)
            {
                //PLay Sound
            }
        }

        private IEnumerator HandleHolding()
        {
            yield return new WaitForSeconds(timeToHold);
            OnButtonHold.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _buttonTween = Tween.Scale(transform, _originSize, 0.15f, Ease.InOutCubic);
            _isButtonDown = false;
            OnBUttonUp?.Invoke();
            StopCoroutine(holdCoroutine);
        }
    }
}