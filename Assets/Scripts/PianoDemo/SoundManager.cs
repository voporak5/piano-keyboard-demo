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
            SoundEffect soundEffect = (SoundEffect)instance.add();
            soundEffect.Set(soundEffectType);
        }

        public static void Remove(SoundEffect soundEffect)
        {
            instance.remove(soundEffect);
        }

        protected override NodeBase derivedCreateNode()
        {
            if(prefab == null) prefab = (GameObject)Resources.Load("Prefabs/SoundEffect");

            NodeBase node = Object.Instantiate(prefab).GetComponent<NodeBase>();
            return node;
        }

        protected override void derivedWash(NodeBase node)
        {
            SoundEffect sound = (SoundEffect)node;
            sound.Wash();
        }
    }
}