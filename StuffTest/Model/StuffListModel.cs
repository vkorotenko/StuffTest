namespace StuffTest.Model
{
    /// <summary>
    /// Список пользователей
    /// </summary>
    public class StuffListModel
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        public IEnumerable<UserModel> Users { get; set; }
        /// <summary>
        /// Конструктор модели
        /// </summary>
        public StuffListModel()
        {
            Users = new List<UserModel>();
        }
    }
}
