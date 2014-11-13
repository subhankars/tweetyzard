using TweetinviLogic.Model;

namespace TweetinviLogic.Helpers.Visitors
{
    public class ResetSuggestedUserListDetailsVisitor : TwitterObjectVisitor<SuggestedUserList>
    {
        /// <summary>
        /// Reset the values of the attributes of the list of suggested users given in parameter
        /// </summary>
        /// <param name="sul">List of suggested users</param>
        public override void Visit(SuggestedUserList sul)
        {
            if (sul != null)
            {
                sul.Name = null;
                sul.Size = -1;

                if (sul.Members != null)
                {
                    sul.Members.Clear();
                }
            }
        }
    }
}