using System.ComponentModel.DataAnnotations;

namespace StuffTest.Model
{
    /// <summary>
    /// Модель для отображения пользователя
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [MaxLength(60)]
        public string FirstName { get; set; } = "";
        /// <summary>
        /// Фамилия
        /// </summary>
        [MaxLength(60)]
        public string LastName { get; set; } = "";
        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(60)]
        public string MiddleName { get; set; } = "";
        /// <summary>
        /// Должность
        /// </summary>
        public String Position { get; set; } = "";
    }
}
