using System;
using System.Collections.Generic;
using TweetinviCore.Interfaces;
using TweetinviCore.Interfaces.Models;
using TweetinviLogic.Helpers;
using TweetinviLogic.Helpers.Visitors;

namespace TweetinviLogic.Model
{
    /// <summary>
    /// Contains suggestions of friends made to a user
    /// </summary>
    public class SuggestedUserList: ISuggestedUserList
    {
        #region Private Attributes
        /// <summary>
        /// Slug: attribute that identifies the list of suggested users
        /// The other attributes give details about the list. 
        /// Name: name of the list of suggested user
        /// Size: number of member in the list of suggested users
        /// Members: the list of suggested users
        /// </summary>
        private string _slug;
        private string _name;
        private int _size;
        private List<IUser> _members;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a list of suggested users with 3 mandatory field values
        /// </summary>
        /// <param name="name">Name of the list</param>
        /// <param name="slug">Id of the list</param>
        /// <param name="size">Number of members of the list</param>
        public SuggestedUserList(string name, string slug, int size)
        {
            this._name = name;
            this._slug = slug;
            this._size = size;
        }
        #endregion

        #region Public Accessors
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Slug
        {
            get { return _slug; }
            set { _slug = value; }
        }
        
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
        
        public List<IUser> Members 
        {
            get { return _members; }
            set { _members = value; }
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Reset the values of all the detail fields: name, size, members.
        /// Null is assigned to the attributes name and members. -1 is assigned to the attribute size
        /// </summary>
        private void ResetAllDetailFields()
        {
            _name = null;
            _size = -1;
            ResetMembers();
        }

        /// <summary>
        /// Reset the value of the attribute members to null.
        /// </summary>
        private void ResetMembers()
        {
            _members = null;
        }

        /// <summary>
        /// Reset the value of the member attribute to null and throw an exception.
        /// </summary>
        private void HandleMembersErrorFromTwitter()
        {
            ResetMembers();
            throw new Exception("no users returned by Twitter for this suggested list");
        }

        /// <summary>
        /// Create a scheduled task that will be launched 50 minutes later. 
        /// The task will reset the values of the detail fields of this list of suggested users.
        /// </summary>
        private void AddScheduledTask()
        {
            new ScheduledTask<SuggestedUserList>(3000000, this, new ResetSuggestedUserListDetailsVisitor());
        }

        /// <summary>
        /// Iterate over the objects given in parameter and create the associated Users.
        /// Build the list containing all these users and assign it to the attribute members.
        /// If the table given in parameter is null, throw an exception and reset the attributes members to null.
        /// </summary>
        /// <param name="suggUsersTable">table in which each object represents a user</param>
        private void ExtractMembers(Dictionary<string, object>[] suggUsersTable)
        {
            // Throw exception if parameter is null
            if (suggUsersTable == null)
            {
                HandleMembersErrorFromTwitter();
            }

            // Build the list of users
            List<IUser> refreshedUserList = new List<IUser>();
            foreach (Dictionary<string, object> su in suggUsersTable)
            {
                throw new NotImplementedException();
                //User u = User.Create(su);

                //if (u != null)
                //{
                //    refreshedUserList.Add(u);
                //}
            }
            // Keep the resulting list to the attribute
            _members = refreshedUserList;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Retrieve the data from the Twitter API for the list of suggested users associated to the attribute _slug.
        /// Process this data and store it in the attributes name, size and members
        /// An Exception is thrown the parameter is null, if no data could be retrieved from Twitter, or if it is incomplete.
        /// </summary>
        /// <param name="token">Token used to request data from the Twitter API</param>
        public void RefreshAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve the members from the Twitter API for the list of suggested users associated to the attribute _slug.
        /// Process this data and store it in the attributes members
        /// An Exception is thrown the parameter is null, or if no data could be retrieved from Twitter.
        /// </summary>
        /// <param name="token">Token used to request data from the Twitter API</param>
        public void RefreshMembers()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}