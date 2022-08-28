namespace StuffTest.Model
{
    /// <summary>
    /// Моделя для отображения ошибок. 
    /// По соображениям безопасности пользователю отдается только краткая ошибка и идентификатор на сервере.
    /// </summary>
    public class ErrorModel
    {
       

        /// <summary>
        /// Сообщение для пользователя
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Идентификатор для отслеживания ошибки
        /// </summary>
        public Guid CorrelationId { get; private set; } = Guid.NewGuid();
    }
}
