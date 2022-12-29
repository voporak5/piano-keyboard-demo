using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCintron.Pooling;

namespace CCintron.PianoDemo
{
    public class SoundManager : PoolManager
    {
        private static SoundManager instance;

        private GameObject prefab;

        public SoundManager(int initialSize = 5, int deltaGrow = 2) 
            : base(initialSize, deltaGrow)
        {
            instance = this;
        }

        public static void Add(SoundEffectType soundEffectType)
        {
            SoundEffect soundEffect = (SoundEffect)instance.Add();
            soundEffect.Set(soundEffectType);
        }

        public static void Remove(SoundEffect soundEffect)
        {
            instance.Remove((NodeBase)soundEffect);
        }

        protected override NodeBase DerivedCreateNode()
        {
            if(prefab == null) prefab = (GameObject)Resources.Load("Prefabs/SoundEffect");

            NodeBase node = Object.Instantiate(prefab).GetComponent<NodeBase>();
            node.gameObject.SetActive(false);
            return node;
        }

        protected override void DerivedWash(NodeBase node)
        {
            SoundEffect sound = (SoundEffect)node;
            sound.Wash();
        }
    }
}