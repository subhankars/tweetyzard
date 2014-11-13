﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.35312
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TweetinviFactories.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TweetinviFactories.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/friendships/show.json?{0}&amp;{1}.
        /// </summary>
        internal static string Friendship_GetRelationship {
            get {
                return ResourceManager.GetString("Friendship_GetRelationship", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/friendships/lookup.json?{0}.
        /// </summary>
        internal static string Friendship_GetRelationships {
            get {
                return ResourceManager.GetString("Friendship_GetRelationships", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/lists/create.json?name={0}&amp;mode={1}.
        /// </summary>
        internal static string List_Create {
            get {
                return ResourceManager.GetString("List_Create", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &amp;description={0}.
        /// </summary>
        internal static string List_Create_DescriptionParameter {
            get {
                return ResourceManager.GetString("List_Create_DescriptionParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/lists/show.json?{0}.
        /// </summary>
        internal static string List_GetExistingList {
            get {
                return ResourceManager.GetString("List_GetExistingList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/direct_messages/show.json?id={0}.
        /// </summary>
        internal static string Message_GetMessageFromId {
            get {
                return ResourceManager.GetString("Message_GetMessageFromId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/saved_searches/create.json?query={0}.
        /// </summary>
        internal static string SavedSearch_Create {
            get {
                return ResourceManager.GetString("SavedSearch_Create", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/saved_searches/show/{0}.json.
        /// </summary>
        internal static string SavedSearch_Get {
            get {
                return ResourceManager.GetString("SavedSearch_Get", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/account/verify_credentials.json.
        /// </summary>
        internal static string TokenUser_GetCurrentUser {
            get {
                return ResourceManager.GetString("TokenUser_GetCurrentUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/statuses/show.json?id={0}&amp;include_my_retweet=true.
        /// </summary>
        internal static string Tweet_Get {
            get {
                return ResourceManager.GetString("Tweet_Get", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/users/show.json?user_id={0}.
        /// </summary>
        internal static string User_GetUserFromId {
            get {
                return ResourceManager.GetString("User_GetUserFromId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/users/show.json?screen_name={0}.
        /// </summary>
        internal static string User_GetUserFromName {
            get {
                return ResourceManager.GetString("User_GetUserFromName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/users/lookup.json?user_id={0}.
        /// </summary>
        internal static string User_GetUsersFromIds {
            get {
                return ResourceManager.GetString("User_GetUsersFromIds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://api.twitter.com/1.1/users/lookup.json?screen_name={0}.
        /// </summary>
        internal static string User_GetUsersFromNames {
            get {
                return ResourceManager.GetString("User_GetUsersFromNames", resourceCulture);
            }
        }
    }
}
