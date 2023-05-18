namespace Link.Slicer.Domain.Common
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
