namespace NetWorking
{
    public class SingletonClass<T> where T: class,new()
    {
        private static T _Instance;

        public static T instance
        {
            get
            {
                if (_Instance==null)
                {
                    _Instance = new T();
                }

                return _Instance;
            }
        }
    }
}