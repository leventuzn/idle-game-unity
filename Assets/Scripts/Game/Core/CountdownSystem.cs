using System;
using UnityEngine;

namespace Game.Core
{
    /// <summary>
    /// This class handles the countdown system in the game. It is used to manage countdown and other classes can subscribe to it.
    /// </summary>
    public class CountdownSystem : MonoBehaviour
    {
        private bool _runningCountdown;
        private float _time;

        public Action onTick;

        private void Update()
        {
            if (!_runningCountdown)
            {
                return;
            }

            _time += Time.deltaTime;
            if (_time >= 1.0f)
            {
                _time = 0f;
                onTick?.Invoke();
            }

        }

        /// <summary>
        /// Registers the specified callback to be called per tick.
        /// </summary>
        /// <param name="callback">The callback to register.</param>
        public void Subscribe(Action callback)
        {
            _runningCountdown = true;
            onTick += callback;
        }

        /// <summary>
        /// Unregisters the specified callback to be called per tick.
        /// </summary>
        /// <param name="callback">The callback to unregister.</param>
        public void Unsubscribe(Action callback)
        {
            if (onTick != null)
            {
                onTick -= callback;
            }
        }
    }
}

