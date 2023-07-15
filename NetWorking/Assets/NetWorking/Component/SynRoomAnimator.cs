using System;
using GameData;
using NetWorking.Net;
using NetWorking.Tool;
using UnityEngine;

namespace NetWorking.Component
{
    [RequireComponent(typeof(NetWorkingID))]
    [RequireComponent(typeof(Animator))]
    public class SynRoomAnimator : MonoBehaviour
    {
        public float howOftenDoYouSyn;
        public bool isFraming;
        
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            if (!gameObject.IsLocal()) return;
            if (!isFraming)
            {
                InvokeRepeating("SendAnimData",0,howOftenDoYouSyn);
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Update()
        {
            if (!gameObject.IsLocal()) return;
            if (isFraming&&NetManager.Instance.IsOnline())
            {
                NetManager.Instance.SenMessage(GetMsg());
            }
        }


        private void SendAnimData()
        {
            if (NetManager.Instance.IsOnline())
            {
                NetManager.Instance.SenMessage(GetMsg());
            }
        }

        
        


        private Data GetMsg()
        {
            Data data = new Data
            {
                MsgType = MsgType.RoomMsg,
                RoomMsgType = RoomMsgType.RoomAnimMsg,
                AnimParameters = new AnimParameters()
            };
            foreach (var item in _animator.parameters)
            {
                data.AnimParameters.AnimData.Add(new AnimData{AnimDataName = item.name,
                    AnimDataType = (int)item.type,
                    FloatData = _animator.GetFloat(item.name),
                    IntData = _animator.GetInteger(item.name),
                    BoolData = _animator.GetBool(item.name)
                    
                });
            }
            
            

            return data;
        }

        
        public void SynAnimData(Data data)
        {
            foreach (var item in data.AnimParameters.AnimData)
            {
                switch ((AnimatorControllerParameterType)item.AnimDataType)
                {
                    case AnimatorControllerParameterType.Bool:
                        _animator.SetBool(item.AnimDataName,item.BoolData);
                        break;
                    case AnimatorControllerParameterType.Float:
                        _animator.SetFloat(item.AnimDataName,item.FloatData);
                        break;
                    case AnimatorControllerParameterType.Int:
                        _animator.SetInteger(item.AnimDataName,item.IntData);
                        break;
                    default:
                        break;
                }
            }
        }
        
        
    }
}