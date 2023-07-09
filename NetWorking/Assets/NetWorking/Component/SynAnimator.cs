using System;
using GameData;
using UnityEngine;

namespace NetWorking.Component
{
    [RequireComponent(typeof(Animator))]
    public class SynAnimator : MonoBehaviour
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
            if (isFraming&&NetManager.instance.IsOnline())
            {
                NetManager.instance.SenMessage(GetMsg());
            }
        }


        private void SendAnimData()
        {
            if (NetManager.instance.IsOnline())
            {
                NetManager.instance.SenMessage(GetMsg());
            }
        }

        
        


        private Data GetMsg()
        {
            Data data = new Data
            {
                MsgType = MsgType.AnimMsg,
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