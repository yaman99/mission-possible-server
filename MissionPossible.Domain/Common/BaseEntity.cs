namespace MissionPossible.Domain.Common {
    public abstract class BaseEntity<T> : IIdentifiable<T>
    {
        public T Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; protected set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int Version { get; set; } = 1;
        public BaseEntity(T id)
        {
            Id = id;
            CreatedDate = DateTime.UtcNow;
            SetUpdatedDate();
        }

        protected virtual void SetUpdatedDate()
            => UpdatedDate = DateTime.UtcNow;
        public void SetDelete(bool value)
            => IsDeleted = value;
       
    }
}