using System;

namespace Dews.Shared
{
    using System;

    public abstract class  Singleton<singletonType>
          where singletonType : new()
    {
        private static singletonType instance;

        static Singleton() { }
     
        public static singletonType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new singletonType();
                }
                return instance;
            }
        }
    }
}
