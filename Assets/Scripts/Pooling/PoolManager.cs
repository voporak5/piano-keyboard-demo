using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCintron.Pooling
{
    public abstract class PoolManager
    {
        protected int totalNodes;
        protected int deltaGrow;
        protected int numReserved;
        protected int numActive;

        //Dictionary to quickly find a specific active node
        private Dictionary<int, NodeBase> active;
        //Stack so data can easily grow/shrink
        private Stack<NodeBase> reserve;

        public PoolManager(/*int initialSize = 5, */int deltaGrow = 2)
        {
            this.deltaGrow = deltaGrow;
            totalNodes = 0;
            numReserved = 0;
            numActive = 0;
            active = new Dictionary<int, NodeBase>();
            reserve = new Stack<NodeBase>();

            //privFillReservedPool(initialSize);
        }

        public NodeBase add()
        {
            NodeBase pNodeBase = privAdd();            

            // copy to active
            active[pNodeBase.GetHashCode()] = pNodeBase;

            return pNodeBase;
        }

        public void remove(NodeBase node)
        {
            active.Remove(node.GetHashCode());

            node.Wash();

            reserve.Push(node);

            numActive--;
            numReserved++;
        }

        private NodeBase privAdd()
        {
            // Are there any nodes on the Reserve list?
            if (reserve.Count == 0)
            {
                // refill the reserve list by the DeltaGrow
                privFillReservedPool(deltaGrow);
            }

            // Always take from the reserve list
            NodeBase node = reserve.Pop();
            
            // Wash it
            derivedWash(node);

            // Update stats
            numActive++;
            numReserved--;

            return node;
        }

        protected void privFillReservedPool(int count)
        {
            totalNodes += count;
            numReserved += count;
            
            // Preload the reserve
            for (int i = 0; i < count; i++)
            {
                NodeBase node = derivedCreateNode();
                derivedWash(node);
                reserve.Push(node);
            }
        }

        abstract protected NodeBase derivedCreateNode();
        abstract protected void derivedWash(NodeBase node);
    }
}