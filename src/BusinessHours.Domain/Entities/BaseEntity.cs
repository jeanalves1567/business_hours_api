using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessHours.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value == null ? DateTime.UtcNow : value; }
        }

        public DateTime? UpdatedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public BaseEntity(string id)
        {
            Id = id;
        }
    }
}
