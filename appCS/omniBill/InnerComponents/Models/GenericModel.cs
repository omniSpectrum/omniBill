namespace omniBill.InnerComponents.Models
{
    public abstract class GenericModel
    {
        protected int key;

        public GenericModel(int key)
        {
            this.key = key;
        }

        public abstract int Key { get; set; }
    }
}
