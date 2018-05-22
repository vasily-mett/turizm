namespace turizm.Lib.Classes
{
    public interface ITopic
    {
        /// <summary>
        /// ID группы - владельца обсуждения
        /// </summary>
        long GroupID { get; set; }
        /// <summary>
        /// название
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// ID обсуждения
        /// </summary>
        long TopicID { get; set; }
    }
}