using Link.Slicer.Domain.Common;

namespace Link.Slicer.Domain.Entities
{
    /// <summary>
    /// Урл
    /// </summary>
    public class Url : BaseEntity
    {
        /// <summary>
        /// Ключ сокращения
        /// </summary>
        public string Shortening { get; set; }
        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTimeOffset? ExpiresAt { get; set; }
        /// <summary>
        /// Протокол
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Домен
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }
}
