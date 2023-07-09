using System;
using GameData;
using NetWorking.Tool;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace NetWorking.Component
{
    [RequireComponent(typeof(Transform))]
    public class SynTransfrom : MonoBehaviour
    {
        public float howOftenDoYouSyn;
        public bool isFraming;
        
        
        
        private void OnEnable()
        {
            InvokeRepeating("SendTransform",0,howOftenDoYouSyn);
        }

        private void Update()
        {
            if (isFraming)
            {
                NetManager.instance.SenMessage(GetMsg());
            }
        }

        private void OnDisable()
        {
            CancelInvoke();
        }


        private void SendTransform()
        {
            NetManager.instance.SenMessage(GetMsg());
        }

        public void SynTransform(Data data)
        {
            transform.position = data.GameTransfrom.Position.Vector.GameVector3ToMonoVector3();
            transform.eulerAngles = data.GameTransfrom.Ratation.Vector.GameVector3ToMonoVector3();
            transform.localScale = data.GameTransfrom.Scale.Vector.GameVector3ToMonoVector3();
        }

        private Data GetMsg()
        {
            Data data = new Data
            {
                MsgType = MsgType.TransformMsg,
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