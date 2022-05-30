﻿using System;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.HudUI
{
    public class RaceLapsUIController : MonoBehaviour
    {
        [Header("Event Channels")] [SerializeField]
        private IntEventChannelSO onSetMaxLapsCount;

        [SerializeField] private VoidEventChannelSO onRaceFinished;
        [SerializeField] private IntEventChannelSO onUpdateLapsCount;

        [SerializeField] int UpdateFrameCount = 3;
        [SerializeField] TextMeshProUGUI SpeedText;
        [SerializeField] TextMeshProUGUI CurrentGearText;

        [SerializeField] RectTransform TahometerArrow;
        [SerializeField] float MinArrowAngle = 0;
        [SerializeField] float MaxArrowAngle = -315f;

        int CurrentFrame;

        [SerializeField] private CarSO car;

        //  CarSO SelectedCar { get { return GameController.PlayerCar; } }

        private int _maxLapsCount;
        private int _lapsCount;

        private void Update()
        {
            if (CurrentFrame >= UpdateFrameCount)
            {
                UpdateGamePanel();
                CurrentFrame = 0;
            }
            else
            {
                CurrentFrame++;
            }

            UpdateArrow();
        }

        void UpdateArrow()
        {
            //var procent = car._engineRPM / car._maxRPM;
            //var angle = (MaxArrowAngle - MinArrowAngle) * procent + MinArrowAngle;
            //TahometerArrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            var procent = 0.3f *(car._engineRPM / car._maxRPM);
            var angle = (MaxArrowAngle - MinArrowAngle) * procent + MinArrowAngle;
            TahometerArrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //TahometerArrow.transform.Rotate(0,0,angle);
        }

        void UpdateGamePanel()
        {
            //SpeedText.text = car.{Tu jest miejsce na prędkość!}.ToString ("000");
            CurrentGearText.text = car._gearNum.ToString();
        }

        private void OnEnable()
        {
            onSetMaxLapsCount.OnEventRaised += (int value) => _maxLapsCount = value;
            onUpdateLapsCount.OnEventRaised += (int value) => _lapsCount = value;
        }

        private void OnDisable()
        {
            onSetMaxLapsCount.OnEventRaised -= (int value) => _maxLapsCount = value;
            onUpdateLapsCount.OnEventRaised -= (int value) => _lapsCount = value;
        }
    }
}