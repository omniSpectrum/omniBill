namespace omniBill.InnerComponents.Models
{
    public abstract class BaseModel
    {
        protected int key;

        public BaseModel(int key)
        {
            this.key = key;
        }

        public virtual int Key 
        { 
            get { return key; } 
            set { key = value; } 
        }
    }
}
