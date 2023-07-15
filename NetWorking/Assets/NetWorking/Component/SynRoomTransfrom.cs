using System;
using DG.Tweening;
using GameData;
using NetWorking.Net;
using NetWorking.Tool;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace NetWorking.Component
{
    [RequireComponent(typeof(NetWorkingID))]
    [RequireComponent(typeof(Transform))]
    public class SynRoomTransfrom : MonoBehaviour
    {
        public float howOftenDoYouSyn;
        public bool isFraming;
        public float synSpeed = 0.2f;


        private Vector3 _loc;
        
        private Tweener _moveTweener;
        private Tweener _rotationTweener;
        private Tweener _scaleTweener;


        private void Start()
        {
            if (!gameObject.IsLocal()) return;
            
            InvokeRepeating("SendTransform",0,howOftenDoYouSyn);
        }

        private void Update()
        {
            if (!gameObject.IsLocal()) return;
            if (isFraming)
            {
                if (_loc.Equals(transform.position)) return;
                _loc = transform.position;
                NetManager.Instance.SenMessage(GetMsg());
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }


        private void SendTransform()
        {
            if (_loc.Equals(transform.position)) return;
            _loc = transform.position;
            NetManager.Instance.SenMessage(GetMsg());
        }

        public void SynTransform(Data data)
        {
            _moveTweener?.Kill();
            _rotationTweener?.Kill();
            _scaleTweener?.Kill();

            _moveTweener=transform.DOMove(data.GameTransfrom.Position.Vector.GameVector3ToMonoVector3(),synSpeed);
            _rotationTweener=transform.DORotateQuaternion(Quaternion.Euler(data.GameTransfrom.Ratation.Vector.GameVector3ToMonoVector3()), synSpeed);
            _scaleTweener=transform.DOScale(data.GameTransfrom.Scale.Vector.GameVector3ToMonoVector3(), synSpeed);
            
        }

        private Data GetMsg()
        {
            Data data = new Data
            {
                MsgType = MsgType.RoomMsg,
                RoomMsgType = RoomMsgType.RoomTransformMsg,
                PlayerData = new PlayerData{ID = NetManager.Instance.GetID()},
                
                GameTransfrom = new GameTransfrom
                {
                    Position = new Position{Vector = transform.position.MonoVector3ToGameVector3()},
                    Ratation = new Ratation{Vector = transform.eulerAngles.MonoVector3ToGameVector3()},
                    Scale = new Scale{Vector = transform.localScale.MonoVector3ToGameVector3()}
                }
            };

            return data;
        }

        
    }
}