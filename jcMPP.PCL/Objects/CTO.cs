namespace jcMPP.PCL.Objects {
    public class CTO<T> {
        public T Value { get; set; }

        public string Exception { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Exception);

        public CTO() {
            Value = default(T);
            Exception = null;
        }

        public CTO(T objectValue, string exception = null) {
            Value = objectValue;
            Exception = exception;
        }
    }
}