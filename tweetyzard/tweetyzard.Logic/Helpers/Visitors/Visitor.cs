namespace TweetinviLogic.Helpers.Visitors
{
    public interface IVisitor<T>
    {
        /// <summary>
        /// Process a generic object
        /// </summary>
        /// <param name="o">Instance of Generic object</param>
        void Visit(T o);
    }
}