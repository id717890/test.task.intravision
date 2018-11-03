namespace drinks.domain.@interface.entities
{
    using System;

    public abstract class Entity
    {
        public Int64 Id { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var obj_cast = obj as Entity;

            if (obj_cast != null)
            {
                return obj_cast.Id.Equals(Id);
            }

            return ReferenceEquals(obj, this);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
