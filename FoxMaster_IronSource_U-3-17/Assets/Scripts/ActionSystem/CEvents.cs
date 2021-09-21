using System.Collections.Generic;

namespace ActionSystem
{
    public delegate void SubscriberFunction();
    //public delegate void SubscriberFunctionParam();

    public enum EEventType
    {
        eRestartGameEvent,
        eAttentionCuckoo,
        eChangeActivePage
    }

    public class CEvents
    {
        public void Register(EEventType mEventType, SubscriberFunction mSubscriberFunction)
        {
            FunctionList(mEventType).Add(mSubscriberFunction);
        }

        /*public void RegisterParam(EEventType mEventType, SubscriberFunctionParam mSubscriberFunctionParam)
        {

        }*/

        public void Unregister(EEventType mEventType, SubscriberFunction mSubscriberFunction)
        {
            FunctionList(mEventType).Remove(mSubscriberFunction);
        }

        public void Invoke(EEventType mEventType)
        {
            foreach (SubscriberFunction mSubscriberFunction in FunctionList(mEventType))
            {
                mSubscriberFunction.Invoke();
            }
        }

        /*public void InvokeParam(EEventType mEventType, Chicken chicken)
        {
            //foreach (SubscriberFunctionParam mSubscriberFunctionParam in FunctionList(mEventType))
            //{
            //    mSubscriberFunctionParam.InvokeParam();
            //}
        }*/

        protected Dictionary<EEventType, List<SubscriberFunction>> mDictionary = new Dictionary<EEventType, List<SubscriberFunction>>();

        protected List<SubscriberFunction> FunctionList(EEventType mEventType)
        {

            if (mDictionary.ContainsKey(mEventType) == false)
            {
                mDictionary[mEventType] = new List<SubscriberFunction>();
            }

            return mDictionary[mEventType];
        }
    }
}


