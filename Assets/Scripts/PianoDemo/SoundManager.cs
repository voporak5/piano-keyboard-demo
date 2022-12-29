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

        public SoundManager(GameObject prefab, int initialSize = 5, int deltaGrow = 2) 
            : base(/*initialSize,*/deltaGrow)
        {
            this.prefab = prefab;
            instance = this;
            privFillReservedPool(initialSize);
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